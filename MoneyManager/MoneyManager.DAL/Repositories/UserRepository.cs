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
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        private readonly MoneyManagerContext _context;

        public UserRepository(MoneyManagerContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            User user = await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            return user != null;
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

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context
                .User
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email == email);
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
                        Balance = await _context
                            .Asset
                            .AsNoTracking()
                            .Where(asset => asset.UserId == user.Key.Id)
                            .Select(asset => asset.CurrentBalance)
                            .SumAsync()
                    }
                )
                ))
                .ToList();

            return result;
        }
    }
}
