using System;

namespace MoneyManager.BLL.Interfaces.Models.Category
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }
    }
}
