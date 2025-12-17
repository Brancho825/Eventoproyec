using System.ComponentModel.DataAnnotations;

namespace AntaqaCafe.Api.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        
        [Required]
        public DateTime ReservationDate { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int NumberOfGuests { get; set; }
        
        [StringLength(500)]
        public string? SpecialRequests { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public bool IsConfirmed { get; set; } = false;
        
        public bool IsCancelled { get; set; } = false;
        
        public DateTime? CancelledAt { get; set; }
        
        // Foreign keys
        public int UserId { get; set; }
        public int EventId { get; set; }
        
        // Navigation properties
        public User? User { get; set; }
        public Event? Event { get; set; }
    }
}