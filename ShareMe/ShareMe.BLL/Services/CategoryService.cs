using AutoMapper;
using ShareMe.BLL.Interfaces.Models.CategoryModels;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
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

        public async Task<List<CategoryViewModel>> GetCategoriesAsync()
        {
            var result = await _categoryRepository.GetCategoriesWithPostsCountAsync();
            var convertedResult = _mapper.Map<List<DAL.Interfaces.Models.CategoryModels.CategoryWithPostsCount>, List<CategoryViewModel>>(result);

            return convertedResult;
        }
    }
}
