using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.DAL.Interfaces.DataSeeding;
using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace MoneyManager.BLL.DataSeeding
{
    public class DataSeeding : IDataSeeding
    {
        private readonly MoneyManagerContext _context;

        private readonly IConfigService _configService;

        private readonly Coder _coder;
        private readonly Generate _generate;

        private readonly Random _random;

        public DataSeeding(MoneyManagerContext context, Coder coder, Generate generate, IConfigService configService)
        {
            _context = context;
            _configService = configService;
            _coder = coder;
            _generate = generate;
            _random = new Random();
        }

        public async Task SeedData()
        {
            if (_context.Database.GetService<IRelationalDatabaseCreator>().Exists())
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var countOfUsers = _configService.GetCountOfUsers();
                        var countOfAssets = _configService.GetCountOfAssets();
                        var countOfCategories = _configService.GetCountOfCategories();
                        var countOfTransactions = _configService.GetCountOfTransactions();

                        var usersId = new List<Guid>(countOfUsers);
                        if (!await _context.User.AnyAsync())
                        {
                            usersId = await SeedUsersAsync(countOfUsers);
                        }
                        else
                        {
                            usersId = await _context.User.Select(user => user.Id).ToListAsync();
                        }

                        var assetsId = new List<Guid>(countOfAssets);
                        if (!await _context.Asset.AnyAsync())
                        {
                            assetsId = await SeedAssetsAsync(countOfAssets);
                        }
                        else
                        {
                            assetsId = await _context.Asset.Select(asset => asset.Id).ToListAsync();
                        }

                        if (!await _context.Type.AnyAsync())
                        {
                            await SeedTypesAsync();
                        }

                        var categories = new List<Category>(countOfCategories);
                        if (!await _context.Category.AnyAsync())
                        {
                            categories = await SeedCategoriesAsync(countOfCategories);
                        }
                        else
                        {
                            categories = await _context.Category.ToListAsync();
                        }

                        if (!await _context.Transaction.AnyAsync())
                        {
                            await SeedTransactionsAsync
                            (
                                categories,
                                assetsId,
                                countOfTransactions,
                                countOfCategories,
                                countOfAssets
                            );
                        }

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();

                        throw new InvalidOperationException($"Transaction completed by Rollback", e);
                    }
                }
            }
        }

        private async Task<List<Guid>> SeedUsersAsync(int countOfUsers)
        {
            var usersId = new List<Guid>(countOfUsers);

            for (int i = 0; i < countOfUsers; i++)
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = _generate.RandomName(),
                    Email = _generate.RandomEmail(),
                    Salt = _generate.RandomSalt()
                };
                user.Hash = _coder.Encode(user.Name + user.Salt);

                usersId.Add(user.Id);

                _context.User.Add(user);
            }

            await _context.SaveChangesAsync();

            return usersId;
        }

        private async Task<List<Guid>> SeedAssetsAsync(int countOfAssets)
        {
            var assetsId = new List<Guid>(countOfAssets);

            for (int i = 0; i < countOfAssets; i++)
            {
                var asset = new Asset
                {
                    Id = Guid.NewGuid(),
                    Name = _generate.RandomName(),
                    UserId = assetsId[i / 2]
                };

                assetsId.Add(asset.Id);

                _context.Asset.Add(asset);
            }

            await _context.SaveChangesAsync();

            return assetsId;
        }

        private async Task SeedTypesAsync()
        {
            _context.Type.Add(new DAL.Interfaces.Models.Type { Name = "income" });
            _context.Type.Add(new DAL.Interfaces.Models.Type { Name = "expence" });

            await _context.SaveChangesAsync();
        }

        private async Task<List<Category>> SeedCategoriesAsync(int countOfCategories)
        {
            var categories = new List<Category>(countOfCategories);

            for (int i = 0; i < countOfCategories; i++)
            {
                var category = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = _generate.RandomName(),
                    Type = _random.Next(1, 3)
                };

                categories.Add(category);
            }
            foreach (var category in categories)
            {
                _context.Category.Add(category);
            }

            await _context.SaveChangesAsync();

            return categories;
        }

        private async Task SeedTransactionsAsync
        (
            List<Category> categories, 
            List<Guid> assetsId, 
            int countOfTransactions, 
            int countOfCategories, 
            int countOfAssets
        )
        {
            for (int i = 0; i < countOfTransactions; i++)
            {
                var transactionEntity = new Transaction
                {
                    Id = Guid.NewGuid(),
                    CategoryId = categories[_random.Next(countOfCategories)].Id,
                    Amount = _random.Next(1000000),
                    Date = DateTime.Now,
                    AssetId = assetsId[_random.Next(countOfAssets)]
                };

                _context.Transaction.Add(transactionEntity);
            }

            await _context.SaveChangesAsync();
        }
    }
}
