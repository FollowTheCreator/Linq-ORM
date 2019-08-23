using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.DAL.Interfaces.Models.QueriesModels
{
    public class UserIdEmailName
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
