using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.DAL.Interfaces.Models.PostModels
{
    public class UserPost
    {
        public Guid Id { get; set; }

        public string Image { get; set; }

        public string Header { get; set; }

        public DateTime Date { get; set; }
    }
}
