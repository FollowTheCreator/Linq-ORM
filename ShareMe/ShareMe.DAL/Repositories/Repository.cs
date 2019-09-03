using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly ShareMeContext _context;

        public readonly DbSet<T> DbSet;

        public Repository(ShareMeContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public async Task<T> CreateAsync(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            DbSet.Add(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task DeleteAsync(Guid id)
        {
            DbSet.Remove(await GetByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetRecordsAsync(PageInfo pageInfo)
        {
            if (pageInfo == null)
            {
                throw new ArgumentNullException(nameof(pageInfo));
            }

            var records = DbSet.AsNoTracking();

            var recordsPage = GetPageOfRecords(records, pageInfo);

            var result = await recordsPage.ToListAsync();

            return result;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(item => item.Id.Equals(id));
        }

        public async Task UpdateAsync(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            DbSet.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<int> RecordsCountAsync()
        { 
            return await DbSet.CountAsync();
        }

        public IQueryable<T> GetPageOfRecords(IQueryable<T> records, PageInfo pageInfo)
        {
            if (records == null)
            {
                throw new ArgumentNullException(nameof(records));
            }

            if (pageInfo == null)
            {
                throw new ArgumentNullException(nameof(pageInfo));
            }

            return records
                .Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize)
                .Take(pageInfo.PageSize);
        }
    }
}
