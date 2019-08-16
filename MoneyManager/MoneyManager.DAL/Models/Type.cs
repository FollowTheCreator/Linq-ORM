using System;
using System.Collections.Generic;

namespace MoneyManager.DAL.Models
{
    public partial class Type
    {
        public Type()
        {
            Category = new HashSet<Category>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Category> Category { get; set; }
    }
}
