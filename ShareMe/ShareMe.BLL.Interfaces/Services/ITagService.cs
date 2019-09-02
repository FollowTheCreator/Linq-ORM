using ShareMe.BLL.Interfaces.Models.TagModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface ITagService
    {
        Task<Tag> GetByNameAsync(string name);

        Task<Tag> CreateIfExistsAsync(string name);

        Task<bool> IsTagExistsAsync(Guid id);

        Task<List<string>> GetPostTagsAsync(Guid postId);

        Task<List<string>> GetTagsAsync();
    }
}
