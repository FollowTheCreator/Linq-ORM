using MoneyManager.BLL.Interfaces.Models;
using MoneyManager.BLL.Interfaces.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<TransactionViewModel> GetRecordsAsync(PageInfo pageInfo);

        Task<Transaction> GetByIdAsync(Guid id);

        Task<CreateTransactionResult> CreateAsync(Transaction item);

        Task<UpdateTransactionResult> UpdateAsync(Transaction item);

        Task DeleteAsync(Guid id);

        Task<bool> IsTransactionExistsAsync(Guid id);

        bool IsAmountPositive(decimal amount);
    }
}
