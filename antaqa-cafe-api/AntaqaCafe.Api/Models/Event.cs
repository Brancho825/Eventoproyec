using System.ComponentModel.DataAnnotations;

namespace AntaqaCafe.Api.Models
{
    public class Event
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;
        
        [Range(0, int.MaxValue)]
        public int MaxCapacity { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public bool IsActive { get; set; } = true;
        
        // Foreign key
        public int CreatedByUserId { get; set; }
        
        // Navigation property
        public User? CreatedByUser { get; set; }
        
        // Navigation property for reservations
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}