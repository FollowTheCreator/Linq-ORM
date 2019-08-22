using System;

namespace MoneyManager.DAL.Interfaces.Models
{
    public partial class Transaction : IId<Guid>
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
