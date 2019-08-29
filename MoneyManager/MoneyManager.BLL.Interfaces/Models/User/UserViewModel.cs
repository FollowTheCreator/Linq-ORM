using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.BLL.Interfaces.Models.User
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
