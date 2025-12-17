namespace Proyecto.Application.DTOs
{
    // DTO para la solicitud de registro (RF01)
    public class UsuarioRegistroDto
    {
        public string Email { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
    }
}