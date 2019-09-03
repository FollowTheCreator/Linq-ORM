using System;

namespace ShareMe.DAL.Interfaces.Models.CategoryModels
{
    public class CategoryWithPostsCount
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int PostsCount { get; set; }
    }
}
