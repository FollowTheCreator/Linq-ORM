using MoneyManager.DAL.Interfaces.Models;
using System;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Interfaces.Repositories.TransactionRepository
{
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
        Task<bool> IsCategoryExistsAsync(Guid id);

        Task<bool> IsAssetExistsAsync(Guid id);
    }
}
