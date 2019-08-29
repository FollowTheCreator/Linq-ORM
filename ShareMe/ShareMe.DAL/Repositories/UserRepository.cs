﻿using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ShareMeContext context)
            : base(context)
        { }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            var item = await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Email == email);

            return item != null;
        }
    }
}
