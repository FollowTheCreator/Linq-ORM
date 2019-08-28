using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Models.QueriesModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
        Task<IEnumerable<AmountOfCategories>> GetTotalAmountOfCategories(Guid id, int operationTypeId, DateTime date);
    }
}
