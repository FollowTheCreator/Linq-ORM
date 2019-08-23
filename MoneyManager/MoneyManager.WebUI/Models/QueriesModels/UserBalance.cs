using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.WebUI.Models.QueriesModels
{
    public class UserBalance
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }
    }
}
