using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.BLL.Interfaces.Services.AssetService;
using MoneyManager.WebUI.Models.Asset;
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

        public async Task<ActionResult<IEnumerable<Asset>>> GetAllAsync()
        {
            var items = await _assetService.GetAllAsync();

            var convertedItems = _mapper.Map<IEnumerable<BLL.Interfaces.Models.Asset.Asset>, IEnumerable<Asset>>(items);

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
            var convertedModel = _mapper.Map<Asset, BLL.Interfaces.Models.Asset.Asset>(model);

            var createResult = await _assetService.CreateAsync(convertedModel);
            if (createResult.IsExists)
            {
                return RedirectToAction("GetAllAsync", "Asset");
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
            var convertedModel = _mapper.Map<Asset, BLL.Interfaces.Models.Asset.Asset>(model);
 
            var createResult = await _assetService.UpdateAsync(convertedModel);
            if (createResult.IsExists)
            {
                return RedirectToAction("GetByIdAsync", "Asset", new { id = model.Id });
            }

            ModelState.AddModelError("", "User with this Id doesn't exist");
            return View("~/Views/Asset/Update.cshtml", model);
        }

        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _assetService.DeleteAsync(id);
            return RedirectToAction("GetAllAsync", "Asset");
        }
    }
}