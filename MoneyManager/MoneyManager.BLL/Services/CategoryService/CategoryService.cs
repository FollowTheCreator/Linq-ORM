using AutoMapper;
using MoneyManager.BLL.Interfaces.Models.Category;
using MoneyManager.BLL.Interfaces.Services.CategoryService;
using MoneyManager.DAL.Interfaces.Repositories.CategoryRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateCategoryResult> CreateAsync(Category item)
        {
            var result = new CreateCategoryResult
            {
                IsTypeExists = await IsTypeExistsAsync(item.Type)
            };

            if (item.ParentId != null)
            {
                result.IsParentExists = await IsParentExistsAsync((Guid)item.ParentId);
            }
            else
            {
                result.IsParentExists = true;
            }

            if (result.IsParentExists && result.IsTypeExists)
            {
                item.Id = Guid.NewGuid();
                var convertedItem = _mapper.Map<Category, DAL.Interfaces.Models.Category>(item);
                await _repository.CreateAsync(convertedItem);
            }

            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            var convertedItems = _mapper.Map<IEnumerable<DAL.Interfaces.Models.Category>, IEnumerable<Category>>(items);

            return convertedItems;
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var convertedItem = _mapper.Map<DAL.Interfaces.Models.Category, Category>(item);

            return convertedItem;
        }

        public async Task<bool> IsParentExistsAsync(Guid id)
        {
            return await _repository.IsParentExistsAsync(id);
        }

        public async Task<bool> IsTypeExistsAsync(int id)
        {
            return await _repository.IsTypeExistsAsync(id);
        }

        public async Task<UpdateCategoryResult> UpdateAsync(Category item)
        {
            var result = new UpdateCategoryResult
            {
                IsTypeExists = await IsTypeExistsAsync(item.Type)
            };

            if (item.ParentId != null)
            {
                result.IsParentExists = await IsParentExistsAsync((Guid)item.ParentId);
            }
            else
            {
                result.IsParentExists = true;
            }

            if (result.IsParentExists && result.IsTypeExists)
            {
                var convertedItem = _mapper.Map<Category, DAL.Interfaces.Models.Category>(item);
                await _repository.UpdateAsync(convertedItem);
            }

            return result;
        }
    }
}
