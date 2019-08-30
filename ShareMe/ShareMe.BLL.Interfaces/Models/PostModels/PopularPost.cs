using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.BLL.Interfaces.Models.PostModels
{
    public class PopularPost
    {
        public Guid Id { get; set; }

        public string Image { get; set; }

        public string Header { get; set; }
    }
}
