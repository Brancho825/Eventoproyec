namespace Proyecto.Application.DTOs
{
    // DTO para la respuesta de login (JWT y Refresh Token)
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}