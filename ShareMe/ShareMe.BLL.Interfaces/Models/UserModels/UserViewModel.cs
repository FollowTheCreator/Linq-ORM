using ShareMe.BLL.Interfaces.Models.PostModels;
using System;
using System.Collections.Generic;

namespace ShareMe.BLL.Interfaces.Models.UserModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Image { get; set; }

        public List<UserPost> UserPosts { get; set; }
    }
}
