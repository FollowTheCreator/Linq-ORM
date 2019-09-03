using AutoMapper;
using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.CommentModels;
using ShareMe.BLL.Interfaces.Models.PostModels;
using ShareMe.BLL.Interfaces.Models.PostTagModels;
using ShareMe.BLL.Interfaces.Models.TagModels;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        private readonly IConfigService _configService;
        private readonly ITagService _tagService;
        private readonly IPostTagService _postTagService;
        private readonly ICommentService _commentService;
        private readonly ICategoryService _categoryService;
        private readonly IIsEntityExistsService _isEntityExistsService;

        private readonly IMapper _mapper;

        public PostService
        (
            IPostRepository postRepository, 

            IConfigService configService, 
            ITagService tagService,
            IPostTagService postTagService,
            ICommentService commentService,
            ICategoryService categoryService,
            IIsEntityExistsService isEntityExistsService,

            IMapper mapper
        )
        {
            _postRepository = postRepository;

            _configService = configService;
            _tagService = tagService;
            _postTagService = postTagService;
            _commentService = commentService;
            _categoryService = categoryService;
            _isEntityExistsService = isEntityExistsService;

            _mapper = mapper;
        }

        public async Task CreateAsync(PostCreateModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            item.Views = 0;
            item.Date = DateTime.Now;

            var result = _mapper.Map<PostCreateModel, DAL.Interfaces.Models.PostModels.Post>(item);

            var postId = Guid.NewGuid();
            result.Id = postId;

            await _postRepository.CreateAsync(result);

            var tagIds = new List<Guid>(item.Tags.Count);
            foreach(var tag in item.Tags)
            {
                var currentTag = await _tagService.GetOrCreateAsync(tag);

                tagIds.Add(currentTag.Id);
            }

            foreach(var tagId in tagIds)
            {
                await _postTagService.CreateAsync(
                    new PostTag
                    {
                        PostId = postId,
                        TagId = tagId
                    }
                );
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await _commentService.DeleteByPostIdAsync(id);

            await _postTagService.DeleteByPostIdAsync(id);

            await _postRepository.DeleteAsync(id);
        }

        public async Task<PostPreviewViewModel> GetPostPreviewsAsync(PageInfo pageInfo)
        {
            if (pageInfo == null)
            {
                throw new ArgumentNullException(nameof(pageInfo));
            }

            pageInfo.CheckPageInfo(_configService.GetPageSize());

            var convertedPageInfo = _mapper.Map<PageInfo, DAL.Interfaces.Models.PageInfo>(pageInfo);

            var posts = await _postRepository.GetPostsAsync(convertedPageInfo);

            var result = await FillPostPreviewViewModel(posts, pageInfo);

            return result;
        }

        public async Task<PostPreviewViewModel> GetPostPreviewsBySearchAsync(PageInfo pageInfo, string header)
        {
            if (string.IsNullOrWhiteSpace(header))
            {
                return await GetPostPreviewsAsync(pageInfo);
            }

            if (pageInfo == null)
            {
                throw new ArgumentNullException(nameof(pageInfo));
            }

            pageInfo.CheckPageInfo(_configService.GetPageSize());

            var convertedPageInfo = _mapper.Map<PageInfo, DAL.Interfaces.Models.PageInfo>(pageInfo);

            var posts = await _postRepository.GetPostPreviewsBySearchAsync(convertedPageInfo, header);

            var result = await FillPostPreviewViewModel(posts, pageInfo);

            return result;
        }

        public async Task<PostPreviewViewModel> GetPostPreviewsByTagAsync(PageInfo pageInfo, string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
            {
                return await GetPostPreviewsAsync(pageInfo);
            }

            if (pageInfo == null)
            {
                throw new ArgumentNullException(nameof(pageInfo));
            }

            pageInfo.CheckPageInfo(_configService.GetPageSize());

            var convertedPageInfo = _mapper.Map<PageInfo, DAL.Interfaces.Models.PageInfo>(pageInfo);

            var posts = await _postRepository.GetPostPreviewsByTagAsync(convertedPageInfo, tag);

            var result = await FillPostPreviewViewModel(posts, pageInfo);

            return result;
        }

        public async Task<PostPreviewViewModel> GetPostPreviewsByCategoryAsync(PageInfo pageInfo, Guid categoryId)
        {
            if (!await _isEntityExistsService.IsCategoryExistsAsync(categoryId))
            {
                return await GetPostPreviewsAsync(pageInfo);
            }

            if (pageInfo == null)
            {
                throw new ArgumentNullException(nameof(pageInfo));
            }

            pageInfo.CheckPageInfo(_configService.GetPageSize());

            var convertedPageInfo = _mapper.Map<PageInfo, DAL.Interfaces.Models.PageInfo>(pageInfo);

            var posts = await _postRepository.GetPostPreviewsByCategoryAsync(convertedPageInfo, categoryId);

            var result = await FillPostPreviewViewModel(posts, pageInfo);

            return result;
        }

        public async Task<PostViewModel> GetPostViewModelAsync(Guid id)
        {
            if (!await _isEntityExistsService.IsPostExistsAsync(id))
            {
                throw new ArgumentException(nameof(id), "Post with this Id doesn't exist");
            }

            var post = await _postRepository.GetPostByIdAsync(id);

            var result = new PostViewModel
            {
                Id = post.Id,
                Header = post.Header,
                Content = post.Content,
                Date = post.Date,
                Image = post.Image,
                UserId = post.User.Id,
                UserName = post.User.Name
            };

            var tags = new List<string>(post.PostTag.Count);
            foreach(var postTag in post.PostTag)
            {
                tags.Add(postTag.Tag.Name);
            }
            result.Tags = tags;

            result.Comments = post
                .Comment
                .Where(comment => comment.ParentId == null)
                .Select(comment => 
                    new CommentViewModel
                    {
                        Id = comment.Id,
                        Content = comment.Content,
                        Date = comment.Date,
                        UserId = comment.UserId,
                        UserName = comment.User.Name,
                        UserImage = comment.User.Image,
                        Children = comment
                            .InverseParent
                            .Select(child =>
                                new CommentViewModel
                                {
                                    Id = child.Id,
                                    Content = child.Content,
                                    Date = child.Date,
                                    UserId = child.UserId,
                                    UserImage = child.User.Image,
                                    UserName = child.User.Name
                                }
                            )
                            .OrderBy(child => child.Date)
                            .ToList()
                    }
                )
                .OrderBy(comment => comment.Date)
                .ToList();

            result.CommentsCount = post.Comment.Count;

            return result;
        }

        public async Task UpdateAsync(Post item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var convertedItem = _mapper.Map<Post, DAL.Interfaces.Models.PostModels.Post>(item);

            await _postRepository.UpdateAsync(convertedItem);
        }

        private string TrimPost(string postContent, int maxLength)
        {
            if (postContent.Length > maxLength)
            {
                postContent = $"{postContent.Substring(0, maxLength)}...";
            }

            return postContent;
        }

        private async Task<PostPreviewViewModel> FillPostPreviewViewModel(List<DAL.Interfaces.Models.PostModels.Post> posts, PageInfo pageInfo)
        {
            var postPreviews = new List<PostPreview>(posts.Count);
            foreach (var post in posts)
            {
                postPreviews.Add(new PostPreview
                {
                    Id = post.Id,
                    Content = post.Content,
                    Date = post.Date,
                    Header = post.Header,
                    Image = post.Image,
                    Tags = post
                        .PostTag
                        .Select(postTag => postTag.Tag.Name)
                        .ToList(),
                    UserId = post.UserId,
                    UserName = post.User.Name
                });
            }

            var maxLength = _configService.GetMaxPreviewContentLength();
            foreach (var item in postPreviews)
            {
                item.Content = TrimPost(item.Content, maxLength);
            }

            foreach (var post in postPreviews)
            {
                var tags = await _tagService.GetPostTagsAsync(post.Id);
                post.Tags = tags;
            }

            pageInfo.TotalItems = await _postRepository.RecordsCountAsync();
            pageInfo.TotalPages = (int)Math.Ceiling(pageInfo.TotalItems / (double)pageInfo.PageSize);

            var popularPosts = await GetPopularPostsAsync();
            var topPopularPosts = popularPosts.Take(_configService.GetPopularPostsCount()).ToList();

            var categories = await _categoryService.GetCategoriesAsync();

            var allTags = await _tagService.GetTagsAsync(pageInfo);

            return new PostPreviewViewModel
            {
                PostPreviews = postPreviews,
                PageInfo = pageInfo,
                PopularPosts = topPopularPosts,
                Categories = categories,
                Tags = allTags
            };
        }

        public async Task<List<PopularPost>> GetPopularPostsAsync()
        {
            var orderedPosts = await _postRepository.GetPostsOrderedByViewsDescAsync();

            var result = orderedPosts
                .Select(post =>
                    new PopularPost
                    {
                        Id = post.Id,
                        Header = post.Header,
                        Image = post.Image
                    }
                )
                .ToList();

            return result;
        }

        public async Task<List<UserPost>> GetUserPostsAsync(Guid userId)
        {
            if(!await _isEntityExistsService.IsUserExistsAsync(userId))
            {
                throw new ArgumentException(nameof(userId), "User with this Id doesn't exist");
            }

            var posts = await _postRepository.GetUserPostsAsync(userId);

            var result = new List<UserPost>(posts.Count);
            foreach(var post in posts)
            {
                result.Add(
                    new UserPost
                    {
                        Id = post.Id,
                        Date = post.Date,
                        Header = post.Header,
                        Image = post.Image
                    }
                );
            }

            return result;
        }
    }
}
