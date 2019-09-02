using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models.UserModels;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ShareMeContext context)
            : base(context)
        { }

        public async Task<UserViewModel> GetUserAsync(Guid id)
        {
            var item = await DbSet
                .Where(user => user.Id == id)
                .Select(user =>
                    new UserViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Name,
                        Image = user.Image
                    }
                )
                .FirstOrDefaultAsync();

            return item;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            var item = await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Email == email);

            return item != null;
        }


    }
}
