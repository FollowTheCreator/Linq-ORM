using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.WebUI.Models.ViewModels
{
    public class CategoryViewModel
    {
        public IEnumerable<Category.Category> Categories { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
