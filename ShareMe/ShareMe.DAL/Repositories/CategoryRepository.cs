using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models.CategoryModels;
using ShareMe.DAL.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.DAL.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ShareMeContext context)
            : base(context)
        { }

        public async Task<List<CategoryWithPostsCount>> GetCategoriesWithPostsCountAsync()
        {
            var result = await DbSet
                .Include(c => c.Post)
                .Select(category =>
                    new CategoryWithPostsCount
                    {
                        Id = category.Id,
                        Name = category.Name,
                        PostsCount = category.Post.Count
                    }
                )
                .OrderByDescending(category => category.PostsCount)
                .ToListAsync();

            return result;
        }
    }
}
