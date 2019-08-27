using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Repositories;
using MoneyManager.DAL.Models.Contexts;
using System;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Repositories
{
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        private readonly MoneyManagerContext _context;

        public CategoryRepository(MoneyManagerContext context)
            :base(context)
        {
            _context = context;
        }

        public async Task<bool> IsTypeExistsAsync(int id)
        {
            var result = await _context.Type.FindAsync(id);

            return result != null;
        }
    }
}
