using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Models.QueriesModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Interfaces.Repositories
{
    public interface IAssetRepository : IRepository<Asset, Guid>
    {
        Task<List<UserAsset>> GetUserAssets(Guid id);
    }
}
