using System;

namespace ShareMe.BLL.Interfaces.Models.PostTagModels
{
    public class PostTag
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public Guid TagId { get; set; }
    }
}
