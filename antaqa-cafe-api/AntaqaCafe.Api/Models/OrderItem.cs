using System.ComponentModel.DataAnnotations;

namespace AntaqaCafe.Api.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal TotalPrice { get; set; }
        
        // Foreign keys
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        
        // Navigation properties
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}