using MoneyManager.BLL.Interfaces.Models;
using MoneyManager.BLL.Interfaces.Models.Asset;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Interfaces.Services
{
    public interface IAssetService
    {
        Task<AssetViewModel> GetRecordsAsync(PageInfo pageInfo);

        Task<Asset> GetByIdAsync(Guid id);

        Task<CreateAssetResult> CreateAsync(Asset item);

        Task<UpdateAssetResult> UpdateAsync(Asset item);

        Task DeleteAsync(Guid id);

        Task<bool> IsAssetExistsAsync(Guid id);
    }
}
