using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Models.QueriesModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Interfaces.Repositories
{
    public interface IQueriesRepository
    {
        Task DeleteAllUsersInMonth(Guid id, DateTime date);

        Task<User> GetUserByEmail(string email);

        Task<List<UserIdEmailName>> GetSortedUsers(Expression<Func<UserIdEmailName, string>> expression);

        Task<List<UserBalance>> GetUsersBalances();

        Task<List<UserAsset>> GetUserAssets(Guid id);

        Task<List<UserTransaction>> GetUserTransactions(Guid id);

        Task<IEnumerable<TotalAmountForDate>> GetTotalAmount(Guid id, DateTime startDate, DateTime endDate);

        Task<IEnumerable<AmountOfCategories>> GetTotalAmountOfCategories(Guid id, int operationTypeId, DateTime date);
    }
}
