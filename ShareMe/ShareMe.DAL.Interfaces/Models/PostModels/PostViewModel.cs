using ShareMe.DAL.Interfaces.Models.CommentModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.DAL.Interfaces.Models.PostModels
{
    public class PostViewModel
    {
        public Guid Id { get; set; }

        public string Header { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string Image { get; set; }

        public List<string> Tags { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public List<CommentViewModel> Comments { get; set; }

        public int CommentsCount { get; set; }
    }
}
