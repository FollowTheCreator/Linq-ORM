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
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        private readonly MoneyManagerContext _context;

        public CategoryRepository(MoneyManagerContext context)
            :base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AmountOfCategories>> GetTotalAmountOfCategories(Guid id, int operationTypeId, DateTime date)
        {
            var groupedRecords = await _context
                .Transaction
                .AsNoTracking()
                .Include(transaction => transaction.Category)
                .ThenInclude(category => category.TypeNavigation)
                .Include(transaction => transaction.Asset)
                .ThenInclude(asset => asset.User)
                .Where(transaction =>
                    transaction.Asset.User.Id == id &&
                    transaction.Category.TypeNavigation.Id == operationTypeId &&
                    transaction.Date.Month == date.Month
                )
                .GroupBy(transaction =>
                    new
                    {
                        transaction.Category.Name
                    }
                )
                .ToListAsync();

            var result = (await Task.WhenAll(groupedRecords
                    .Select(async transaction =>
                        new AmountOfCategories
                        {
                            Name = transaction.Key.Name,
                            Amount = await _context.Transaction.AsNoTracking()
                                .Include(item => item.Category)
                                .ThenInclude(category => category.TypeNavigation)
                                .Include(item => item.Asset)
                                .ThenInclude(asset => asset.User)
                                .Where(item =>
                                    item.Category.Name == transaction.Key.Name &&
                                    item.Asset.User.Id == id &&
                                    item.Category.TypeNavigation.Id == operationTypeId &&
                                    item.Date.Month == date.Month
                                )
                                .Select(item => item.Amount)
                                .SumAsync()
                        }
                    )
                ))
                .OrderByDescending(a => a.Amount)
                .ThenBy(a => a.Name)
                .ToList();

            return result;
        }
    }
}
