﻿using System;
using System.Collections.Generic;

namespace ShareMe.DAL.Interfaces.Models
{
    public partial class Tag
    {
        public Tag()
        {
            PostTag = new HashSet<PostTag>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<PostTag> PostTag { get; set; }
    }
}
