using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager.WebUI.Models.Category
{
    public class Category
    {
        [Required(ErrorMessage = "Id is null")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is null")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is null")]
        public int Type { get; set; }
    }
}
