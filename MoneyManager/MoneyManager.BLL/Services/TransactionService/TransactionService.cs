using AutoMapper;
using MoneyManager.BLL.Interfaces.Models.Transaction;
using MoneyManager.BLL.Interfaces.Services.TransactionService;
using MoneyManager.DAL.Interfaces.Repositories.TransactionRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Services.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateTransactionResult> CreateAsync(Transaction item)
        {
            var result = new CreateTransactionResult
            {
                IsAssetExists = await IsAssetExistsAsync(item.AssetId),
                IsCategoryExists = await IsCategoryExistsAsync(item.CategoryId),
                IsAmountPositive = IsAmountPositive(item.Amount)
            };

            if (result.IsCategoryExists && result.IsAssetExists)
            {
                item.Id = Guid.NewGuid();
                var convertedItem = _mapper.Map<Transaction, DAL.Interfaces.Models.Transaction>(item);
                await _repository.CreateAsync(convertedItem);
            }

            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            var convertedItems = _mapper.Map<IEnumerable<DAL.Interfaces.Models.Transaction>, IEnumerable<Transaction>>(items);

            return convertedItems;
        }

        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var convertedItem = _mapper.Map<DAL.Interfaces.Models.Transaction, Transaction>(item);

            return convertedItem;
        }

        public bool IsAmountPositive(decimal amount)
        {
            return amount > 0;
        }

        public async Task<bool> IsAssetExistsAsync(Guid id)
        {
            return await _repository.IsAssetExistsAsync(id);
        }

        public async Task<bool> IsCategoryExistsAsync(Guid id)
        {
            return await _repository.IsCategoryExistsAsync(id);
        }

        public async Task<UpdateTransactionResult> UpdateAsync(Transaction item)
        {
            var result = new UpdateTransactionResult
            {
                IsAssetExists = await IsAssetExistsAsync(item.AssetId),
                IsCategoryExists = await IsCategoryExistsAsync(item.CategoryId),
                IsAmountPositive = IsAmountPositive(item.Amount)
            };

            if (result.IsCategoryExists && result.IsAssetExists)
            {
                var convertedItem = _mapper.Map<Transaction, DAL.Interfaces.Models.Transaction>(item);
                await _repository.UpdateAsync(convertedItem);
            }

            return result;
        }
    }
}
