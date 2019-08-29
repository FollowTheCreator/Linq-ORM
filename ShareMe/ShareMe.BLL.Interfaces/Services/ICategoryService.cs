using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetRecordsAsync(PageInfo pageInfo);

        Task<Category> GetByIdAsync(Guid id);

        Task CreateAsync(Category item);

        Task UpdateAsync(Category item);

        Task DeleteAsync(Guid id);

        Task<bool> IsCategoryExistsAsync(Guid id);
    }
}
