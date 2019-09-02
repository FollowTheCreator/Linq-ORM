using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.WebUI.Models.UserModels;
using System;
using System.Threading.Tasks;

namespace ShareMe.WebUI.Controllers
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

        public async Task<ActionResult<UserViewModel>> GetUser(Guid id)
        {
            var result = await _userService.GetUserAsync(id);
            var convertedResult = _mapper.Map<BLL.Interfaces.Models.UserModels.UserViewModel, UserViewModel>(result);

            return View("~/Views/User/User.cshtml", convertedResult);
        }
    }
}