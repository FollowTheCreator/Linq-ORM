using ShareMe.DAL.Interfaces.Models;
using ShareMe.DAL.Interfaces.Models.PostModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.DAL.Interfaces.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<List<PostPreview>> GetPostPreviewsAsync(PageInfo pageInfo);

        Task<List<PostPreview>> GetPostPreviewsBySearchAsync(PageInfo pageInfo, string header);

        Task<List<PostPreview>> GetPostPreviewsByTagAsync(PageInfo pageInfo, string tag);

        Task<List<PostPreview>> GetPostPreviewsByCategoryAsync(PageInfo pageInfo, Guid categoryId);

        Task<PostViewModel> GetPostViewModelAsync(Guid id);

        Task<List<PopularPost>> GetPopularPostsAsync();

        Task<List<UserPost>> GetUserPostsAsync(Guid userId);
    }
}
