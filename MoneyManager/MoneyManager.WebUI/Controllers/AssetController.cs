using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.WebUI.Models.Asset;
using MoneyManager.WebUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.WebUI.Controllers
{
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;

        private readonly IMapper _mapper;

        public AssetController(IAssetService assetService, IMapper mapper)
        {
            _assetService = assetService;
            _mapper = mapper;
        }

        public async Task<ActionResult<AssetViewModel>> GetRecordsAsync(PageInfo pageInfo)
        {
            var convertedPageInfo = _mapper.Map<PageInfo, BLL.Interfaces.Models.PageInfo>(pageInfo);
            var items = await _assetService.GetRecordsAsync(convertedPageInfo);

            var convertedItems = _mapper.Map<BLL.Interfaces.Models.Asset.AssetViewModel, AssetViewModel>(items);

            return View("~/Views/Asset/Assets.cshtml", convertedItems);
        }

        public async Task<ActionResult<Asset>> GetByIdAsync(Guid id)
        {
            var item = await _assetService.GetByIdAsync(id);

            var convertedItem = _mapper.Map<BLL.Interfaces.Models.Asset.Asset, Asset>(item);

            return View("~/Views/Asset/Asset.cshtml", convertedItem);
        }

        [HttpGet]
        public ActionResult CreateAsync()
        {
            return View("~/Views/Asset/Create.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateAsync(Asset model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Asset/Create.cshtml", model);
            }

            var convertedModel = _mapper.Map<Asset, BLL.Interfaces.Models.Asset.Asset>(model);

            var createResult = await _assetService.CreateAsync(convertedModel);
            if (createResult.IsAssetUserExists)
            {
                return RedirectToAction("GetRecordsAsync", "Asset");
            }

            ModelState.AddModelError("", "User with this Id doesn't exist");
            return View("~/Views/Asset/Create.cshtml", model);
        }

        [HttpGet]
        public async Task<ActionResult<Asset>> UpdateAsync(Guid id)
        {
            var item = await _assetService.GetByIdAsync(id);

            var convertedItem = _mapper.Map<BLL.Interfaces.Models.Asset.Asset, Asset>(item);

            return View("~/Views/Asset/Update.cshtml", convertedItem);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAsync(Asset model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Asset/Update.cshtml", model);
            }

            var convertedModel = _mapper.Map<Asset, BLL.Interfaces.Models.Asset.Asset>(model);
 
            var updateResult = await _assetService.UpdateAsync(convertedModel);
            if (!updateResult.IsAssetExists)
            {
                ModelState.AddModelError("", "Asset with this Id doesn't exist");
            }
            else if (!updateResult.IsAssetUserExists)
            {
                ModelState.AddModelError("", "User with this Id doesn't exist");
            }
            else
            {
                return RedirectToAction("GetByIdAsync", "Asset", new { id = model.Id });
            }

            return View("~/Views/Asset/Update.cshtml", model);
        }

        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _assetService.DeleteAsync(id);
            return RedirectToAction("GetRecordsAsync", "Asset");
        }
    }
}