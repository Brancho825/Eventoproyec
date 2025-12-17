using Proyecto.Application.DTOs;

namespace Proyecto.Application.Interfaces
{
    public interface IReservaService
    {
        Task<ReservaDto> CrearReservaAsync(ReservaCreacionDto dto, Guid usuarioId);
        Task CancelarReservaAsync(Guid reservaId, Guid usuarioId);
        Task<IEnumerable<ReservaDto>> ObtenerMisReservasAsync(Guid usuarioId);
        Task<IEnumerable<ReservaDto>> ObtenerReservasPorEventoAsync(Guid eventoId, Guid userId, string userRole);
    }
}