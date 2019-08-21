using MoneyManager.DAL.Interfaces.Models;
using System;

namespace MoneyManager.DAL.Interfaces.Repositories.TransactionRepository
{
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    { }
}
