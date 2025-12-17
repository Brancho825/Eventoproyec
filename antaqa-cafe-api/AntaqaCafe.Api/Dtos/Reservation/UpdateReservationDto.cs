using System.ComponentModel.DataAnnotations;

namespace AntaqaCafe.Api.Dtos.Reservation
{
    public class UpdateReservationDto
    {
        public DateTime? ReservationDate { get; set; }
        
        [Range(1, int.MaxValue)]
        public int? NumberOfGuests { get; set; }
        
        [StringLength(500)]
        public string? SpecialRequests { get; set; }
        
        public bool? IsConfirmed { get; set; }
        
        public bool? IsCancelled { get; set; }
    }
}