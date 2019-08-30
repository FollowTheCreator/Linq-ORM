using AutoMapper;
using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.PostModels;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        private readonly IConfigService _configService;
        private readonly ITagService _tagService;
        private readonly ICommentService _commentService;
        private readonly ICategoryService _categoryService;

        private readonly IMapper _mapper;

        public PostService
        (
            IPostRepository postRepository, 
            IConfigService configService, 
            ITagService tagService,
            ICommentService commentService,
            ICategoryService categoryService,
            IMapper mapper
        )
        {
            _postRepository = postRepository;

            _configService = configService;
            _tagService = tagService;
            _commentService = commentService;
            _categoryService = categoryService;

            _mapper = mapper;
        }

        public async Task CreateAsync(Post item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PostViewModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PostPreviewViewModel> GetPostPreviewsAsync(PageInfo pageInfo)
        {
            pageInfo.CheckPageInfo(_configService.GetPageSize());

            var convertedPageInfo = _mapper.Map<PageInfo, DAL.Interfaces.Models.PageInfo>(pageInfo);

            var postResult = await _postRepository.GetPostPreviewsAsync(convertedPageInfo);

            var result = await FillPostPreviewViewModel(postResult, pageInfo);

            return result;
        }

        public async Task<PostPreviewViewModel> GetPostPreviewsBySearchAsync(PageInfo pageInfo, string header)
        {
            if (string.IsNullOrWhiteSpace(header))
            {
                return await GetPostPreviewsAsync(pageInfo);
            }

            pageInfo.CheckPageInfo(_configService.GetPageSize());

            var convertedPageInfo = _mapper.Map<PageInfo, DAL.Interfaces.Models.PageInfo>(pageInfo);

            var postResult = await _postRepository.GetPostPreviewsBySearchAsync(convertedPageInfo, header);

            var result = await FillPostPreviewViewModel(postResult, pageInfo);

            return result;
        }

        public async Task<PostPreviewViewModel> GetPostPreviewsByTagAsync(PageInfo pageInfo, string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
            {
                return await GetPostPreviewsAsync(pageInfo);
            }

            pageInfo.CheckPageInfo(_configService.GetPageSize());

            var convertedPageInfo = _mapper.Map<PageInfo, DAL.Interfaces.Models.PageInfo>(pageInfo);

            var postResult = await _postRepository.GetPostPreviewsByTagAsync(convertedPageInfo, tag);

            var result = await FillPostPreviewViewModel(postResult, pageInfo);

            return result;
        }

        public async Task<PostPreviewViewModel> GetPostPreviewsByCategoryAsync(PageInfo pageInfo, Guid categoryId)
        {
            if (!await _categoryService.IsCategoryExistsAsync(categoryId))
            {
                return await GetPostPreviewsAsync(pageInfo);
            }

            pageInfo.CheckPageInfo(_configService.GetPageSize());

            var convertedPageInfo = _mapper.Map<PageInfo, DAL.Interfaces.Models.PageInfo>(pageInfo);

            var postResult = await _postRepository.GetPostPreviewsByCategoryAsync(convertedPageInfo, categoryId);

            var result = await FillPostPreviewViewModel(postResult, pageInfo);

            return result;
        }

        public async Task<PostViewModel> GetPostViewModelAsync(Guid id)
        {
            var postResult = await _postRepository.GetPostViewModelAsync(id);
            var convertedPostResult = _mapper.Map<DAL.Interfaces.Models.PostModels.PostViewModel, PostViewModel>(postResult);

            var tags = await _tagService.GetPostTagsAsync(convertedPostResult.Id);
            convertedPostResult.Tags = tags;

            var comments = await _commentService.GetPostCommentsAsync(convertedPostResult.Id);
            convertedPostResult.Comments = comments;

            var commentsCount = await _commentService.GetPostCommentsCount(convertedPostResult.Id);
            convertedPostResult.CommentsCount = commentsCount;

            return convertedPostResult;
        }

        public async Task<IEnumerable<Post>> GetRecordsAsync(PageInfo pageInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsPostExistsAsync(Guid id)
        {
            var result = await _postRepository.GetByIdAsync(id);

            return result != null;
        }

        public async Task UpdateAsync(Post item)
        {
            throw new NotImplementedException();
        }

        private static string TrimPost(string postContent)
        {
            if (postContent.Length > 300)
            {
                postContent = postContent.Substring(0, 300) + "...";
            }

            return postContent;
        }

        private async Task<PostPreviewViewModel> FillPostPreviewViewModel(List<DAL.Interfaces.Models.PostModels.PostPreview> postResult, PageInfo pageInfo)
        {
            foreach (var item in postResult)
            {
                item.Content = TrimPost(item.Content);
            }
            var convertedPostResult = _mapper.Map<List<DAL.Interfaces.Models.PostModels.PostPreview>, List<PostPreview>>(postResult);

            foreach (var post in convertedPostResult)
            {
                var tags = await _tagService.GetPostTagsAsync(post.Id);
                post.Tags = tags;
            }

            pageInfo.TotalItems = await _postRepository.RecordsCountAsync();
            pageInfo.TotalPages = (int)Math.Ceiling(pageInfo.TotalItems / (double)pageInfo.PageSize);

            var popularPosts = await GetPopularPostsAsync();
            var topPopularPosts = popularPosts.Take(_configService.GetPopularPostsCount()).ToList();

            var categories = await _categoryService.GetCategoriesAsync();

            var allTags = await _tagService.GetTagsAsync();

            return new PostPreviewViewModel
            {
                PostPreviews = convertedPostResult,
                PageInfo = pageInfo,
                PopularPosts = topPopularPosts,
                Categories = categories,
                Tags = allTags
            };
        }

        public async Task<List<PopularPost>> GetPopularPostsAsync()
        {
            var result = await _postRepository.GetPopularPostsAsync();
            var convertedResult = _mapper.Map<List<DAL.Interfaces.Models.PostModels.PopularPost>, List<PopularPost>>(result);

            return convertedResult;
        }

        public async Task<List<UserPost>> GetUserPostsAsync(Guid userId)
        {
            var result = await _postRepository.GetUserPostsAsync(userId);
            var convertedResult = _mapper.Map<List<DAL.Interfaces.Models.PostModels.UserPost>, List<UserPost>>(result);

            return convertedResult;
        }
    }
}
