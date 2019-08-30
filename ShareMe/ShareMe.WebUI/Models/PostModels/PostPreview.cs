﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.WebUI.Models.PostModels
{
    public class PostPreview
    {
        public Guid Id { get; set; }

        public string Header { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string Image { get; set; }

        public List<string> Tags { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }
    }
}
