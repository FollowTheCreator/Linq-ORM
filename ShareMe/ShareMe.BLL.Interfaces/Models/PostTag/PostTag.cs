using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.BLL.Interfaces.Models.PostTag
{
    public class PostTag
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public Guid TagId { get; set; }
    }
}
