using ShareMe.BLL.Interfaces.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<bool> IsCategoryExistsAsync(Guid id);

        Task<List<CategoryViewModel>> GetCategoriesAsync();
    }
}
