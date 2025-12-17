using Proyecto.Domain.Entities;
using Proyecto.Application.DTOs;

namespace Proyecto.Application.Interfaces
{
    public interface IJwtService
    {
        AuthResponseDto GenerarTokens(Usuario usuario);
        (Guid? UsuarioId, string Rol) ValidarAccessToken(string token);
    }
}