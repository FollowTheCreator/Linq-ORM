using ShareMe.BLL.Interfaces.Models.UserModels;
using System;
using System.Threading.Tasks;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateAsync(User item);

        Task UpdateAsync(User item);

        Task DeleteAsync(Guid id);

        Task<bool> IsEmailExistsAsync(string email);

        Task<UserViewModel> GetUserAsync(Guid id);
    }
}
