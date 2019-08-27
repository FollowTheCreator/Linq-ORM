using MoneyManager.BLL.Interfaces.Models.Category;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> GetByIdAsync(Guid id);

        Task<CreateCategoryResult> CreateAsync(Category item);

        Task<UpdateCategoryResult> UpdateAsync(Category item);

        Task DeleteAsync(Guid id);

        Task<bool> IsTypeExistsAsync(int id);
    }
}
