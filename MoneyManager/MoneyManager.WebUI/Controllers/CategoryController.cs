using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.WebUI.Models.Category;
using MoneyManager.WebUI.Models.ViewModels;
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

        public async Task<ActionResult<IEnumerable<Category>>> GetRecordsAsync(PageInfo pageInfo)
        {
            var convertedPageInfo = _mapper.Map<PageInfo, BLL.Interfaces.Models.PageInfo>(pageInfo);
            var items = await _categoryService.GetRecordsAsync(convertedPageInfo);

            var convertedItems = _mapper.Map<BLL.Interfaces.Models.Category.CategoryViewModel, CategoryViewModel>(items);

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
            if (!ModelState.IsValid)
            {
                return View("~/Views/Category/Create.cshtml", model);
            }

            var convertedModel = _mapper.Map<Category, BLL.Interfaces.Models.Category.Category>(model);

            var createResult = await _categoryService.CreateAsync(convertedModel);
            if (createResult.IsCategoryTypeExists)
            {
                return RedirectToAction("GetRecordsAsync", "Category");
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
            if (!ModelState.IsValid)
            {
                return View("~/Views/Category/Update.cshtml", model);
            }

            var convertedModel = _mapper.Map<Category, BLL.Interfaces.Models.Category.Category>(model);

            var updateResult = await _categoryService.UpdateAsync(convertedModel);
            if (!updateResult.IsCategoryExists)
            {
                ModelState.AddModelError("", "Category with this Id doesn't exist");
            }
            else if (!updateResult.IsCategoryTypeExists)
            {
                ModelState.AddModelError("", "Type with this Id doesn't exist");
            }
            else
            {
                return RedirectToAction("GetRecordsAsync", "Category");
            }

            return View("~/Views/Category/Update.cshtml", model);
        }

        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction("GetRecordsAsync", "Category");
        }
    }
}