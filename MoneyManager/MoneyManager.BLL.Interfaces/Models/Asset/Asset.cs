using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.BLL.Interfaces.Models.Asset
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }

        public User.User User { get; set; }
    }
}
