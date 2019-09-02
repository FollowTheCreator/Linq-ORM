using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models.PostTagModels;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.DAL.Repositories
{
    public class PostTagRepository : Repository<PostTag>, IPostTagRepository
    {
        private readonly ShareMeContext _context;

        public PostTagRepository(ShareMeContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task DeleteByPostIdAsync(Guid postId)
        {
            var postTags = _context
                .PostTag
                .AsNoTracking()
                .Where(postTag => postTag.PostId == postId);

            DbSet.RemoveRange(postTags);
            await _context.SaveChangesAsync();
        }
    }
}
