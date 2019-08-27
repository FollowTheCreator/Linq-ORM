using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.WebUI.Models.Type;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.WebUI.Controllers
{
    public class TypeController : Controller
    {
        private readonly ITypeService _typeService;

        private readonly IMapper _mapper;

        public TypeController(ITypeService typeService, IMapper mapper)
        {
            _typeService = typeService;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<Type>>> GetAllAsync()
        {
            var items = await _typeService.GetAllAsync();

            var convertedItems = _mapper.Map<IEnumerable<BLL.Interfaces.Models.Type.Type>, IEnumerable<Type>>(items);

            return View("~/Views/Type/Types.cshtml", convertedItems);
        }

        public async Task<ActionResult<Type>> GetByIdAsync(int id)
        {
            var item = await _typeService.GetByIdAsync(id);

            var convertedItem = _mapper.Map<BLL.Interfaces.Models.Type.Type, Type>(item);

            return View("~/Views/Type/Type.cshtml", convertedItem);
        }

        [HttpGet]
        public ActionResult CreateAsync()
        {
            return View("~/Views/Type/Create.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateAsync(Type model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Type/Create.cshtml", model);
            }

            var convertedModel = _mapper.Map<Type, BLL.Interfaces.Models.Type.Type>(model);

            await _typeService.CreateAsync(convertedModel);

            return RedirectToAction("GetAllAsync", "Type");
        }

        [HttpGet]
        public async Task<ActionResult<Type>> UpdateAsync(int id)
        {
            var item = await _typeService.GetByIdAsync(id);

            var convertedItem = _mapper.Map<BLL.Interfaces.Models.Type.Type, Type>(item);

            return View("~/Views/Type/Update.cshtml", convertedItem);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAsync(Type model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Type/Update.cshtml", model);
            }

            var convertedModel = _mapper.Map<Type, BLL.Interfaces.Models.Type.Type>(model);

            await _typeService.UpdateAsync(convertedModel);

            return RedirectToAction("GetByIdAsync", "Type", new { id = model.Id });
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _typeService.DeleteAsync(id);

            return RedirectToAction("GetAllAsync", "Type");
        }
    }
}