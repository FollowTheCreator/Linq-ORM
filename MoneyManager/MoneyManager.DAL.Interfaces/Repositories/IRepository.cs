﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Interfaces.Repositories
{
    public interface IRepository<T, TId>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(TId id);

        Task CreateAsync(T item);

        Task UpdateAsync(T item);

        Task DeleteAsync(TId id);
    }
}
