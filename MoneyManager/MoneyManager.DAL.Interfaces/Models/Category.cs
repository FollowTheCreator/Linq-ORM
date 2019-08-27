using System;
using System.Collections.Generic;

namespace MoneyManager.DAL.Interfaces.Models
{
    public partial class Category : IId<Guid>
    {
        public Category()
        {
            Transaction = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }

        public Type TypeNavigation { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
    }
}
