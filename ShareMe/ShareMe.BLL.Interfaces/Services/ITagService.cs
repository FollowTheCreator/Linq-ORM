using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.TagModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface ITagService
    {
        Task<Tag> GetByNameAsync(string name);

        Task<Tag> GetOrCreateAsync(string name);

        Task<List<string>> GetPostTagsAsync(Guid postId);

        Task<List<string>> GetTagsAsync(PageInfo pageInfo);

        Task<Tag> CreateAsync(string name);
    }
}
