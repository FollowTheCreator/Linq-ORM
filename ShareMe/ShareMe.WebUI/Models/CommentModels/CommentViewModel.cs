using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.WebUI.Models.CommentModels
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
