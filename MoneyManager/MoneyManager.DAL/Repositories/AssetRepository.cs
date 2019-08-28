using Microsoft.EntityFrameworkCore;
using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Models.QueriesModels;
using MoneyManager.DAL.Interfaces.Repositories;
using MoneyManager.DAL.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Repositories
{
    public class AssetRepository : Repository<Asset, Guid>, IAssetRepository
    {
        private readonly MoneyManagerContext _context;

        public AssetRepository(MoneyManagerContext context)
            :base(context)
        {
            _context = context;
        }

        public async Task<List<UserAsset>> GetUserAssets(Guid id)
        {
            var groupedRecords = await _context
                .Asset
                .AsNoTracking()
                .Include(asset => asset.User)
                .Include(asset => asset.Transaction)
                .Where(asset => asset.UserId == id)
                .GroupBy(asset =>
                    new
                    {
                        asset.Id,
                        asset.Name,
                        asset.CurrentBalance
                    }
                )
                .ToListAsync();

            var result = groupedRecords
               .Select(asset =>
                   new UserAsset
                   {
                       Id = asset.Key.Id,
                       Name = asset.Key.Name,
                       Balance = asset.Key.CurrentBalance
                   }
               )
               .OrderBy(a => a.Name)
               .ToList();

            return result;
        }
    }
}
