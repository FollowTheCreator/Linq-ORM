using System;

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
