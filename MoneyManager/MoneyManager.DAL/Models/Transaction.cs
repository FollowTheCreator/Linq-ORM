using System;
using System.Collections.Generic;

namespace MoneyManager.DAL.Models
{
    public partial class Transaction
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid AssetId { get; set; }
        public string Comment { get; set; }

        public Asset Asset { get; set; }
        public Category Category { get; set; }
    }
}
