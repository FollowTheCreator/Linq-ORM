using System;

namespace MoneyManager.BLL.Interfaces.Models.Transaction
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public Guid AssetId { get; set; }

        public string Comment { get; set; }
    }
}
