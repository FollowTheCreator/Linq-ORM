using MoneyManager.DAL.Interfaces.Models;
using System;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Interfaces.Repositories.UserRepository
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<bool> IsEmailExistsAsync(string email);

        Task<string> GetSaltByIdAsync(Guid id);
    }
}
