using ShareMe.DAL.Interfaces.Models.CategoryModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.DAL.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<CategoryWithPostsCount>> GetCategoriesWithPostsCountAsync();
    }
}
