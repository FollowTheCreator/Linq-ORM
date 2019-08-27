using System.ComponentModel.DataAnnotations;

namespace MoneyManager.WebUI.Models.Type
{
    public class Type
    {
        [Required(ErrorMessage = "Id is null")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is null")]
        public string Name { get; set; }
    }
}
