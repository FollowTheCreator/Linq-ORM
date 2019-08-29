﻿using System.Collections.Generic;

namespace MoneyManager.DAL.Interfaces.Models
{
    public partial class Type : IEntity<int>
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
