using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.Post;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetRecordsAsync(PageInfo pageInfo);

        Task<Post> GetByIdAsync(Guid id);

        Task CreateAsync(Post item);

        Task UpdateAsync(Post item);

        Task DeleteAsync(Guid id);

        Task<bool> IsPostExistsAsync(Guid id);
    }
}
