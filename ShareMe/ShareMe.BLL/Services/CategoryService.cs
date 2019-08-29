using AutoMapper;
using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.Category;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;

            _mapper = mapper;
        }

        public async Task CreateAsync(Category item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetRecordsAsync(PageInfo pageInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsCategoryExistsAsync(Guid id)
        {
            var result = await _categoryRepository.GetByIdAsync(id);

            return result != null;
        }

        public async Task UpdateAsync(Category item)
        {
            throw new NotImplementedException();
        }
    }
}
