using Proyecto.Application.DTOs;

namespace Proyecto.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegistrarUsuarioAsync(UsuarioRegistroDto registroDto);
        Task<AuthResponseDto> LoginAsync(string email, string contrasena);
        Task<AuthResponseDto> RefrescarTokenAsync(string refreshToken); 
    }
}