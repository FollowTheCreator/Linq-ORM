using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.BLL.Interfaces.Services.UserService;
using MoneyManager.WebUI.Models.User;

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

        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();

            var convertedUsers = _mapper.Map<IEnumerable<BLL.Interfaces.Models.User.User>, IEnumerable<UserViewModel>>(users);

            return View("~/Views/User/Users.cshtml", convertedUsers);
        }

        public async Task<ActionResult<UserViewModel>> GetByIdAsync(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            var convertedUser = _mapper.Map<BLL.Interfaces.Models.User.User, UserViewModel>(user);

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
            var createUserResult = await _userService.CreateAsync(convertedModel);
            if (!createUserResult.AlreadyExists)
            {
                return RedirectToAction("GetAllAsync", "User");
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
            var convertedModel = _mapper.Map<UpdateUserModel, BLL.Interfaces.Models.User.UpdateUserModel>(model);

            await _userService.UpdateAsync(convertedModel);

            return RedirectToAction("GetByIdAsync", "User", new { id = model.Id });
        }

        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _userService.DeleteAsync(id);
            return RedirectToAction("GetAllAsync", "User");
        }
    }
}