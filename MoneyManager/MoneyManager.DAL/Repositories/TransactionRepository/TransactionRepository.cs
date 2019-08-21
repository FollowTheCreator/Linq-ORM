using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Repositories.TransactionRepository;
using MoneyManager.DAL.Models.Contexts;

namespace MoneyManager.DAL.Repositories.TransactionRepository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MoneyManagerCodeFirstContext _context;

        public TransactionRepository(MoneyManagerCodeFirstContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Transaction item)
        {
            _context.Transaction.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            _context.Transaction.Remove(await GetByIdAsync(id));

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context
                .Transaction
                .ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            return await _context
                .Transaction
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Transaction item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
