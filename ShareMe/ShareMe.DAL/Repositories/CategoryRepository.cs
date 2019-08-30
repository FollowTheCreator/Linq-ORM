using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models;
using ShareMe.DAL.Interfaces.Models.CategoryModels;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.DAL.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ShareMeContext _context;

        public CategoryRepository(ShareMeContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<CategoryViewModel>> GetCategoriesAsync()
        {
            var categories = await DbSet
                .GroupBy(category =>
                    new
                    {
                        category.Id,
                        category.Name
                    }
                )
                .ToListAsync();

            var result = (await Task.WhenAll(categories
                    .Select(async category =>
                        new CategoryViewModel
                        {
                            Id = category.Key.Id,
                            Name = category.Key.Name,
                            PostsCount = await _context
                                .Post
                                .Where(post => post.CategoryId == category.Key.Id)
                                .CountAsync()
                        }
                    )
                ))
                .OrderByDescending(category => category.PostsCount)
                .ToList();

            return result;
        }
    }
}
