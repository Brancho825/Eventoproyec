using Proyecto.Domain.Entities;

namespace Proyecto.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObtenerPorEmailAsync(string email);
        Task AgregarAsync(Usuario usuario);
        Task<bool> EmailExisteAsync(string email);
        Task<Usuario> ObtenerPorIdAsync(Guid id);
        Task ActualizarAsync(Usuario usuario); 
    }
}