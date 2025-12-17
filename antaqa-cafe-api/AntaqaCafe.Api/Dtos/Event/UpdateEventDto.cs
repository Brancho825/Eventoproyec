using System.ComponentModel.DataAnnotations;

namespace AntaqaCafe.Api.Dtos.Event
{
    public class UpdateEventDto
    {
        [StringLength(200)]
        public string? Title { get; set; }
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        [StringLength(200)]
        public string? Location { get; set; }
        
        [Range(1, int.MaxValue)]
        public int? MaxCapacity { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }
        
        public bool? IsActive { get; set; }
    }
}