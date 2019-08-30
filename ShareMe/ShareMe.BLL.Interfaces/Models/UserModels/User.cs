using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.BLL.Interfaces.Models.UserModels
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Hash { get; set; }

        public string Salt { get; set; }

        public string Image { get; set; }
    }
}
