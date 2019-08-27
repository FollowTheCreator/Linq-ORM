using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Repositories;
using MoneyManager.DAL.Models.Contexts;
using System;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Repositories
{
    public class TransactionRepository : Repository<Transaction, Guid>, ITransactionRepository
    {
        private readonly MoneyManagerContext _context;

        public TransactionRepository(MoneyManagerContext context)
            :base(context)
        {
            _context = context;
        }

        public async Task<bool> IsAssetExistsAsync(Guid id)
        {
            var result = await _context.Asset.FindAsync(id);

            return result != null;
        }

        public async Task<bool> IsCategoryExistsAsync(Guid id)
        {
            var result = await _context.Category.FindAsync(id);

            return result != null;
        }
    }
}
