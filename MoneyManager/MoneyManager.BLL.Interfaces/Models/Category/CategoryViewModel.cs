using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.BLL.Interfaces.Models.Category
{
    public class CategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
