using ShareMe.DAL.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.DAL.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetRecordsAsync(PageInfo pageInfo);

        Task<T> GetByIdAsync(Guid id);

        Task<T> CreateAsync(T item);

        Task UpdateAsync(T item);

        Task DeleteAsync(Guid id);

        Task<int> RecordsCountAsync();

        IQueryable<T> GetPageOfRecords(IQueryable<T> records, PageInfo pageInfo);
    }
}
