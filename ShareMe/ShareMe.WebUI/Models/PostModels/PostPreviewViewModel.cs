using ShareMe.WebUI.Models.CategoryModels;
using System.Collections.Generic;

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
