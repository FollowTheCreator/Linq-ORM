using Microsoft.EntityFrameworkCore;
using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Models.QueriesModels;
using MoneyManager.DAL.Interfaces.Repositories;
using MoneyManager.DAL.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Repositories
{
    public class QueriesRepository : IQueriesRepository
    {
        private readonly MoneyManagerContext _context;

        public QueriesRepository(MoneyManagerContext context)
        {
            _context = context;
        }

        public async Task DeleteAllUsersInMonth(Guid id, DateTime date)
        {
            var transactionIds = await _context
                .Transaction
                .AsNoTracking()
                .Include(transaction => transaction.Asset)
                .ThenInclude(asset => asset.User)
                .Where(transaction =>
                    transaction.Date.Month == date.Month &&
                    transaction.Asset.User.Id == id
                )
                .Select(transaction => transaction.Id)
                .ToListAsync();
        }

        public async Task<List<UserIdEmailName>> GetSortedUsers(Expression<Func<UserIdEmailName, string>> expression)
        {
            var result = await _context
                .User
                .AsNoTracking()
                .Select(user => 
                    new UserIdEmailName
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email
                    }
                )
                .OrderBy(expression)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TotalAmountForDate>> GetTotalAmount(Guid id, DateTime startDate, DateTime endDate)
        {
            var groupedRecords = await _context
                .Transaction
                .AsNoTracking()
                .Include(transaction => transaction.Category)
                .Include(transaction => transaction.Asset)
                .ThenInclude(asset => asset.User)
                .Where(transaction => transaction.Asset.User.Id == id && transaction.Date > startDate && transaction.Date < endDate)
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
                        TotalValue = await _context.Transaction.AsNoTracking().Where(item => 
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

        public async Task<List<UserAsset>> GetUserAssets(Guid id)
        {
            var groupedRecords = await _context
                .Asset
                .AsNoTracking()
                .Include(asset => asset.User)
                .Include(asset => asset.Transaction)
                .Where(asset => asset.UserId == id)
                .GroupBy(asset =>
                    new
                    {
                        asset.Id,
                        asset.Name,
                        asset.CurrentBalance
                    }
                )
                .ToListAsync();

            //var result = (await Task.WhenAll(groupedRecords
            //    .Select(async asset =>
            //        new UserAsset
            //        {
            //            Id = asset.Key.Id,
            //            Name = asset.Key.Name,
            //            Balance = await _context.Asset.AsNoTracking().Where(item => item.Id == asset.Key.Id)
            //                .SelectMany(item => item.Transaction)
            //                .Select(transaction => transaction.Amount)
            //                .SumAsync()
            //        }
            //    )
            var result = (await Task.WhenAll(groupedRecords
               .Select(async asset =>
                   new UserAsset
                   {
                       Id = asset.Key.Id,
                       Name = asset.Key.Name,
                       Balance = asset.Key.CurrentBalance
                   }
               )
               ))
                .OrderBy(a => a.Name)
                .ToList();

            return result;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context
                .User
                .AsNoTracking()
                .Where(user => user.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<List<UserBalance>> GetUsersBalances()
        {
            var groupedRecords = await _context
                .User
                .AsNoTracking()
                .Include(user => user.Asset)
                .ThenInclude(asset => asset.Transaction)
                .GroupBy(user =>
                    new
                    {
                        user.Id,
                        user.Email,
                        user.Name
                    }
                )
                .ToListAsync();

            var result = (await Task.WhenAll(groupedRecords
                .Select(async user =>
                    new UserBalance
                    {
                        Id = user.Key.Id,
                        Email = user.Key.Email,
                        Name = user.Key.Name,
                        Balance = await _context.Asset.AsNoTracking().Where(asset => asset.UserId == user.Key.Id)
                            .Select(asset => asset.CurrentBalance)
                            .SumAsync()
                    }
                )
                ))
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
