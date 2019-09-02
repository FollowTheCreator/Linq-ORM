using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models;
using ShareMe.DAL.Interfaces.Models.PostModels;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.DAL.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ShareMeContext context)
            : base(context)
        { }

        public async Task<List<Post>> GetPostsAsync(PageInfo pageInfo)
        {
            var result = await DbSet
                .Include(post => post.User)
                .Include(post => post.PostTag)
                    .ThenInclude(postTag => postTag.Tag)
                .Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize)
                .Take(pageInfo.PageSize)
                .ToListAsync();

            return result;
        }

        public async Task<List<Post>> GetPostPreviewsByTagAsync(PageInfo pageInfo, string tag)
        {
            var result = await DbSet
                .Include(post => post.User)
                .Include(post => post.PostTag)
                    .ThenInclude(postTag => postTag.Tag)
                .Where(post => post
                    .PostTag
                    .Where(postTag => postTag.Tag.Name == tag)
                    .Any()
                )
                .Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize)
                .Take(pageInfo.PageSize)
                .ToListAsync();

            return result;
        }

        public async Task<List<Post>> GetPostPreviewsBySearchAsync(PageInfo pageInfo, string header)
        {
            var result = await DbSet
               .Include(post => post.User)
               .Include(post => post.PostTag)
                   .ThenInclude(postTag => postTag.Tag)
               .Where(post => post
                   .Header
                   .Contains(header)
                )
               .Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize)
               .Take(pageInfo.PageSize)
               .ToListAsync();

            return result;
        }

        public async Task<List<Post>> GetPostPreviewsByCategoryAsync(PageInfo pageInfo, Guid categoryId)
        {
            var result = await DbSet
              .Include(post => post.User)
              .Include(post => post.PostTag)
                  .ThenInclude(postTag => postTag.Tag)
              .Where(post => post.CategoryId == categoryId)
              .Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize)
              .Take(pageInfo.PageSize)
              .ToListAsync();

            return result;
        }

        public async Task<List<Post>> GetPostsOrderedByViewsDescAsync()
        {
            var result = await DbSet
                .OrderByDescending(post => post.Views)
                .ToListAsync();

            return result;
        }

        public async Task<List<Post>> GetUserPostsAsync(Guid userId)
        {
            var result = await DbSet
                .Where(post => post.UserId == userId)
                .ToListAsync();

            return result;
        }

        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            var result = await DbSet
                .Include(post => post.User)
                .Include(post => post.Comment)
                    .ThenInclude(comment => comment.User)
                .Include(post => post.PostTag)
                    .ThenInclude(postTag => postTag.Tag)
                .FirstOrDefaultAsync(post => post.Id == id);

            return result;
        }
    }
}
