using System.Collections.Generic;

namespace MoneyManager.WebUI.Models.ViewModels
{
    public class TransactionViewModel
    {
        public IEnumerable<Transaction.Transaction> Transactions { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
