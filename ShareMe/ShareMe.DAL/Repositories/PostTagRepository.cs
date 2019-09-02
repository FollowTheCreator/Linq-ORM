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
        public PostTagRepository(ShareMeContext context)
            : base(context)
        { }

        public async Task<List<PostTag>> GetPostTagsByPostId(Guid postId)
        {
            return await DbSet
                .AsNoTracking()
                .Where(postTag => postTag.PostId == postId)
                .ToListAsync();
        }
    }
}
