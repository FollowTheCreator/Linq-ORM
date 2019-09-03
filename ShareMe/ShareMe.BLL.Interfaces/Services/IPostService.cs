using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.PostModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface IPostService
    {
        Task CreateAsync(PostCreateModel item);

        Task UpdateAsync(Post item);

        Task DeleteAsync(Guid id);

        Task<PostPreviewViewModel> GetPostPreviewsAsync(PageInfo pageInfo);

        Task<PostPreviewViewModel> GetPostPreviewsByTagAsync(PageInfo pageInfo, string tag);

        Task<PostPreviewViewModel> GetPostPreviewsBySearchAsync(PageInfo pageInfo, string header);

        Task<PostPreviewViewModel> GetPostPreviewsByCategoryAsync(PageInfo pageInfo, Guid categoryId);

        Task<PostViewModel> GetPostViewModelAsync(Guid id);

        Task<List<PopularPost>> GetPopularPostsAsync();

        Task<List<UserPost>> GetUserPostsAsync(Guid userId);
    }
}
