using Proyecto.Application.DTOs;
using Proyecto.Application.Interfaces;
using Proyecto.Domain.Entities;
using Proyecto.Domain.Exceptions;
using AutoMapper;
using Proyecto.Application.Interfaces;

namespace Proyecto.Application.Services
{
    public class ReporteService : IReporteService
    {
        // Se asume la existencia de IReservaRepository, IEventoRepository, y IMapper
        private readonly IReservaRepository _reservaRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IMapper _mapper;
        
        public ReporteService(IReservaRepository reservaRepository, IEventoRepository eventoRepository, IMapper mapper)
        {
            _reservaRepository = reservaRepository;
            _eventoRepository = eventoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservaDto>> ObtenerReservasPorEventoIdAsync(Guid eventoId)
        {
            var evento = await _eventoRepository.ObtenerPorIdAsync(eventoId);
            if (evento == null) 
            {
                throw new NotFoundException(nameof(Evento), eventoId);
            }
            // Asume que el evento incluye la propiedad de navegación Reservas (corregido en Domain)
            var reservas = evento.Reservas.ToList(); 
            
            return _mapper.Map<IEnumerable<ReservaDto>>(reservas);
        }

        public async Task<IEnumerable<EventoDto>> ObtenerEventosOrganizadosAsync(Guid userId)
        {
            // Asume la existencia del método en el repositorio
            var eventos = await _eventoRepository.ObtenerEventosPorOrganizadorAsync(userId); 
            return _mapper.Map<IEnumerable<EventoDto>>(eventos);
        }

        public Task<object> ObtenerResumenAdministrativoAsync()
        {
            // Lógica simulada de administración
            var resumen = new 
            {
                TotalUsuarios = 150, 
                TotalEventosPublicados = 50, 
                TotalIngresos = 85000.50m 
            };
            return Task.FromResult<object>(resumen);
        }
    }
}