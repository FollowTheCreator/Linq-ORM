using System;

namespace ShareMe.WebUI.Models.PostModels
{
    public class UserPost
    {
        public Guid Id { get; set; }

        public string Image { get; set; }

        public string Header { get; set; }

        public DateTime Date { get; set; }
    }
}
