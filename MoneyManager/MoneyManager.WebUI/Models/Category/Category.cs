using System;

namespace MoneyManager.WebUI.Models.Category
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public Guid? ParentId { get; set; }
    }
}
