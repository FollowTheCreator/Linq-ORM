using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Repositories.TypeRepository;
using MoneyManager.DAL.Models.Contexts;

namespace MoneyManager.DAL.Repositories.TypeRepository
{
    public class TypeRepository : ITypeRepository
    {
        private readonly MoneyManagerCodeFirstContext _context;

        public TypeRepository(MoneyManagerCodeFirstContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Interfaces.Models.Type item)
        {
            _context.Type.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _context.Type.Remove(await GetByIdAsync(id));

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Interfaces.Models.Type>> GetAllAsync()
        {
            return await _context
                .Type
                .ToListAsync();
        }

        public async Task<Interfaces.Models.Type> GetByIdAsync(int id)
        {
            return await _context
                .Type
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Interfaces.Models.Type item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
