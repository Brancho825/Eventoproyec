using Proyecto.Domain.Enums;
using System.Collections.Generic;

namespace Proyecto.Domain.Entities
{
    public class Evento
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Ubicacion { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public int Capacidad { get; set; }
        
        // Propiedades de Negocio (Capitalización corregida de los errores CS1061)
        public int CuposDisponibles { get; set; } // << CORREGIDO
        public EstadoEvento Estado { get; set; }   // << CORREGIDO
        public bool EsPublico { get; set; }       // << CORREGIDO
        public decimal Precio { get; set; }       // Añadida por DTO
        
        public DateTime CreadoEn { get; set; }    // << CORREGIDO
        public DateTime ActualizadoEn { get; set; } // << CORREGIDO

        // Claves Foráneas
        public Guid OrganizadorId { get; set; }
        public Guid CategoriaId { get; set; }

        // Propiedades de navegación
        public Usuario Organizador { get; set; } = null!;
        public Categoria Categoria { get; set; } = null!;
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}