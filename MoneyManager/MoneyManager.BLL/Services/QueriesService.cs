using AutoMapper;
using MoneyManager.BLL.Interfaces.Models.QueriesModels;
using MoneyManager.BLL.Interfaces.Models.User;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Services
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
            await _repository.DeleteAllUsersInMonth(id, DateTime.Now);
        }

        public async Task<List<UserIdEmailName>> GetSortedUsers()
        {
            var result = await _repository.GetSortedUsers(a => a.Name);

            var convertedResult = _mapper.Map<List<DAL.Interfaces.Models.QueriesModels.UserIdEmailName>, List<UserIdEmailName>>(result);

            return convertedResult;
        }

        public async Task<IEnumerable<TotalAmountForDate>> GetTotalAmount(Guid id, DateTime startDate, DateTime endDate)
        {
            var result = await _repository.GetTotalAmount(id, startDate, endDate);

            var convertedResult = _mapper.Map<IEnumerable<DAL.Interfaces.Models.QueriesModels.TotalAmountForDate>, IEnumerable<TotalAmountForDate>>(result);

            return convertedResult;
        }

        public async Task<IEnumerable<AmountOfCategories>> GetTotalAmountOfCategories(Guid id, int operationTypeId)
        {
            var result = await _repository.GetTotalAmountOfCategories(id, operationTypeId, DateTime.Now);

            var convertedResult = _mapper.Map<IEnumerable<DAL.Interfaces.Models.QueriesModels.AmountOfCategories>, IEnumerable<AmountOfCategories>>(result);

            return convertedResult;
        }

        public async Task<List<UserAsset>> GetUserAssets(Guid id)
        {
            var result = await _repository.GetUserAssets(id);

            var convertedResult = _mapper.Map<List<DAL.Interfaces.Models.QueriesModels.UserAsset>, List<UserAsset>>(result);

            return convertedResult;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var result = await _repository.GetUserByEmail(email);

            var convertedResult = _mapper.Map<DAL.Interfaces.Models.User, User>(result);

            return convertedResult;
        }

        public async Task<List<UserBalance>> GetUsersBalances()
        {
            var result = await _repository.GetUsersBalances();

            var convertedResult = _mapper.Map<List<DAL.Interfaces.Models.QueriesModels.UserBalance>, List<UserBalance>>(result);

            return convertedResult;
        }

        public async Task<List<UserTransaction>> GetUserTransactions(Guid id)
        {
            var result = await _repository.GetUserTransactions(id);

            var convertedResult = _mapper.Map<List<DAL.Interfaces.Models.QueriesModels.UserTransaction>, List<UserTransaction>>(result);

            return convertedResult;
        }
    }
}
