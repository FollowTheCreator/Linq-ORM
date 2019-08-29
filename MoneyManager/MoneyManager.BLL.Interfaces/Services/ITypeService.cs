using MoneyManager.BLL.Interfaces.Models;
using MoneyManager.BLL.Interfaces.Models.Type;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Interfaces.Services
{
    public interface ITypeService
    {
        Task<TypeViewModel> GetRecordsAsync(PageInfo pageInfo);

        Task<Type> GetByIdAsync(int id);

        Task CreateAsync(Type item);

        Task<UpdateTypeResult> UpdateAsync(Type item);

        Task DeleteAsync(int id);

        Task<bool> IsTypeExistsAsync(int id);
    }
}
