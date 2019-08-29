using System;

namespace MoneyManager.WebUI.Models.User
{
    public class ProtectedUserModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
