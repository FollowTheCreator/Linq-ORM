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

        Task<List<Post>> GetPostsBySearchAsync(PageInfo pageInfo, string header);

        Task<List<Post>> GetPostsByTagAsync(PageInfo pageInfo, string tag);

        Task<List<Post>> GetPostsByCategoryAsync(PageInfo pageInfo, Guid categoryId);

        Task<Post> GetPostByIdAsync(Guid id);

        Task<List<Post>> GetPostsOrderedByViewsDescAsync();

        Task<List<Post>> GetUserPostsAsync(Guid userId);
    }
}
