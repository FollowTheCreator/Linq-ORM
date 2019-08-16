using System;
using System.Collections.Generic;

namespace MoneyManager.DAL.Models
{
    public partial class Category
    {
        public Category()
        {
            InverseParent = new HashSet<Category>();
            Transaction = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public Guid? ParentId { get; set; }

        public Category Parent { get; set; }
        public Type TypeNavigation { get; set; }
        public ICollection<Category> InverseParent { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
    }
}
