using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface IIsEntityExistsService
    {
        Task<bool> IsCategoryExistsAsync(Guid id);

        Task<bool> IsPostExistsAsync(Guid id);

        Task<bool> IsUserExistsAsync(Guid id);
    }
}
