using ShareMe.BLL.Interfaces.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.BLL.Interfaces.Models.PostModels
{
    public class PostPreviewViewModel
    {
        public IEnumerable<PostPreview> PostPreviews { get; set; }

        public PageInfo PageInfo { get; set; }

        public List<PopularPost> PopularPosts { get; set; }

        public List<CategoryViewModel> Categories { get; set; }

        public List<string> Tags { get; set; }
    }
}
