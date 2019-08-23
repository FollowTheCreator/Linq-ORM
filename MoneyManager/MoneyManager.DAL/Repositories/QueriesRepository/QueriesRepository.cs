using Microsoft.EntityFrameworkCore;
using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Models.QueriesModels;
using MoneyManager.DAL.Interfaces.Repositories.QueriesRepository;
using MoneyManager.DAL.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Repositories.QueriesRepository
{
    public class QueriesRepository : IQueriesRepository
    {
        private readonly MoneyManagerContext _context;

        public QueriesRepository(MoneyManagerContext context)
        {
            _context = context;
        }

        public async Task DeleteAllUsersInCurrentMonth(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserIdEmailName>> GetSortedUsers()
        {
            var result = await _context
                .User
                .Select(user => 
                    new UserIdEmailName
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email
                    }
                )
                .OrderBy(user => user.Name)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TotalAmountForDate>> GetTotalAmount(Guid id, DateTime startDate, DateTime endDate)
        {
            var groupedRecords = await _context
                .Transaction
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

            var result = groupedRecords
                .Select(transaction =>
                    new TotalAmountForDate
                    {
                        Month = transaction.Key.Month,
                        Year = transaction.Key.Year,
                        TotalValue = _context.Transaction.Where(item => 
                                item.Date.Month == transaction.Key.Month &&
                                item.Date.Year == transaction.Key.Year
                            )
                            .Select(item => item.Amount)
                            .Sum()
                    }
                )
                .ToList();

            return result;
        }

        public async Task<IEnumerable<AmountOfParents>> GetTotalAmountOfParents(Guid id, int operationTypeId)
        {
            var groupedRecords = await _context
                .Transaction
                .Include(transaction => transaction.Category)
                .ThenInclude(category => category.TypeNavigation)
                .Include(transaction => transaction.Asset)
                .ThenInclude(asset => asset.User)
                .Where(transaction =>
                    transaction.Asset.User.Id == id && 
                    transaction.Category.TypeNavigation.Id == operationTypeId && 
                    transaction.Date.Month == DateTime.Now.Month &&
                    _context.Category.Where(category => category.ParentId == transaction.Category.Id).Any()
                )
                .GroupBy(transaction =>
                    new
                    {
                        transaction.Category.Name
                    }
                )
                .ToListAsync();

            var result = groupedRecords
                .Select(transaction =>
                    new AmountOfParents
                    {
                        Name = transaction.Key.Name,
                        Amount = _context.Transaction
                            .Include(item => item.Category)
                            .ThenInclude(category => category.TypeNavigation)
                            .Include(item => item.Asset)
                            .ThenInclude(asset => asset.User)
                            .Where(item =>
                                item.Category.Name == transaction.Key.Name &&
                                item.Asset.User.Id == id &&
                                item.Category.TypeNavigation.Id == operationTypeId &&
                                item.Date.Month == DateTime.Now.Month &&
                                _context.Category.Where(category => category.ParentId == item.Category.Id).Any()
                            )
                            .Select(item => item.Amount)
                            .Sum()
                    }
                )
                .OrderByDescending(a => a.Amount)
                .OrderBy(a => a.Name)
                .ToList();

            return result;
        }

        public async Task<List<UserAsset>> GetUserAssets(Guid id)
        {
            var groupedRecords = await _context
                .Asset
                .Include(asset => asset.User)
                .Include(asset => asset.Transaction)
                .Where(asset => asset.UserId == id)
                .GroupBy(asset =>
                    new
                    {
                        asset.Id,
                        asset.Name
                    }
                )
                .ToListAsync();

            var result = groupedRecords
                .Select(asset =>
                    new UserAsset
                    {
                        Id = asset.Key.Id,
                        Name = asset.Key.Name,
                        Balance = _context.Asset.Where(item => item.Id == asset.Key.Id)
                            .SelectMany(item => item.Transaction)
                            .Select(transaction => transaction.Amount)
                            .Sum()
                    }
                )
                .ToList();

            return result;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserBalance>> GetUsersBalances()
        {
            var groupedRecords = await _context
                .User
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

            var result = groupedRecords
                .Select(user =>
                    new UserBalance
                    {
                        Id = user.Key.Id,
                        Email = user.Key.Email,
                        Name = user.Key.Name,
                        Balance = _context.Asset.Where(asset => asset.UserId == user.Key.Id)
                            .SelectMany(asset => asset.Transaction)
                            .Select(transaction => transaction.Amount)
                            .Sum()
                    }
                )
                .ToList();

            return result;
        }

        public async Task<List<UserTransaction>> GetUserTransactions(Guid id)
        {
            //todo 
            throw new NotImplementedException();
        }
    }
}
