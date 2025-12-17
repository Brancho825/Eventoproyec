using Proyecto.Domain.Entities;

namespace Proyecto.Application.Interfaces
{
    public interface ICategoriaRepository
    {
        Task AgregarAsync(Categoria categoria);
        Task<Categoria> ObtenerPorIdAsync(Guid id);
        Task<IEnumerable<Categoria>> ObtenerTodosAsync();
        Task ActualizarAsync(Categoria categoria);
        Task EliminarAsync(Categoria categoria);
        Task<bool> ExisteAsync(Guid id);
    }
}