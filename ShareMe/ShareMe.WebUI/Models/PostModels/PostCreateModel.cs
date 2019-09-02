using System;
using System.Collections.Generic;

namespace ShareMe.WebUI.Models.PostModels
{
    public class PostCreateModel
    {
        public Guid Id { get; set; }

        public string Header { get; set; }

        public string Content { get; set; }

        public int Views { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public Guid CategoryId { get; set; }

        public string Image { get; set; }

        public List<string> Tags { get; set; }
    }
}
