using AutoMapper;
using MoneyManager.BLL.Interfaces.Models;
using MoneyManager.BLL.Interfaces.Models.Asset;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;

        private readonly IUserService _userService;
        private readonly IConfigService _configService;

        private readonly IMapper _mapper;

        public AssetService(IAssetRepository assetRepository, IUserService userService, IMapper mapper, IConfigService configService)
        {
            _assetRepository = assetRepository;

            _configService = configService;
            _userService = userService;

            _mapper = mapper;
        }

        public async Task<CreateAssetResult> CreateAsync(Asset item)
        {
            var result = new CreateAssetResult();

            if(await _userService.IsUserExistsAsync(item.UserId))
            {
                result.IsAssetUserExists = true;

                item.Id = Guid.NewGuid();
                var convertedItem = _mapper.Map<Asset, DAL.Interfaces.Models.Asset>(item);
                await _assetRepository.CreateAsync(convertedItem);

                return result;
            }

            result.IsAssetUserExists = false;
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _assetRepository.DeleteAsync(id);
        }

        public async Task<AssetViewModel> GetRecordsAsync(PageInfo pageInfo)
        {
            pageInfo.CheckPageInfo(_configService.GetPageSize());

            var convertedPageInfo = _mapper.Map<PageInfo, DAL.Interfaces.Models.PageInfo>(pageInfo);
            var items = await _assetRepository.GetRecordsAsync(convertedPageInfo);
            var convertedItems = _mapper.Map<IEnumerable<DAL.Interfaces.Models.Asset>, IEnumerable<Asset>>(items);

            pageInfo.TotalItems = await _assetRepository.RecordsCountAsync();
            pageInfo.TotalPages = (int)Math.Ceiling(pageInfo.TotalItems / (double)pageInfo.PageSize);

            return new AssetViewModel
            {
                Assets = convertedItems,
                PageInfo = pageInfo
            };
        }

        public async Task<Asset> GetByIdAsync(Guid id)
        {
            var item = await _assetRepository.GetByIdAsync(id);
            var convertedItem = _mapper.Map<DAL.Interfaces.Models.Asset, Asset>(item);

            return convertedItem;
        }

        public async Task<UpdateAssetResult> UpdateAsync(Asset item)
        {
            var result = new UpdateAssetResult
            {
                IsAssetUserExists = await _userService.IsUserExistsAsync(item.UserId),
                IsAssetExists = await IsAssetExistsAsync(item.Id)
            };

            if (result.IsAssetExists && result.IsAssetUserExists)
            {
                var convertedItem = _mapper.Map<Asset, DAL.Interfaces.Models.Asset>(item);
                await _assetRepository.UpdateAsync(convertedItem);
            }

            return result;
        }

        public async Task<bool> IsAssetExistsAsync(Guid id)
        {
            var result = await _assetRepository.GetByIdAsync(id);

            return result != null;
        }
    }
}
