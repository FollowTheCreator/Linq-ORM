using Microsoft.EntityFrameworkCore;
using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Repositories;
using MoneyManager.DAL.Models.Contexts;
using System;
using System.Threading.Tasks;

namespace MoneyManager.DAL.Repositories
{
    public class AssetRepository : Repository<Asset, Guid>, IAssetRepository
    {
        private readonly DbSet<User> _userDbSet;

        public AssetRepository(MoneyManagerContext context)
            :base(context)
        {
            _userDbSet = context.User;
        }

        public async Task<bool> IsAssetExistsAsync(Guid id)
        {
            var result = await _userDbSet.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id);

            return result != null;
        }
    }
}
