using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.WebUI.Models.QueriesModels;
using MoneyManager.WebUI.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<ActionResult<UserBalance>> GetUsersBalances()
        {
            var result = await _service.GetUsersBalances();

            var convertedResult = _mapper.Map<List<BLL.Interfaces.Models.QueriesModels.UserBalance>, List<UserBalance>>(result);

            return View("~/Views/Queries/GetUsersBalances.cshtml", convertedResult);
        }

        public async Task<ActionResult<IEnumerable<TotalAmountForDate>>> GetTotalAmount(Guid id, DateTime startDate, DateTime endDate)
        {
            var result = await _service.GetTotalAmount(id, startDate, endDate);

            var convertedResult = _mapper.Map<IEnumerable<BLL.Interfaces.Models.QueriesModels.TotalAmountForDate>, IEnumerable<TotalAmountForDate>>(result);

            return View("~/Views/Queries/GetTotalAmount.cshtml", convertedResult);
        }

        public async Task<ActionResult<IEnumerable<AmountOfCategories>>> GetTotalAmountOfCategories(Guid id, int operationTypeId)
        {
            var result = await _service.GetTotalAmountOfCategories(id, operationTypeId);

            var convertedResult = _mapper.Map<IEnumerable<BLL.Interfaces.Models.QueriesModels.AmountOfCategories>, IEnumerable<AmountOfCategories>>(result);

            return View("~/Views/Queries/GetTotalAmountOfCategories.cshtml", convertedResult);
        }

        public async Task<ActionResult<List<UserAsset>>> GetUserAssets(Guid id)
        {
            var result = await _service.GetUserAssets(id);

            var convertedResult = _mapper.Map<List<BLL.Interfaces.Models.QueriesModels.UserAsset>, List<UserAsset>>(result);

            return View("~/Views/Queries/GetUserAssets.cshtml", convertedResult);
        }

        public async Task<ActionResult<List<UserTransaction>>> GetUserTransactions(Guid id)
        {
            var result = await _service.GetUserTransactions(id);

            var convertedResult = _mapper.Map<List<BLL.Interfaces.Models.QueriesModels.UserTransaction>, List<UserTransaction>>(result);

            return View("~/Views/Queries/GetUserTransactions.cshtml", convertedResult);
        }

        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var result = await _service.GetUserByEmail(email);

            var convertedResult = _mapper.Map<BLL.Interfaces.Models.User.User, User>(result);

            return View("~/Views/Queries/GetUserByEmail.cshtml", convertedResult);
        }

        public async Task DeleteAllUsersInCurrentMonth(Guid id)
        {
            await _service.DeleteAllUsersInCurrentMonth(id);
        }
    }
}