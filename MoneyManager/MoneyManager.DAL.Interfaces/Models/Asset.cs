using System;
using System.Collections.Generic;

namespace MoneyManager.DAL.Interfaces.Models
{
    public partial class Asset : IId<Guid>
    {
        public Asset()
        {
            Transaction = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
    }
}
