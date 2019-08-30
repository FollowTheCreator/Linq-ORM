using ShareMe.DAL.Interfaces.Models;
using ShareMe.DAL.Interfaces.Models.TagModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.DAL.Interfaces.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<List<string>> GetPostTagsAsync(Guid postId);

        Task<List<string>> GetTagsAsync();
    }
}
