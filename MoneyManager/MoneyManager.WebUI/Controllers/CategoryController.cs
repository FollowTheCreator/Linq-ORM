using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.BLL.Interfaces.Services.CategoryService;
using MoneyManager.WebUI.Models.Category;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<Category>>> GetAllAsync()
        {
            var items = await _categoryService.GetAllAsync();

            var convertedItems = _mapper.Map<IEnumerable<BLL.Interfaces.Models.Category.Category>, IEnumerable<Category>>(items);

            return View("~/Views/Category/Categories.cshtml", convertedItems);
        }

        public async Task<ActionResult<Category>> GetByIdAsync(Guid id)
        {
            var item = await _categoryService.GetByIdAsync(id);

            var convertedItem = _mapper.Map<BLL.Interfaces.Models.Category.Category, Category>(item);

            return View("~/Views/Category/Category.cshtml", convertedItem);
        }

        [HttpGet]
        public ActionResult CreateAsync()
        {
            return View("~/Views/Category/Create.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateAsync(Category model)
        {
            var convertedModel = _mapper.Map<Category, BLL.Interfaces.Models.Category.Category>(model);

            var createResult = await _categoryService.CreateAsync(convertedModel);
            if (createResult.IsParentExists && createResult.IsTypeExists)
            {
                return RedirectToAction("GetAllAsync", "Category");
            }
            else if(!createResult.IsParentExists)
            {
                ModelState.AddModelError("", "Parent category with this Id doesn't exist");
            }
            else
            {
                ModelState.AddModelError("", "Type with this Id doesn't exist");
            }

            return View("~/Views/Category/Create.cshtml", model);
        }

        [HttpGet]
        public async Task<ActionResult<Category>> UpdateAsync(Guid id)
        {
            var item = await _categoryService.GetByIdAsync(id);

            var convertedItem = _mapper.Map<BLL.Interfaces.Models.Category.Category, Category>(item);

            return View("~/Views/Category/Update.cshtml", convertedItem);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAsync(Category model)
        {
            var convertedModel = _mapper.Map<Category, BLL.Interfaces.Models.Category.Category>(model);

            var createResult = await _categoryService.UpdateAsync(convertedModel);
            if (createResult.IsParentExists && createResult.IsTypeExists)
            {
                return RedirectToAction("GetAllAsync", "Category");
            }
            else if (!createResult.IsParentExists)
            {
                ModelState.AddModelError("", "Parent category with this Id doesn't exist");
            }
            else
            {
                ModelState.AddModelError("", "Type with this Id doesn't exist");
            }

            return View("~/Views/Category/Update.cshtml", model);
        }

        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction("GetAllAsync", "Category");
        }
    }
}