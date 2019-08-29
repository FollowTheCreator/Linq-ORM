using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.WebUI.Models.ViewModels
{
    public class TypeViewModel
    {
        public IEnumerable<Type.Type> Types { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
