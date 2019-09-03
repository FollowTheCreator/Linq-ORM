﻿using System;

namespace ShareMe.WebUI.Models.CategoryModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int PostsCount { get; set; }
    }
}
