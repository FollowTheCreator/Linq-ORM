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
            if(pageInfo == null)
            {
                throw new ArgumentNullException(nameof(pageInfo));
            }

            var records = DbSet
                .Include(post => post.User)
                .Include(post => post.PostTag)
                    .ThenInclude(postTag => postTag.Tag);

            var recordsPage = GetPageOfRecords(records, pageInfo);
                
            var result = await recordsPage.ToListAsync();

            return result;
        }

        public async Task<List<Post>> GetPostsByTagAsync(PageInfo pageInfo, string tag)
        {
            if (pageInfo == null)
            {
                throw new ArgumentNullException(nameof(pageInfo));
            }

            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            var records = DbSet
                .Include(post => post.User)
                .Include(post => post.PostTag)
                    .ThenInclude(postTag => postTag.Tag)
                .Where(post => post
                    .PostTag
                    .Where(postTag => postTag.Tag.Name == tag)
                    .Any()
                );

            var recordsPage = GetPageOfRecords(records, pageInfo);

            var result = await recordsPage.ToListAsync();

            return result;
        }

        public async Task<List<Post>> GetPostsBySearchAsync(PageInfo pageInfo, string header)
        {
            if (pageInfo == null)
            {
                throw new ArgumentNullException(nameof(pageInfo));
            }

            if (header == null)
            {
                throw new ArgumentNullException(nameof(header));
            }

            var records = DbSet
               .Include(post => post.User)
               .Include(post => post.PostTag)
                   .ThenInclude(postTag => postTag.Tag)
               .Where(post => post
                   .Header
                   .Contains(header)
                );

            var recordsPage = GetPageOfRecords(records, pageInfo);

            var result = await recordsPage.ToListAsync();

            return result;
        }

        public async Task<List<Post>> GetPostsByCategoryAsync(PageInfo pageInfo, Guid categoryId)
        {
            if (pageInfo == null)
            {
                throw new ArgumentNullException(nameof(pageInfo));
            }

            var records = DbSet
              .Include(post => post.User)
              .Include(post => post.PostTag)
                  .ThenInclude(postTag => postTag.Tag)
              .Where(post => post.CategoryId == categoryId);

            var recordsPage = GetPageOfRecords(records, pageInfo);

            var result = await recordsPage.ToListAsync();

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
