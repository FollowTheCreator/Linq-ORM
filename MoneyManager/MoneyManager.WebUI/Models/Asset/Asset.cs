using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager.WebUI.Models.Asset
{
    public class Asset
    {
        [Required(ErrorMessage = "Id is null")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is null")]
        public string Name { get; set; }

        [Required(ErrorMessage = "UserId is null")]
        public Guid UserId { get; set; }
    }
}
