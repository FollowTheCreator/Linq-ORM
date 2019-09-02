using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models.TagModels;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.DAL.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        private readonly ShareMeContext _context;

        public TagRepository(ShareMeContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Tag> GetByNameAsync(string name)
        {
            var result = await DbSet
                .FirstOrDefaultAsync(tag => tag.Name == name);

            return result;
        }

        public async Task<List<Guid>> GetPostTagIdsAsync(Guid postId)
        {
            var result = await _context
                .PostTag
                .Include(postTag => postTag.Tag)
                .Where(postTag => postTag.PostId == postId)
                .Select(postTag => postTag.Tag.Id)
                .ToListAsync();

            return result;
        }

        public async Task<List<string>> GetPostTagsAsync(Guid postId)
        {
            var result = await _context
                .PostTag
                .Include(postTag => postTag.Tag)
                .Where(postTag => postTag.PostId == postId)
                .Select(postTag => postTag.Tag.Name)
                .ToListAsync();

            return result;
        }

        public async Task<List<string>> GetTagsAsync()
        {
            var result = await DbSet
                .Select(tag => tag.Name)
                .ToListAsync();

            return result;
        }
    }
}
