using System.ComponentModel.DataAnnotations;

namespace AntaqaCafe.Api.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        [Required]
        public DateTime OrderDate { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign key
        public int UserId { get; set; }
        
        // Navigation property
        public User? User { get; set; }
        
        // Navigation property for order items
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
    
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Preparing,
        Ready,
        Completed,
        Cancelled
    }
}