using System;

namespace MoneyManager.WebUI.Models.User
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Hash { get; set; }

        public string Salt { get; set; }
    }
}
