using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.PostTag;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface IPostTagService
    {
        Task<IEnumerable<PostTag>> GetRecordsAsync(PageInfo pageInfo);

        Task<PostTag> GetByIdAsync(Guid id);

        Task CreateAsync(PostTag item);

        Task UpdateAsync(PostTag item);

        Task DeleteAsync(Guid id);

        Task<bool> IsPostTagExistsAsync(Guid id);
    }
}
