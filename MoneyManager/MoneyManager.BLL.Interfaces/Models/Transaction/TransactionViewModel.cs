using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.BLL.Interfaces.Models.Transaction
{
    public class TransactionViewModel
    {
        public IEnumerable<Transaction> Transactions { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
