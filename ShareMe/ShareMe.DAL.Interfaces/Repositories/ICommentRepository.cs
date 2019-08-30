using ShareMe.DAL.Interfaces.Models;
using ShareMe.DAL.Interfaces.Models.CommentModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.DAL.Interfaces.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<CommentViewModel>> GetPostComments(Guid postId);

        Task<int> GetPostCommentsCount(Guid postId);
    }
}
