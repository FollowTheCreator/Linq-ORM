using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Repositories.AssetRepository;
using MoneyManager.DAL.Models.Contexts;

namespace MoneyManager.DAL.Repositories.AssetRepository
{
    public class AssetRepository : Repository<Asset, Guid>, IAssetRepository
    {
        private readonly DbSet<User> _userDbSet;

        public AssetRepository(MoneyManagerCodeFirstContext context)
            :base(context)
        {
            _userDbSet = context.User;
        }

        public async Task<bool> IsUserExistsAsync(Guid id)
        {
            var result = await _userDbSet.FindAsync(id);

            return result != null;
        }
    }
}
