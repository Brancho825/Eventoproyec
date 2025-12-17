using Proyecto.Domain.Enums;
using System;

namespace Proyecto.Domain.Entities
{
    public class Reserva
    {
        public Guid Id { get; set; } 
        public int CantidadTickets { get; set; }
        public DateTime CreadoEn { get; set; }
        
        // Propiedades de Negocio (Capitalización corregida de los errores CS1061)
        public decimal MontoTotal { get; set; } // << AÑADIDO Y CORREGIDO
        public EstadoReserva Estado { get; set; } // << AÑADIDO Y CORREGIDO

        // Claves Foráneas (Asegurando que UsuarioId exista para el mapeo)
        public Guid UsuarioId { get; set; } // << AÑADIDO Y CORREGIDO
        public Guid EventoId { get; set; }

        // Propiedades de navegación (Relaciones)
        public Usuario Usuario { get; set; } = null!;
        public Evento Evento { get; set; } = null!;
    }
}