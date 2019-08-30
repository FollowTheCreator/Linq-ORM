using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.DAL.Interfaces.Models.CategoryModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int PostsCount { get; set; }
    }
}
