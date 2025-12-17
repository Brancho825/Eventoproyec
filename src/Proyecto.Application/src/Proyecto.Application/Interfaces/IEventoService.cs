using Proyecto.Application.DTOs;

namespace Proyecto.Application.Interfaces
{
    public interface IEventoService
    {
        Task<EventoDto> CrearEventoAsync(EventoCreacionDto dto, Guid organizadorId);
        Task ActualizarEventoAsync(Guid id, EventoCreacionDto dto, Guid userId, string userRole);
        Task EliminarEventoAsync(Guid id, Guid userId, string userRole);
        Task<EventoDto> ObtenerEventoPorIdAsync(Guid id);
        Task<IEnumerable<EventoDto>> ObtenerEventosPublicosAsync();
        Task PublicarEventoAsync(Guid id, Guid userId, string userRole);
    }
}