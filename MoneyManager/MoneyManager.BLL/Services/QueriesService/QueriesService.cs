using AutoMapper;
using MoneyManager.BLL.Interfaces.Models.QueriesModels;
using MoneyManager.BLL.Interfaces.Models.User;
using MoneyManager.BLL.Interfaces.Services.QueriesService;
using MoneyManager.DAL.Interfaces.Repositories.QueriesRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Services.QueriesService
{
    public class QueriesService : IQueriesService
    {
        private readonly IQueriesRepository _repository;

        private readonly IMapper _mapper;

        public QueriesService(IQueriesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task DeleteAllUsersInCurrentMonth(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserIdEmailName>> GetSortedUsers()
        {
            var result = await _repository.GetSortedUsers();

            var convertedResult = _mapper.Map<List<DAL.Interfaces.Models.QueriesModels.UserIdEmailName>, List<UserIdEmailName>>(result);

            return convertedResult;
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
