using Proyecto.Domain.Enums;

namespace Proyecto.Application.DTOs
{
    public class EventoDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string NombreOrganizador { get; set; } = null!;
        public string NombreCategoria { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public string Ubicacion { get; set; } = null!;
        public int Capacidad { get; set; }
        public int CuposDisponibles { get; set; }
        public decimal Precio { get; set; }
        public EstadoEvento Estado { get; set; }
    }
}