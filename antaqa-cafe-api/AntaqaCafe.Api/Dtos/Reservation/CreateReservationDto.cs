using System.ComponentModel.DataAnnotations;

namespace AntaqaCafe.Api.Dtos.Reservation
{
    public class CreateReservationDto
    {
        [Required]
        public DateTime ReservationDate { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int NumberOfGuests { get; set; }
        
        [StringLength(500)]
        public string? SpecialRequests { get; set; }
        
        [Required]
        public int EventId { get; set; }
    }
}