using Proyecto.Domain.Enums;

namespace Proyecto.Application.DTOs
{
    // DTO para crear o actualizar un evento (RF02)
    public class EventoCreacionDto
    {
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public Guid CategoriaId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Ubicacion { get; set; } = null!;
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public int Capacidad { get; set; }
        public decimal Precio { get; set; }
        public bool EsPublico { get; set; }
    }
}