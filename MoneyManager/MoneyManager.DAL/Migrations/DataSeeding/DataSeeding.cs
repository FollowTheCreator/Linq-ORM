using MoneyManager.DAL.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DAL.Interfaces.DataSeeding;
using MoneyManager.DAL.Interfaces.Models;

namespace MoneyManager.DAL.Migrations.DataSeeding
{
    public class DataSeeding : IDataSeeding
    {
        private readonly MoneyManagerCodeFirstContext _context;

        private readonly Coder _coder;
        private readonly Generate _generate;

        private readonly Random _random;

        public DataSeeding(MoneyManagerCodeFirstContext context, Coder coder, Generate generate)
        {
            _context = context;
            _coder = coder;
            _generate = generate;
            _random = new Random();
        }

        public async Task SeedData()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var usersId = new List<Guid>(11);
                    if (!await _context.User.AnyAsync())
                    {
                        for (int i = 0; i < 11; i++)
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

                            //todo repository ---- AddUser(user)
                        }

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        usersId = await _context.User.Select(user => user.Id).ToListAsync();
                        //usersId = GetAllUsers().Select(user => user.Id)
                    }

                    var assetsId = new List<Guid>(21);
                    if (!await _context.Asset.AnyAsync())
                    {
                        for (int i = 0; i < 21; i++)
                        {
                            var asset = new Asset
                            {
                                Id = Guid.NewGuid(),
                                Name = _generate.RandomName(),
                                UserId = usersId[i / 2]
                            };

                            assetsId.Add(asset.Id);

                            _context.Asset.Add(asset);
                            
                            //todo repository ---- AddAsset(asset)
                        }

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        assetsId = await _context.Asset.Select(asset => asset.Id).ToListAsync();
                        //todo repository ---- assetsId = GetAllAssets().Select(asset => asset.Id)
                    }

                    if (!await _context.Type.AnyAsync())
                    {
                        _context.Type.Add(new Interfaces.Models.Type { Id = 1, Name = "income" });
                        _context.Type.Add(new Interfaces.Models.Type { Id = 2, Name = "expence" });
                        await _context.SaveChangesAsync();
                        //todo repository ---- AddType(Type)
                    }

                    var catrgories = new List<Category>(11);
                    if (!await _context.Category.AnyAsync())
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            var category = new Category
                            {
                                Id = Guid.NewGuid(),
                                Name = _generate.RandomName(),
                                Type = _random.Next(1, 2)
                            };

                            catrgories.Add(category);
                        }
                        catrgories[5].ParentId = catrgories[0].Id;
                        catrgories[6].ParentId = catrgories[0].Id;
                        catrgories[7].ParentId = catrgories[1].Id;
                        catrgories[8].ParentId = catrgories[1].Id;
                        catrgories[9].ParentId = catrgories[5].Id;
                        catrgories[10].ParentId = catrgories[6].Id;
                        foreach (var category in catrgories)
                        {
                            _context.Category.Add(category);
                            //todo repository ---- AddCategory(category)
                        }

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        catrgories = await _context.Category.ToListAsync();
                        //todo repository ---- categories = GetAllCategories()
                    }

                    if (!await _context.Transaction.AnyAsync())
                    {
                        for (int i = 0; i < 1001; i++)
                        {
                            var transactionEntity = new Transaction
                            {
                                Id = Guid.NewGuid(),
                                CategoryId = catrgories[_random.Next(10)].Id,
                                Amount = _random.Next(1000000),
                                Date = DateTime.Now,
                                AssetId = assetsId[_random.Next(20)]
                            };

                            _context.Transaction.Add(transactionEntity);
                            
                            //todo repository ---- AddTransaction(transationEntity)
                        }

                        await _context.SaveChangesAsync();
                    }

                    transaction.Commit();
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
