using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager.WebUI.Models.User
{
    public class UpdateUserModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is null")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is null")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not confirmed")]
        public string ConfirmPassword { get; set; }
    }
}
