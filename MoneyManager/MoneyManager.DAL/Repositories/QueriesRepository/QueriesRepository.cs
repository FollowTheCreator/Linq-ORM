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
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AmountOfParents>> GetTotalAmountOfParents(Guid id, int operationTypeId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
