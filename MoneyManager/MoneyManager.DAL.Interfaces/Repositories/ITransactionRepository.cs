using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Models.QueriesModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Interfaces.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
        Task<IEnumerable<TotalAmountForDate>> GetTotalAmount(Guid id, DateTime startDate, DateTime endDate);

        Task<List<UserTransaction>> GetUserTransactions(Guid id);
    }
}
