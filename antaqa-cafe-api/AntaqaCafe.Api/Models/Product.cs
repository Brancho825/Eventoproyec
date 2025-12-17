using System.ComponentModel.DataAnnotations;

namespace AntaqaCafe.Api.Models
{
    public class Product
    {
        public int Id { get; set; }
        
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
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign key
        public int CategoryId { get; set; }
        
        // Navigation property
        public ProductCategory? Category { get; set; }
        
        // Navigation property for orders
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}