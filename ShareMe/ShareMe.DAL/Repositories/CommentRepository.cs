using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.DAL.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ShareMeContext context)
            : base(context)
        { }
    }
}
