using Microsoft.EntityFrameworkCore;
using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Repositories;
using MoneyManager.DAL.Models.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Repositories
{
    public class Repository<T, TId> : IRepository<T, TId> where T : class, IId<TId>
    {
        private readonly MoneyManagerCodeFirstContext _context;

        public readonly DbSet<T> DbSet;

        public Repository(MoneyManagerCodeFirstContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public async Task CreateAsync(T item)
        {
            DbSet.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TId id)
        {
            DbSet.Remove(await GetByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await DbSet
                .Where(item => item.Id.Equals(id))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
