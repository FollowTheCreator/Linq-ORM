using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.WebUI.Models.PostModels
{
    public class PopularPost
    {
        public Guid Id { get; set; }

        public string Image { get; set; }

        public string Header { get; set; }
    }
}
