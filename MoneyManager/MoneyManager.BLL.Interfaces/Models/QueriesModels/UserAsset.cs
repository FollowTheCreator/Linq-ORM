using System;

namespace MoneyManager.BLL.Interfaces.Models.QueriesModels
{
    public class UserAsset
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }
    }
}
