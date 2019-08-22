using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Repositories.TransactionRepository;
using MoneyManager.DAL.Models.Contexts;
using System;

namespace MoneyManager.DAL.Repositories.TransactionRepository
{
    public class TransactionRepository : Repository<Transaction, Guid>, ITransactionRepository
    {
        public TransactionRepository(MoneyManagerContext context)
            :base(context)
        { }
    }
}
