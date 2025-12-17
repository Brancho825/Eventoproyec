using Proyecto.Domain.Entities;

namespace Proyecto.Application.Interfaces
{
    public interface IEventoRepository
    {
        Task AgregarAsync(Evento evento);
        Task<Evento> ObtenerPorIdAsync(Guid id);
        Task<IEnumerable<Evento>> ObtenerTodosAsync();
        Task ActualizarAsync(Evento evento);
        Task EliminarAsync(Evento evento);
        Task<bool> ExisteAsync(Guid id);
        Task<IEnumerable<Evento>> ObtenerEventosPorOrganizadorAsync(Guid organizadorId);
    }
}