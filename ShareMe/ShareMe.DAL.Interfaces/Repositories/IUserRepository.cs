using ShareMe.DAL.Interfaces.Models.UserModels;
using System;
using System.Threading.Tasks;

namespace ShareMe.DAL.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> IsEmailExistsAsync(string email);

        Task<UserViewModel> GetUserAsync(Guid id);
    }
}
