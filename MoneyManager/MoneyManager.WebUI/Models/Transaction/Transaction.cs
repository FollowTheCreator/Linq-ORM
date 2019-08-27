using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager.WebUI.Models.Transaction
{
    public class Transaction
    {
        [Required(ErrorMessage = "Id is null")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "CategoryId is null")]
        public Guid CategoryId { get; set; }

        [Range(1, (double)decimal.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Date is null")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "AssetId is null")]
        public Guid AssetId { get; set; }

        public string Comment { get; set; }
    }
}
