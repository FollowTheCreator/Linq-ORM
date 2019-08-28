using AutoMapper;
using MoneyManager.BLL.Interfaces.Models;
using MoneyManager.BLL.Interfaces.Models.Transaction;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.DAL.Interfaces.Repositories;
using MoneyManager.DAL.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly MoneyManagerContext _context;

        private readonly ITransactionRepository _transactionRepository;

        private readonly IAssetService _assetService;
        private readonly ICategoryService _categoryService;
        private readonly IConfigService _configService;

        private readonly IMapper _mapper;

        public TransactionService
        (
            MoneyManagerContext context,
            ITransactionRepository transactionRepository, 
            IAssetService assetService, 
            ICategoryService categoryService, 
            IConfigService configService,
            IMapper mapper
        )
        {
            _context = context;

            _transactionRepository = transactionRepository;

            _assetService = assetService;
            _categoryService = categoryService;
            _configService = configService;

            _mapper = mapper;
        }

        public async Task<CreateTransactionResult> CreateAsync(Transaction item)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var result = new CreateTransactionResult
                {
                    IsTransactionAssetExists = await _assetService.IsAssetExistsAsync(item.AssetId),
                    IsTransactionCategoryExists = await _categoryService.IsCategoryExistsAsync(item.CategoryId),
                    IsTransactionAmountPositive = IsAmountPositive(item.Amount)
                };

                try
                {
                    if (result.IsTransactionCategoryExists && result.IsTransactionAssetExists && result.IsTransactionAmountPositive)
                    {
                        item.Id = Guid.NewGuid();
                        var convertedItem = _mapper.Map<Transaction, DAL.Interfaces.Models.Transaction>(item);
                        await _transactionRepository.CreateAsync(convertedItem);

                        var asset = await _assetService.GetByIdAsync(item.AssetId);
                        asset.CurrentBalance += item.Amount;
                        await _assetService.UpdateAsync(asset);
                    }

                    transaction.Commit();

                    return result;
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    return result;
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var dbTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var transaction = await _transactionRepository.GetByIdAsync(id);
                    var asset = await _assetService.GetByIdAsync(transaction.AssetId);
                    asset.CurrentBalance -= transaction.Amount;
                    await _assetService.UpdateAsync(asset);

                    await _transactionRepository.DeleteAsync(id);

                    dbTransaction.Commit();
                }
                catch (Exception e)
                {
                    dbTransaction.Rollback();
                }
            }
        }

        public async Task<TransactionViewModel> GetRecordsAsync(PageInfo pageInfo)
        {
            pageInfo.CheckPageInfo(_configService.GetPageSize());

            var convertedPageInfo = _mapper.Map<PageInfo, DAL.Interfaces.Models.PageInfo>(pageInfo);
            var items = await _transactionRepository.GetRecordsAsync(convertedPageInfo);
            var convertedItems = _mapper.Map<IEnumerable<DAL.Interfaces.Models.Transaction>, IEnumerable<Transaction>>(items);

            pageInfo.TotalItems = await _transactionRepository.RecordsCountAsync();
            pageInfo.TotalPages = (int)Math.Ceiling(pageInfo.TotalItems / (double)pageInfo.PageSize);

            return new TransactionViewModel
            {
                Transactions = convertedItems,
                PageInfo = pageInfo
            };
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

        public async Task<UpdateTransactionResult> UpdateAsync(Transaction item)
        {
            using (var dbTransaction = await _context.Database.BeginTransactionAsync())
            {
                var result = new UpdateTransactionResult
                {
                    IsTransactionAssetExists = await _assetService.IsAssetExistsAsync(item.AssetId),
                    IsTransactionCategoryExists = await _categoryService.IsCategoryExistsAsync(item.CategoryId),
                    IsTransactionAmountPositive = IsAmountPositive(item.Amount),
                    IsTransactionExists = await IsTransactionExistsAsync(item.Id)
                };

                try
                {
                    if (result.IsTransactionExists && result.IsTransactionCategoryExists && result.IsTransactionAssetExists && result.IsTransactionAmountPositive)
                    {
                        var transaction = await _transactionRepository.GetByIdAsync(item.Id);
                        if (transaction.AssetId != item.AssetId)
                        {
                            var oldAsset = await _assetService.GetByIdAsync(transaction.AssetId);
                            oldAsset.CurrentBalance -= transaction.Amount;
                            await _assetService.UpdateAsync(oldAsset);

                            var newAsset = await _assetService.GetByIdAsync(item.AssetId);
                            newAsset.CurrentBalance += item.Amount;
                            await _assetService.UpdateAsync(newAsset);
                        }
                        else
                        {
                            var asset = await _assetService.GetByIdAsync(transaction.AssetId);
                            asset.CurrentBalance -= transaction.Amount;
                            asset.CurrentBalance += item.Amount;
                            await _assetService.UpdateAsync(asset);
                        }

                        var convertedItem = _mapper.Map<Transaction, DAL.Interfaces.Models.Transaction>(item);
                        await _transactionRepository.UpdateAsync(convertedItem);
                    }

                    dbTransaction.Commit();

                    return result;
                }
                catch (Exception e)
                {
                    dbTransaction.Rollback();

                    return result;
                }
            }
        }

        public async Task<bool> IsTransactionExistsAsync(Guid id)
        {
            var result = await _transactionRepository.GetByIdAsync(id);

            return result != null;
        }
    }
}
