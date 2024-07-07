
using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter a price.")]
        [Range(0, Int32.MaxValue, ErrorMessage = "The value must be greater than or equal to 0.")]
        public decimal? Price { get; set; }

        [Display(Name = "Image")]
        public string? ImageName { get; set; }

        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public int? CategoryId { get; set; }

    }
}