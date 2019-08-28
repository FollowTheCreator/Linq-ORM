using MoneyManager.BLL.Interfaces.Models;
using MoneyManager.BLL.Interfaces.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserViewModel> GetRecordsAsync(PageInfo pageInfo);

        Task<User> GetByIdAsync(Guid id);

        Task<CreateUserResult> CreateAsync(CreateUserModel item);

        Task<UpdateUserResult> UpdateAsync(UpdateUserModel item);

        Task DeleteAsync(Guid id);

        Task<bool> IsEmailExistsAsync(string email);

        Task<string> GetSaltByIdAsync(Guid id);

        Task<bool> IsUserExistsAsync(Guid id);
    }
}
