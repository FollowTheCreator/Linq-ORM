using System;
using System.Collections.Generic;

namespace ShareMe.DAL.Interfaces.Models
{
    public partial class Category
    {
        public Category()
        {
            Post = new HashSet<Post>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Post> Post { get; set; }
    }
}
