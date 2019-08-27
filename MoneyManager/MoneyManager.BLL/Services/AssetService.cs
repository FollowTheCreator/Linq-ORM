using AutoMapper;
using MoneyManager.BLL.Interfaces.Models.Asset;
using MoneyManager.BLL.Interfaces.Services.AssetService;
using MoneyManager.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _repository;

        private readonly IMapper _mapper;

        public AssetService(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateAssetResult> CreateAsync(Asset item)
        {
            var result = new CreateAssetResult();

            if(await IsUserExistsAsync(item.UserId))
            {
                result.IsExists = true;

                item.Id = Guid.NewGuid();
                var convertedItem = _mapper.Map<Asset, DAL.Interfaces.Models.Asset>(item);
                await _repository.CreateAsync(convertedItem);

                return result;
            }

            result.IsExists = false;
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Asset>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            var convertedItems = _mapper.Map<IEnumerable<DAL.Interfaces.Models.Asset>, IEnumerable<Asset>>(items);

            return convertedItems;
        }

        public async Task<Asset> GetByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var convertedItem = _mapper.Map<DAL.Interfaces.Models.Asset, Asset>(item);

            return convertedItem;
        }

        public async Task<bool> IsUserExistsAsync(Guid id)
        {
            return await _repository.IsAssetExistsAsync(id);
        }

        public async Task<UpdateAssetResult> UpdateAsync(Asset item)
        {
            var result = new UpdateAssetResult();

            if (await IsUserExistsAsync(item.UserId))
            {
                result.IsExists = true;

                var convertedItem = _mapper.Map<Asset, DAL.Interfaces.Models.Asset>(item);
                await _repository.UpdateAsync(convertedItem);

                return result;
            }

            result.IsExists = false;
            return result;
        }
    }
}
