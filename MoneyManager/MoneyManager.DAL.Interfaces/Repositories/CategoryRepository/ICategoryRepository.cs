using MoneyManager.DAL.Interfaces.Models;
using System;

namespace MoneyManager.DAL.Interfaces.Repositories.CategoryRepository
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    { }
}
