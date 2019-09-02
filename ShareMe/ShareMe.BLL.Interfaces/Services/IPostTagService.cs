using ShareMe.BLL.Interfaces.Models.PostTagModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.BLL.Interfaces.Services
{
    public interface IPostTagService
    {
        Task CreateAsync(PostTag item);

        Task<bool> IsPostTagExistsAsync(Guid id);

        Task DeleteByPostIdAsync(Guid postId);
    }
}
