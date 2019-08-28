using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.WebUI.Models.Transaction;
using MoneyManager.WebUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.WebUI.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        public async Task<ActionResult<TransactionViewModel>> GetRecordsAsync(PageInfo pageInfo)
        {
            var convertedPageInfo = _mapper.Map<PageInfo, BLL.Interfaces.Models.PageInfo>(pageInfo);
            var items = await _transactionService.GetRecordsAsync(convertedPageInfo);

            var convertedItems = _mapper.Map<BLL.Interfaces.Models.Transaction.TransactionViewModel, TransactionViewModel>(items);

            return View("~/Views/Transaction/Transactions.cshtml", convertedItems);
        }

        public async Task<ActionResult<Transaction>> GetByIdAsync(Guid id)
        {
            var item = await _transactionService.GetByIdAsync(id);

            var convertedItem = _mapper.Map<BLL.Interfaces.Models.Transaction.Transaction, Transaction>(item);

            return View("~/Views/Transaction/Transaction.cshtml", convertedItem);
        }

        [HttpGet]
        public ActionResult CreateAsync()
        {
            return View("~/Views/Transaction/Create.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateAsync(Transaction model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Transaction/Create.cshtml", model);
            }

            var convertedModel = _mapper.Map<Transaction, BLL.Interfaces.Models.Transaction.Transaction>(model);

            var createResult = await _transactionService.CreateAsync(convertedModel);
            if (!createResult.IsTransactionCategoryExists)
            {
                ModelState.AddModelError("", "Category with this Id doesn't exist");
            }
            else if (!createResult.IsTransactionAssetExists)
            {
                ModelState.AddModelError("", "Asset with this Id doesn't exist");
            }
            else if(!createResult.IsTransactionAmountPositive)
            {
                ModelState.AddModelError("", "Amount should be positive");
            }
            else
            {
                return RedirectToAction("GetRecordsAsync", "Transaction");
            }

            return View("~/Views/Transaction/Create.cshtml", model);
        }

        [HttpGet]
        public async Task<ActionResult<Transaction>> UpdateAsync(Guid id)
        {
            var item = await _transactionService.GetByIdAsync(id);

            var convertedItem = _mapper.Map<BLL.Interfaces.Models.Transaction.Transaction, Transaction>(item);

            return View("~/Views/Transaction/Update.cshtml", convertedItem);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAsync(Transaction model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Transaction/Update.cshtml", model);
            }

            var convertedModel = _mapper.Map<Transaction, BLL.Interfaces.Models.Transaction.Transaction>(model);

            var updateResult = await _transactionService.UpdateAsync(convertedModel);
            if (!updateResult.IsTransactionExists)
            {
                ModelState.AddModelError("", "Transaction with this Id doesn't exist");
            }
            else if (!updateResult.IsTransactionCategoryExists)
            {
                ModelState.AddModelError("", "Category with this Id doesn't exist");
            }
            else if (!updateResult.IsTransactionAssetExists)
            {
                ModelState.AddModelError("", "Asset with this Id doesn't exist");
            }
            else if(!updateResult.IsTransactionAmountPositive)
            {
                ModelState.AddModelError("", "Amount should be positive");
            }
            else
            {
                return RedirectToAction("GetRecordsAsync", "Transaction");
            }

            return View("~/Views/Transaction/Update.cshtml", model);
        }

        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _transactionService.DeleteAsync(id);
            return RedirectToAction("GetRecordsAsync", "Transaction");
        }
    }
}