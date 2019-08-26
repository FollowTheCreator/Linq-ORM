using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Models.QueriesModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Interfaces.Repositories.QueriesRepository
{
    public interface IQueriesRepository
    {
        Task DeleteAllUsersInCurrentMonth(Guid id);

        Task<User> GetUserByEmail(string email);

        Task<List<UserIdEmailName>> GetSortedUsers();

        Task<List<UserBalance>> GetUsersBalances();

        Task<List<UserAsset>> GetUserAssets(Guid id);

        Task<List<UserTransaction>> GetUserTransactions(Guid id);

        Task<IEnumerable<TotalAmountForDate>> GetTotalAmount(Guid id, DateTime startDate, DateTime endDate);

        Task<IEnumerable<AmountOfParents>> GetTotalAmountOfParents(Guid id, int operationTypeId);
    }
}
