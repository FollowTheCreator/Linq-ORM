using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models.CommentModels;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.DAL.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly ShareMeContext _context;

        public CommentRepository(ShareMeContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task DeleteByPostIdAsync(Guid postId)
        {
            var comments = _context
                .Comment
                .AsNoTracking()
                .Where(comment => comment.PostId == postId);

            DbSet.RemoveRange(comments);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetChildrenAsync(Guid id)
        {
            var result = await DbSet
                .AsNoTracking()
                .Where(comment => comment.ParentId == id)
                .ToListAsync();

            return result;
        }
    }
}
