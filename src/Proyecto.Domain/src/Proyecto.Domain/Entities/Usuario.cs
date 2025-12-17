using Proyecto.Domain.Enums;

namespace Proyecto.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; } 
        // Propiedades de texto
        public string Email { get; set; } = null!; // Indica que EF Core lo inicializará
        public string ContrasenaHash { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public Rol Rol { get; set; } 
        public bool EstaActivo { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime ActualizadoEn { get; set; } 

        // Propiedades de navegación (Relaciones)
        public Perfil Perfil { get; set; } = null!; // Relación 1:1
        // Colecciones se inicializan con una lista vacía para evitar errores al agregar ítems
        public ICollection<Evento> EventosOrganizados { get; set; } = new List<Evento>(); 
        public ICollection<Reserva> MisReservas { get; set; } = new List<Reserva>();
    }
}