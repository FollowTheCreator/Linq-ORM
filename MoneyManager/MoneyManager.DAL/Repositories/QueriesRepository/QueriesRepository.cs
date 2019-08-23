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
            var result = await _context.User.Select(user => 
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
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserBalance>> GetUsersBalances()
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserTransaction>> GetUserTransactions(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
