using ShareMe.DAL.Interfaces.Models.PostTagModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.DAL.Interfaces.Repositories
{
    public interface IPostTagRepository : IRepository<PostTag>
    {
        Task DeleteByPostIdAsync(Guid postId);
    }
}
