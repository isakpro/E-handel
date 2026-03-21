using System.ComponentModel.DataAnnotations;

namespace ECommerce.Shared.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        [Range(0.1, 100000, ErrorMessage = "Priset måste vara över 0.")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}