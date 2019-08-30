using ShareMe.DAL.Interfaces.Models.PostModels;
using ShareMe.DAL.Interfaces.Models.TagModels;
using System;
using System.Collections.Generic;

namespace ShareMe.DAL.Interfaces.Models.PostTagModels
{
    public partial class PostTag : IEntity
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid TagId { get; set; }

        public Post Post { get; set; }
        public Tag Tag { get; set; }
    }
}
