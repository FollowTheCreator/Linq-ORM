using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Repositories.CategoryRepository;
using MoneyManager.DAL.Models.Contexts;
using System;

namespace MoneyManager.DAL.Repositories.CategoryRepository
{
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(MoneyManagerContext context)
            :base(context)
        { }
    }
}
