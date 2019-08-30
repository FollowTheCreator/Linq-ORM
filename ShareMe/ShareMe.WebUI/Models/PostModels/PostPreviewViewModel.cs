using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.WebUI.Models.CategoryModels;

namespace ShareMe.WebUI.Models.PostModels
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
