using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.DAL.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ShareMeContext context)
            : base(context)
        { }
    }
}
