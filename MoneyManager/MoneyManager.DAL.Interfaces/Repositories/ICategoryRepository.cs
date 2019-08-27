using MoneyManager.DAL.Interfaces.Models;
using System;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
        Task<bool> IsTypeExistsAsync(int id);
    }
}
