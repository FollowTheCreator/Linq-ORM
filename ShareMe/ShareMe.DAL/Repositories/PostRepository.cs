using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models;
using ShareMe.DAL.Interfaces.Models.CategoryModels;
using ShareMe.DAL.Interfaces.Models.PostModels;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.DAL.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly ShareMeContext _context;

        public PostRepository(ShareMeContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<PostPreview>> GetPostPreviewsAsync(PageInfo pageInfo)
        {
            var result = await DbSet
                .Include(post => post.User)
                .Select(post => 
                    new PostPreview
                    {
                        Id = post.Id,
                        Content = post.Content,
                        Header = post.Header,
                        Date = post.Date,
                        Image = post.Image,
                        UserId = post.User.Id,
                        UserName = post.User.Name
                    }
                )
                .ToListAsync();

            return result;
        }

        public async Task<List<PostPreview>> GetPostPreviewsByTagAsync(PageInfo pageInfo, string tag)
        {
            var result = await _context
                .PostTag
                .Include(postTag => postTag.Post)
                .ThenInclude(post => post.User)
                .Include(postTag => postTag.Tag)
                .Where(postTag => postTag.Tag.Name == tag)
                .Select(postTag => 
                    new PostPreview
                    {
                        Id = postTag.Post.Id,
                        Content = postTag.Post.Content,
                        Header = postTag.Post.Header,
                        Date = postTag.Post.Date,
                        Image = postTag.Post.Image,
                        UserId = postTag.Post.User.Id,
                        UserName = postTag.Post.User.Name
                    }
                )
                .ToListAsync();

            return result;
        }

        public async Task<List<PostPreview>> GetPostPreviewsBySearchAsync(PageInfo pageInfo, string header)
        {
            var result = await DbSet
                .Include(post => post.User)
                .Where(post => post.Header.Contains(header))
                .Select(post =>
                    new PostPreview
                    {
                        Id = post.Id,
                        Content = post.Content,
                        Header = post.Header,
                        Date = post.Date,
                        Image = post.Image,
                        UserId = post.User.Id,
                        UserName = post.User.Name
                    }
                )
                .ToListAsync();

            return result;
        }

        public async Task<List<PostPreview>> GetPostPreviewsByCategoryAsync(PageInfo pageInfo, Guid categoryId)
        {
            var result = await DbSet
                .Include(post => post.User)
                .Where(post => post.CategoryId == categoryId)
                .Select(post =>
                    new PostPreview
                    {
                        Id = post.Id,
                        Content = post.Content,
                        Header = post.Header,
                        Date = post.Date,
                        Image = post.Image,
                        UserId = post.User.Id,
                        UserName = post.User.Name
                    }
                )
                .ToListAsync();

            return result;
        }

        public async Task<PostViewModel> GetPostViewModelAsync(Guid id)
        {
            var result = await DbSet
                .Include(post => post.User)
                .Where(post => post.Id == id)
                .Select(post => new PostViewModel
                    {
                        Id = post.Id,
                        Content = post.Content,
                        Header = post.Header,
                        Date = post.Date,
                        Image = post.Image,
                        UserId = post.User.Id,
                        UserName = post.User.Name
                    }
                )
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<PopularPost>> GetPopularPostsAsync()
        {
            var result = await DbSet
                .OrderByDescending(post => post.Views)
                .Select(post => 
                    new PopularPost
                    {
                        Id = post.Id,
                        Header = post.Header,
                        Image = post.Image
                    }
                )
                .ToListAsync();

            return result;
        }

        public async Task<List<UserPost>> GetUserPostsAsync(Guid userId)
        {
            var result = await DbSet
                .Where(post => post.UserId == userId)
                .Select(post =>
                    new UserPost
                    {
                        Id = post.Id,
                        Header = post.Header,
                        Date = post.Date,
                        Image = post.Image
                    }
                )
                .ToListAsync();

            return result;
        }
    }
}
