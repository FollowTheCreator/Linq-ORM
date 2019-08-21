using System;
using System.Collections.Generic;

namespace MoneyManager.DAL.Interfaces.Models
{
    public partial class User : IId<Guid>
    {
        public User()
        {
            Asset = new HashSet<Asset>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        public ICollection<Asset> Asset { get; set; }
    }
}
