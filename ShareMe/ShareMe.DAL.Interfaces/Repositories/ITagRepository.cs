using ShareMe.DAL.Interfaces.Models;
using ShareMe.DAL.Interfaces.Models.TagModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.DAL.Interfaces.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<List<string>> GetPostTagsAsync(Guid postId);

        Task<List<string>> GetTagsAsync(PageInfo pageInfo);

        Task<Tag> GetByNameAsync(string name);
    }
}
