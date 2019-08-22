using MoneyManager.BLL.Interfaces.Models.Asset;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Interfaces.Services.AssetService
{
    public interface IAssetService
    {
        Task<IEnumerable<Asset>> GetAllAsync();

        Task<Asset> GetByIdAsync(Guid id);

        Task<CreateAssetResult> CreateAsync(Asset item);

        Task<CreateAssetResult> UpdateAsync(Asset item);

        Task DeleteAsync(Guid id);

        Task<bool> IsUserExistsAsync(Guid id);
    }
}
