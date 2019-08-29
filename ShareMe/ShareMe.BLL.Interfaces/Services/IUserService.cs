using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetRecordsAsync(PageInfo pageInfo);

        Task<User> GetByIdAsync(Guid id);

        Task CreateAsync(User item);

        Task UpdateAsync(User item);

        Task DeleteAsync(Guid id);

        Task<bool> IsEmailExistsAsync(string email);

        Task<bool> IsUserExistsAsync(Guid id);
    }
}
