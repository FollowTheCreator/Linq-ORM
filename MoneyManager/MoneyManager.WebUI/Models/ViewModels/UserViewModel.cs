using MoneyManager.WebUI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.WebUI.Models.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<ProtectedUserModel> Users { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
