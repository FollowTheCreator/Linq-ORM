using ShareMe.DAL.Interfaces.Models.TagModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.DAL.Interfaces.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<List<string>> GetPostTagsAsync(Guid postId);

        Task<List<string>> GetTagsAsync();

        Task<Tag> GetByNameAsync(string name);
    }
}
