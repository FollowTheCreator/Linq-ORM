﻿using System;

namespace ShareMe.BLL.Interfaces.Models.PostModels
{
    public class Post
    {
        public Guid Id { get; set; }

        public string Header { get; set; }

        public string Content { get; set; }

        public int Views { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public Guid CategoryId { get; set; }

        public string Image { get; set; }
    }
}
