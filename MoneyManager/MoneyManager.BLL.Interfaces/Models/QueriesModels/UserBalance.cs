using System;

namespace MoneyManager.BLL.Interfaces.Models.QueriesModels
{
    public class UserBalance
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }
    }
}
