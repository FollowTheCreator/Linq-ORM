using MoneyManager.BLL.Interfaces.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Interfaces.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetByIdAsync(Guid id);

        Task<CreateUserResult> CreateAsync(CreateUserModel item);

        Task UpdateAsync(UpdateUserModel item);

        Task DeleteAsync(Guid id);

        Task<bool> IsEmailExistsAsync(string email);
    }
}
