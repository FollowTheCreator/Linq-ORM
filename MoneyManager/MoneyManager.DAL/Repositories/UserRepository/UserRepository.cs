using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Repositories.UserRepository;
using MoneyManager.DAL.Models.Contexts;

namespace MoneyManager.DAL.Repositories.UserRepository
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(MoneyManagerCodeFirstContext context)
            : base(context)
        { }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            User user = await DbSet
                .FirstOrDefaultAsync(u => u.Email == email);

            return user != null;
        }

        public async Task<string> GetSaltByIdAsync(Guid id)
        {
            var item = await GetByIdAsync(id);

            return item.Salt;
        }
    }
}
