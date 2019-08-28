using AutoMapper;
using MoneyManager.BLL.Interfaces.Models;
using MoneyManager.BLL.Interfaces.Models.Type;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.DAL.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Services
{
    public class TypeService : ITypeService
    {
        private readonly ITypeRepository _typeRepository;

        private readonly IConfigService _configService;

        private readonly IMapper _mapper;

        public TypeService(ITypeRepository typeRepository, IMapper mapper, IConfigService configService)
        {
            _typeRepository = typeRepository;

            _configService = configService;

            _mapper = mapper;
        }

        public async Task CreateAsync(Type item)
        {
            var convertedItem = _mapper.Map<Type, DAL.Interfaces.Models.Type>(item);
            await _typeRepository.CreateAsync(convertedItem);
        }

        public async Task DeleteAsync(int id)
        {
            await _typeRepository.DeleteAsync(id);
        }

        public async Task<TypeViewModel> GetRecordsAsync(PageInfo pageInfo)
        {
            pageInfo.CheckPageInfo(_configService.GetPageSize());

            var convertedPageInfo = _mapper.Map<PageInfo, DAL.Interfaces.Models.PageInfo>(pageInfo);
            var items = await _typeRepository.GetRecordsAsync(convertedPageInfo);
            var convertedItems = _mapper.Map<IEnumerable<DAL.Interfaces.Models.Type>, IEnumerable<Type>>(items);

            pageInfo.TotalItems = await _typeRepository.RecordsCountAsync();
            pageInfo.TotalPages = (int)System.Math.Ceiling(pageInfo.TotalItems / (double)pageInfo.PageSize);

            return new TypeViewModel
            {
                Types = convertedItems,
                PageInfo = pageInfo
            };
        }

        public async Task<Type> GetByIdAsync(int id)
        {
            var item = await _typeRepository.GetByIdAsync(id);
            var convertedItem = _mapper.Map<DAL.Interfaces.Models.Type, Type>(item);

            return convertedItem;
        }

        public async Task<UpdateTypeResult> UpdateAsync(Type item)
        {
            var result = new UpdateTypeResult
            {
                IsTypeExists = await IsTypeExistsAsync(item.Id)
            };

            if (result.IsTypeExists)
            {
                var convertedItem = _mapper.Map<Type, DAL.Interfaces.Models.Type>(item);
                await _typeRepository.UpdateAsync(convertedItem);
            }

            return result;
        }

        public async Task<bool> IsTypeExistsAsync(int id)
        {
            var result = await _typeRepository.GetByIdAsync(id);

            return result != null;
        }
    }
}
