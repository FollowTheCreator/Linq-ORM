using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.TagModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetRecordsAsync(PageInfo pageInfo);

        Task<Tag> GetByIdAsync(Guid id);

        Task CreateAsync(Tag item);

        Task UpdateAsync(Tag item);

        Task DeleteAsync(Guid id);

        Task<bool> IsTagExistsAsync(Guid id);

        Task<List<string>> GetPostTagsAsync(Guid postId);

        Task<List<string>> GetTagsAsync();
    }
}
