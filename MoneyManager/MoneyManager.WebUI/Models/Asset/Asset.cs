using System;

namespace MoneyManager.WebUI.Models.Asset
{
    public class Asset
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid UserId { get; set; }
    }
}
