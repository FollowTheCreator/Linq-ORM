using ShareMe.DAL.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.DAL.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetRecordsAsync(PageInfo pageInfo);

        Task<T> GetByIdAsync(Guid id);

        Task CreateAsync(T item);

        Task UpdateAsync(T item);

        Task DeleteAsync(Guid id);

        Task<int> RecordsCountAsync(); 
    }
}
