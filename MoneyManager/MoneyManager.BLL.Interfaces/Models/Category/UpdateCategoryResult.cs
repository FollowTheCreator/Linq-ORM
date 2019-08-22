using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.BLL.Interfaces.Models.Category
{
    public class UpdateCategoryResult
    {
        public bool IsParentExists { get; set; }

        public bool IsTypeExists { get; set; }
    }
}
