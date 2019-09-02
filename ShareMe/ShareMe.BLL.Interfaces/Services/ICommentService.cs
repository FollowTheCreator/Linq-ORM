using ShareMe.BLL.Interfaces.Models.CommentModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface ICommentService
    {
        Task CreateAsync(Comment item);

        Task UpdateAsync(Comment item);

        Task DeleteAsync(Guid id);

        Task<bool> IsCommentExistsAsync(Guid id);

        Task<List<CommentViewModel>> GetPostCommentsAsync(Guid postId);

        Task<int> GetPostCommentsCount(Guid postId);
    }
}
