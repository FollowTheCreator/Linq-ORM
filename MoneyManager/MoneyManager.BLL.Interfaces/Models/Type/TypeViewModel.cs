using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.BLL.Interfaces.Models.Type
{
    public class TypeViewModel
    {
        public IEnumerable<Type> Types { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
