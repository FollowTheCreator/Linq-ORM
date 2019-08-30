using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.BLL.Interfaces.Models.CommentModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public List<CommentViewModel> Children { get; set; }

        public string UserName { get; set; }

        public string UserImage { get; set; }

        public Guid UserId { get; set; }
    }
}
