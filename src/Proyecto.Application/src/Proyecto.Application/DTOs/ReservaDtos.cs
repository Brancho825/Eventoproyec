using Proyecto.Domain.Enums;

namespace Proyecto.Application.DTOs
{
    public class ReservaDto
    {
        public Guid Id { get; set; }
        public string TituloEvento { get; set; } = null!;
        public DateTime FechaEvento { get; set; }
        public int CantidadTickets { get; set; }
        public decimal MontoTotal { get; set; }
        public EstadoReserva Estado { get; set; }
        public DateTime CreadoEn { get; set; }
    }
}