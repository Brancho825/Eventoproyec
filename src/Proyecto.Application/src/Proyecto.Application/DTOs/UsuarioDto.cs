using Proyecto.Domain.Enums;

namespace Proyecto.Application.DTOs
{
    // DTO para visualizar el perfil de usuario (RF01)
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public Rol Rol { get; set; }
        public DateTime CreadoEn { get; set; }
    }
}