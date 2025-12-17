using System.ComponentModel.DataAnnotations;

namespace AntaqaCafe.Api.Dtos.Product
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        
        [StringLength(500)]
        public string? ImageUrl { get; set; }
        
        public bool IsAvailable { get; set; } = true;
        
        [Required]
        public int CategoryId { get; set; }
    }
}