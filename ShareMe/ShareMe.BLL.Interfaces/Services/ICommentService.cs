using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.Comment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetRecordsAsync(PageInfo pageInfo);

        Task<Comment> GetByIdAsync(Guid id);

        Task CreateAsync(Comment item);

        Task UpdateAsync(Comment item);

        Task DeleteAsync(Guid id);

        Task<bool> IsCommentExistsAsync(Guid id);
    }
}
