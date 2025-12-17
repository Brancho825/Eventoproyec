namespace Proyecto.Application.DTOs
{
    // DTO para la solicitud de login (RF01)
    public class UsuarioLoginDto
    {
        public string Email { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
    }
}