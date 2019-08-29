using System;

namespace MoneyManager.DAL.Interfaces.Models.QueriesModels
{
    public class UserAsset
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }
    }
}
