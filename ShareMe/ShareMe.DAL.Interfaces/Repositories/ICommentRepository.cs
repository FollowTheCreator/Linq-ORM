using ShareMe.DAL.Interfaces.Models.CommentModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.DAL.Interfaces.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<CommentViewModel>> GetPostCommentsAsync(Guid postId);

        Task<int> GetPostCommentsCountAsync(Guid postId);

        Task<List<Comment>> GetChildrenAsync(Guid id);
    }
}
