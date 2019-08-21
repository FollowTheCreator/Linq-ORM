using MoneyManager.DAL.Interfaces.Models;
using System;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Interfaces.Repositories.AssetRepository
{
    public interface IAssetRepository : IRepository<Asset, Guid>
    {
        Task<bool> IsUserExistsAsync(Guid id);
    }
}
