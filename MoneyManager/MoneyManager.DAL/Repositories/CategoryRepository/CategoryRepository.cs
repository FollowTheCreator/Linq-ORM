using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Repositories.CategoryRepository;
using MoneyManager.DAL.Models.Contexts;
using System;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Repositories.CategoryRepository
{
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        private readonly MoneyManagerContext _context;

        public CategoryRepository(MoneyManagerContext context)
            :base(context)
        {
            _context = context;
        }

        public async Task<bool> IsParentExistsAsync(Guid id)
        {
            var result = await _context.Category.FindAsync(id);

            return result != null;
        }

        public async Task<bool> IsTypeExistsAsync(int id)
        {
            var result = await _context.Type.FindAsync(id);

            return result != null;
        }
    }
}
