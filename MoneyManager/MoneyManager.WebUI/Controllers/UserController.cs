using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.WebUI.Models.User;
using MoneyManager.WebUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ActionResult<UserViewModel>> GetRecordsAsync(PageInfo pageInfo)
        {
            var convertedPageInfo = _mapper.Map<PageInfo, BLL.Interfaces.Models.PageInfo>(pageInfo);
            var items = await _userService.GetRecordsAsync(convertedPageInfo);

            var convertedUsers = _mapper.Map<BLL.Interfaces.Models.User.UserViewModel, UserViewModel>(items);

            return View("~/Views/User/Users.cshtml", convertedUsers);
        }

        public async Task<ActionResult<ProtectedUserModel>> GetByIdAsync(Guid id)
        {
            var item = await _userService.GetByIdAsync(id);

            var convertedUser = _mapper.Map<BLL.Interfaces.Models.User.User, ProtectedUserModel>(item);

            return View("~/Views/User/User.cshtml", convertedUser);
        }

        [HttpGet]
        public ActionResult CreateAsync()
        {
            return View("~/Views/User/Create.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateAsync(CreateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/User/Create.cshtml", model);
            }

            var convertedModel = _mapper.Map<CreateUserModel, BLL.Interfaces.Models.User.CreateUserModel>(model);
            var createResult = await _userService.CreateAsync(convertedModel);
            if (!createResult.AlreadyExists)
            {
                return RedirectToAction("GetRecordsAsync", "User");
            }

            ModelState.AddModelError("", "This Email already exists");
            return View("~/Views/User/Create.cshtml", model);
        }

        [HttpGet]
        public async Task<ActionResult<UpdateUserModel>> UpdateAsync(Guid id)
        {
            var item = await _userService.GetByIdAsync(id);

            var convertedItem = _mapper.Map<BLL.Interfaces.Models.User.User, UpdateUserModel>(item);

            return View("~/Views/User/Update.cshtml", convertedItem);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAsync(UpdateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/User/Update.cshtml", model);
            }

            var convertedModel = _mapper.Map<UpdateUserModel, BLL.Interfaces.Models.User.UpdateUserModel>(model);

            var updateResult = await _userService.UpdateAsync(convertedModel);
            if (!updateResult.IsUserExists)
            {
                ModelState.AddModelError("", "User with this Id doesn't exist");

                return View("~/Views/User/Update.cshtml", model);
            }

            return RedirectToAction("GetByIdAsync", "User", new { id = model.Id });
        }

        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _userService.DeleteAsync(id);
            return RedirectToAction("GetRecordsAsync", "User");
        }
    }
}