using System;
using System.Collections.Generic;

namespace ShareMe.DAL.Interfaces.Models
{
    public partial class Comment : IEntity
    {
        public Comment()
        {
            InverseParent = new HashSet<Comment>();
        }

        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public Guid PostId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid UserId { get; set; }

        public Comment Parent { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
        public ICollection<Comment> InverseParent { get; set; }
    }
}
