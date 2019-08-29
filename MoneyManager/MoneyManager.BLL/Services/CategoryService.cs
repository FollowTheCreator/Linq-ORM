using AutoMapper;
using MoneyManager.BLL.Interfaces.Models;
using MoneyManager.BLL.Interfaces.Models.Category;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly ITypeService _typeService;
        private readonly IConfigService _configService;

        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            ITypeService typeService,
            IMapper mapper,
            IConfigService configService)
        {
            _categoryRepository = categoryRepository;

            _typeService = typeService;
            _configService = configService;

            _mapper = mapper;
        }

        public async Task<CreateCategoryResult> CreateAsync(Category item)
        {
            var result = new CreateCategoryResult
            {
                IsCategoryTypeExists = await _typeService.IsTypeExistsAsync(item.Type)
            };

            if (result.IsCategoryTypeExists)
            {
                item.Id = Guid.NewGuid();
                var convertedItem = _mapper.Map<Category, DAL.Interfaces.Models.Category>(item);
                await _categoryRepository.CreateAsync(convertedItem);
            }

            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<CategoryViewModel> GetRecordsAsync(PageInfo pageInfo)
        {
            pageInfo.CheckPageInfo(_configService.GetPageSize());

            var convertedPageInfo = _mapper.Map<PageInfo, DAL.Interfaces.Models.PageInfo>(pageInfo);
            var items = await _categoryRepository.GetRecordsAsync(convertedPageInfo);
            var convertedItems = _mapper.Map<IEnumerable<DAL.Interfaces.Models.Category>, IEnumerable<Category>>(items);

            pageInfo.TotalItems = await _categoryRepository.RecordsCountAsync();
            pageInfo.TotalPages = (int)Math.Ceiling(pageInfo.TotalItems / (double)pageInfo.PageSize);

            return new CategoryViewModel
            {
                Categories = convertedItems,
                PageInfo = pageInfo
            };
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var item = await _categoryRepository.GetByIdAsync(id);
            var convertedItem = _mapper.Map<DAL.Interfaces.Models.Category, Category>(item);

            return convertedItem;
        }

        public async Task<UpdateCategoryResult> UpdateAsync(Category item)
        {
            var result = new UpdateCategoryResult
            {
                IsCategoryTypeExists = await _typeService.IsTypeExistsAsync(item.Type),
                IsCategoryExists = await IsCategoryExistsAsync(item.Id)
            };

            if (result.IsCategoryExists && result.IsCategoryTypeExists)
            {
                var convertedItem = _mapper.Map<Category, DAL.Interfaces.Models.Category>(item);
                await _categoryRepository.UpdateAsync(convertedItem);
            }

            return result;
        }

        public async Task<bool> IsCategoryExistsAsync(Guid id)
        {
            var result = await _categoryRepository.GetByIdAsync(id);

            return result != null;
        }
    }
}
