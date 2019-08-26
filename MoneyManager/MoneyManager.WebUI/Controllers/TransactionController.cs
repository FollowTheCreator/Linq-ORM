using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.BLL.Interfaces.Services.TransactionService;
using MoneyManager.WebUI.Models.Transaction;
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

        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllAsync()
        {
            var items = await _transactionService.GetAllAsync();

            var convertedItems = _mapper.Map<IEnumerable<BLL.Interfaces.Models.Transaction.Transaction>, IEnumerable<Transaction>>(items);

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
            var convertedModel = _mapper.Map<Transaction, BLL.Interfaces.Models.Transaction.Transaction>(model);

            var createResult = await _transactionService.CreateAsync(convertedModel);
            if (createResult.IsAssetExists && createResult.IsCategoryExists && createResult.IsAmountPositive)
            {
                return RedirectToAction("GetAllAsync", "Transaction");
            }
            else if (!createResult.IsCategoryExists)
            {
                ModelState.AddModelError("", "Category with this Id doesn't exist");
            }
            else if (!createResult.IsAssetExists)
            {
                ModelState.AddModelError("", "Asset with this Id doesn't exist");
            }
            else
            {
                ModelState.AddModelError("", "Amount should be positive");
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
            var convertedModel = _mapper.Map<Transaction, BLL.Interfaces.Models.Transaction.Transaction>(model);

            var createResult = await _transactionService.UpdateAsync(convertedModel);
            if (createResult.IsAssetExists && createResult.IsCategoryExists && createResult.IsAmountPositive)
            {
                return RedirectToAction("GetAllAsync", "Transaction");
            }
            else if (!createResult.IsCategoryExists)
            {
                ModelState.AddModelError("", "Category with this Id doesn't exist");
            }
            else if (!createResult.IsAssetExists)
            {
                ModelState.AddModelError("", "Asset with this Id doesn't exist");
            }
            else
            {
                ModelState.AddModelError("", "Amount should be positive");
            }

            return View("~/Views/Transaction/Update.cshtml", model);
        }

        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _transactionService.DeleteAsync(id);
            return RedirectToAction("GetAllAsync", "Transaction");
        }
    }
}