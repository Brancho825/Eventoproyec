using System.ComponentModel.DataAnnotations;

namespace AntaqaCafe.Api.Dtos.Event
{
    public class CreateEventDto
    {
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
        
        [Range(1, int.MaxValue)]
        public int MaxCapacity { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}