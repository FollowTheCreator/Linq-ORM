using MoneyManager.DAL.Interfaces.Models;
using System;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Interfaces.Repositories.CategoryRepository
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
        Task<bool> IsParentExistsAsync(Guid id);

        Task<bool> IsTypeExistsAsync(int id);
    }
}
