using ShareMe.DAL.Interfaces.Models;
using ShareMe.DAL.Interfaces.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.DAL.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<CategoryViewModel>> GetCategoriesAsync();

    }
}
