using ShareMe.DAL.Interfaces.Models;
using ShareMe.DAL.Interfaces.Models.PostModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.DAL.Interfaces.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<List<Post>> GetPostsAsync(PageInfo pageInfo);

        Task<List<Post>> GetPostPreviewsBySearchAsync(PageInfo pageInfo, string header);

        Task<List<Post>> GetPostPreviewsByTagAsync(PageInfo pageInfo, string tag);

        Task<List<Post>> GetPostPreviewsByCategoryAsync(PageInfo pageInfo, Guid categoryId);

        Task<Post> GetPostByIdAsync(Guid id);

        Task<List<Post>> GetPostsOrderedByViewsDescAsync();

        Task<List<Post>> GetUserPostsAsync(Guid userId);
    }
}
