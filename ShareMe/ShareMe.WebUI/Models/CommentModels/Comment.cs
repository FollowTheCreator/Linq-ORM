﻿using System;

namespace ShareMe.WebUI.Models.CommentModels
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public Guid PostId { get; set; }

        public Guid? ParentId { get; set; }

        public Guid UserId { get; set; }
    }
}
