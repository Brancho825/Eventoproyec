using Proyecto.Application.DTOs;
using Proyecto.Domain.Entities;

namespace Proyecto.Application.Interfaces
{
    public interface IReporteService
    {
        Task<IEnumerable<ReservaDto>> ObtenerReservasPorEventoIdAsync(Guid eventoId);
        Task<IEnumerable<EventoDto>> ObtenerEventosOrganizadosAsync(Guid userId);
        Task<object> ObtenerResumenAdministrativoAsync();
    }
}