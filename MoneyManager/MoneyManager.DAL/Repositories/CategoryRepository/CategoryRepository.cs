using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Repositories.CategoryRepository;
using MoneyManager.DAL.Models.Contexts;

namespace MoneyManager.DAL.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MoneyManagerCodeFirstContext _context;

        public CategoryRepository(MoneyManagerCodeFirstContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category item)
        {
            _context.Category.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            _context.Category.Remove(await GetByIdAsync(id));

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context
                .Category
                .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _context
                .Category
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Category item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
