using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.BLL.Interfaces.Services.QueriesService;
using MoneyManager.WebUI.Models.QueriesModels;

namespace MoneyManager.WebUI.Controllers
{
    public class QueriesController : Controller
    {
        private readonly IQueriesService _service;

        private readonly IMapper _mapper;

        public QueriesController(IQueriesService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public ActionResult All()
        {
            return View("~/Views/Queries/Queries.cshtml");
        }

        public async Task<ActionResult<UserIdEmailName>> GetSortedUsers()
        {
            var result = await _service.GetSortedUsers();

            var convertedResult = _mapper.Map<List<BLL.Interfaces.Models.QueriesModels.UserIdEmailName>, List<UserIdEmailName>>(result);

            return View("~/Views/Queries/GetSortedUsers.cshtml", convertedResult);
        }
    }
}