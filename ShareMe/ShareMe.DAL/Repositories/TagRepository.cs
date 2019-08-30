using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models;
using ShareMe.DAL.Interfaces.Models.TagModels;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
