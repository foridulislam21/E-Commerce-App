using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater then zero!")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity muct be at least one!")]
        public int Quantity { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Type { get; set; }
    }
}