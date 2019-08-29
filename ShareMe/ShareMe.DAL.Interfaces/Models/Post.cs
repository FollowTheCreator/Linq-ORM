using System;
using System.Collections.Generic;

namespace ShareMe.DAL.Interfaces.Models
{
    public partial class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
            PostTag = new HashSet<PostTag>();
        }

        public Guid Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public int Views { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string Image { get; set; }

        public Category Category { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public ICollection<PostTag> PostTag { get; set; }
    }
}
