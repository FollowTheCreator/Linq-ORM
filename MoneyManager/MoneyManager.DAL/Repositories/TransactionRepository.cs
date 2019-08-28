using Microsoft.EntityFrameworkCore;
using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Models.QueriesModels;
using MoneyManager.DAL.Interfaces.Repositories;
using MoneyManager.DAL.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<TotalAmountForDate>> GetTotalAmount(Guid id, DateTime startDate, DateTime endDate)
        {
            var groupedRecords = await _context
                .Transaction
                .AsNoTracking()
                .Include(transaction => transaction.Category)
                .Include(transaction => transaction.Asset)
                .ThenInclude(asset => asset.User)
                .Where(transaction =>
                    transaction.Asset.User.Id == id &&
                    transaction.Date > startDate &&
                    transaction.Date < endDate
                )
                .GroupBy(transaction =>
                    new
                    {
                        transaction.Date.Month,
                        transaction.Date.Year
                    }
                )
                .ToListAsync();

            var result = (await Task.WhenAll(groupedRecords
                    .Select(async transaction =>
                        new TotalAmountForDate
                        {
                            Month = transaction.Key.Month,
                            Year = transaction.Key.Year,
                            TotalValue = await _context
                                .Transaction
                                .AsNoTracking()
                                .Where(item =>
                                    item.Date.Month == transaction.Key.Month &&
                                    item.Date.Year == transaction.Key.Year
                                )
                                .Select(item => item.Amount)
                                .SumAsync()
                        }
                    )
                ))
                .OrderBy(a => a.Year)
                .ThenBy(a => a.Month)
                .ToList();

            return result;
        }

        public async Task<List<UserTransaction>> GetUserTransactions(Guid id)
        {
            var result = await _context
                .Transaction
                .AsNoTracking()
                .Include(transaction => transaction.Category)
                .Include(transaction => transaction.Asset)
                .ThenInclude(asset => asset.User)
                .Where(transaction => transaction.Asset.User.Id == id)
                .Select(transaction =>
                    new UserTransaction
                    {
                        AssetName = transaction.Asset.Name,
                        TransactionSubcategory = transaction.Category.Name,
                        TransactionAmount = transaction.Amount,
                        TransactionDate = transaction.Date,
                        TransactionComment = transaction.Comment
                    }
                )
                .OrderByDescending(a => a.TransactionDate)
                .ThenBy(a => a.AssetName)
                .ThenBy(a => a.TransactionSubcategory)
                .ToListAsync();

            return result;
        }
    }
}
