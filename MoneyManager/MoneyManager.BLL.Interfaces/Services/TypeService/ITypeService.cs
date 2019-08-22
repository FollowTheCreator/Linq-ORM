using MoneyManager.BLL.Interfaces.Models.Type;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Interfaces.Services.TypeService
{
    public interface ITypeService
    {
        Task<IEnumerable<Type>> GetAllAsync();

        Task<Type> GetByIdAsync(int id);

        Task CreateAsync(Type item);

        Task UpdateAsync(Type item);

        Task DeleteAsync(int id);
    }
}
