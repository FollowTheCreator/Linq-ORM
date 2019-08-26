using MoneyManager.BLL.Interfaces.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Interfaces.Services.TransactionService
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllAsync();

        Task<Transaction> GetByIdAsync(Guid id);

        Task<CreateTransactionResult> CreateAsync(Transaction item);

        Task<UpdateTransactionResult> UpdateAsync(Transaction item);

        Task DeleteAsync(Guid id);

        Task<bool> IsAssetExistsAsync(Guid id);

        Task<bool> IsCategoryExistsAsync(Guid id);

        bool IsAmountPositive(decimal amount);
    }
}
