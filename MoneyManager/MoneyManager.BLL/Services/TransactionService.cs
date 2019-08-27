using AutoMapper;
using MoneyManager.BLL.Interfaces.Models.Transaction;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        private readonly IAssetRepository _assetRepository;

        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IAssetRepository assetRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _assetRepository = assetRepository;
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

            if (result.IsCategoryExists && result.IsAssetExists && result.IsAmountPositive)
            {
                item.Id = Guid.NewGuid();
                var convertedItem = _mapper.Map<Transaction, DAL.Interfaces.Models.Transaction>(item);
                await _transactionRepository.CreateAsync(convertedItem);

                var asset = await _assetRepository.GetByIdAsync(item.AssetId);
                asset.CurrentBalance += item.Amount;
                await _assetRepository.UpdateAsync(asset);
            }

            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            var asset = await _assetRepository.GetByIdAsync(transaction.AssetId);
            asset.CurrentBalance -= transaction.Amount;
            await _assetRepository.UpdateAsync(asset);

            await _transactionRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            var items = await _transactionRepository.GetAllAsync();
            var convertedItems = _mapper.Map<IEnumerable<DAL.Interfaces.Models.Transaction>, IEnumerable<Transaction>>(items);

            return convertedItems;
        }

        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            var item = await _transactionRepository.GetByIdAsync(id);
            var convertedItem = _mapper.Map<DAL.Interfaces.Models.Transaction, Transaction>(item);

            return convertedItem;
        }

        public bool IsAmountPositive(decimal amount)
        {
            return amount > 0;
        }

        public async Task<bool> IsAssetExistsAsync(Guid id)
        {
            return await _transactionRepository.IsAssetExistsAsync(id);
        }

        public async Task<bool> IsCategoryExistsAsync(Guid id)
        {
            return await _transactionRepository.IsCategoryExistsAsync(id);
        }

        public async Task<UpdateTransactionResult> UpdateAsync(Transaction item)
        {
            var result = new UpdateTransactionResult
            {
                IsAssetExists = await IsAssetExistsAsync(item.AssetId),
                IsCategoryExists = await IsCategoryExistsAsync(item.CategoryId),
                IsAmountPositive = IsAmountPositive(item.Amount)
            };

            if (result.IsCategoryExists && result.IsAssetExists && result.IsAmountPositive)
            {
                var transaction = await _transactionRepository.GetByIdAsync(item.Id);
                if(transaction.AssetId != item.AssetId)
                {
                    var oldAsset = await _assetRepository.GetByIdAsync(transaction.AssetId);
                    oldAsset.CurrentBalance -= transaction.Amount;
                    await _assetRepository.UpdateAsync(oldAsset);

                    var newAsset = await _assetRepository.GetByIdAsync(item.AssetId);
                    newAsset.CurrentBalance += item.Amount;
                    await _assetRepository.UpdateAsync(newAsset);
                }
                else
                {
                    var asset = await _assetRepository.GetByIdAsync(transaction.AssetId);
                    asset.CurrentBalance -= transaction.Amount;
                    asset.CurrentBalance += item.Amount;
                    await _assetRepository.UpdateAsync(asset);
                }

                var convertedItem = _mapper.Map<Transaction, DAL.Interfaces.Models.Transaction>(item);
                await _transactionRepository.UpdateAsync(convertedItem);
            }

            return result;
        }
    }
}
