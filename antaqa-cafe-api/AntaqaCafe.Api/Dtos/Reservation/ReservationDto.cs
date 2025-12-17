namespace AntaqaCafe.Api.Dtos.Reservation
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string? SpecialRequests { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? CancelledAt { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string EventTitle { get; set; } = string.Empty;
    }
}