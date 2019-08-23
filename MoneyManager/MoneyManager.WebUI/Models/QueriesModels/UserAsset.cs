using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.WebUI.Models.QueriesModels
{
    public class UserAsset
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }
    }
}
