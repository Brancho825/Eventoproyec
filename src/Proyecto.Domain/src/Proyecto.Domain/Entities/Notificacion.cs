using Proyecto.Domain.Enums;

namespace Proyecto.Domain.Entities
{
    public class Notificacion
    {
        public Guid Id { get; set; } 
        public Guid UsuarioId { get; set; } 
        
        public TipoNotificacion Tipo { get; set; } 
        public string Asunto { get; set; } = null!;
        public string Contenido { get; set; } = null!;
        // ...

        // Propiedad de navegación
        public Usuario Usuario { get; set; } = null!;
    }
}