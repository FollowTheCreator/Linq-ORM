using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Models.QueriesModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<bool> IsEmailExistsAsync(string email);

        Task DeleteAllUsersInMonth(Guid id, DateTime date);

        Task<List<UserIdEmailName>> GetSortedUsers(Expression<Func<UserIdEmailName, string>> expression);

        Task<User> GetUserByEmail(string email);

        Task<List<UserBalance>> GetUsersBalances();
    }
}
