using Proyecto.Application.DTOs;
using Proyecto.Application.Interfaces;
using Proyecto.Domain.Entities;
using Proyecto.Domain.Enums;
using Proyecto.Domain.Exceptions;
using AutoMapper;

namespace Proyecto.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IMapper _mapper; 
        // private readonly IGeolocalizacionService _geolocalizacionService; // RF04: Integración pendiente

        public EventoService(IEventoRepository eventoRepository, IMapper mapper /*, IGeolocalizacionService geolocalizacionService */)
        {
            _eventoRepository = eventoRepository;
            _mapper = mapper;
            // _geolocalizacionService = geolocalizacionService;
        }

        public async Task<EventoDto> CrearEventoAsync(EventoCreacionDto dto, Guid organizadorId)
        {
            // 1. Mapear DTO a Entidad
            var nuevoEvento = _mapper.Map<Evento>(dto);
            
            // 2. Asignar valores de negocio y técnicos
            nuevoEvento.Id = Guid.NewGuid();
            nuevoEvento.OrganizadorId = organizadorId;
            nuevoEvento.CuposDisponibles = dto.Capacidad; 
            nuevoEvento.Estado = EstadoEvento.Borrador; // CU001
            nuevoEvento.CreadoEn = DateTime.UtcNow;
            nuevoEvento.ActualizadoEn = DateTime.UtcNow;

            // 3. (RF04) Lógica de geocodificación:
            // var coordenadas = await _geolocalizacionService.GeocodificarAsync(dto.Ubicacion);
            // nuevoEvento.Latitud = coordenadas.Latitud;
            // nuevoEvento.Longitud = coordenadas.Longitud;

            await _eventoRepository.AgregarAsync(nuevoEvento);

            // 4. Obtener evento con relaciones para el DTO de respuesta
            var eventoConRelaciones = await _eventoRepository.ObtenerPorIdAsync(nuevoEvento.Id);

            return _mapper.Map<EventoDto>(eventoConRelaciones);
        }
        
        public async Task ActualizarEventoAsync(Guid id, EventoCreacionDto dto, Guid userId, string userRole)
        {
            var evento = await _eventoRepository.ObtenerPorIdAsync(id);
            if (evento == null) throw new NotFoundException(nameof(Evento), id);
            
            // Autorización por Roles: [owner/admin] 
            if (userRole != Rol.Admin.ToString() && evento.OrganizadorId != userId)
            {
                throw new DomainException("No tienes permiso para actualizar este evento.");
            }
            
            // Mapear DTO a la entidad (Actualiza campos básicos)
            evento = _mapper.Map(dto, evento);
            
            evento.ActualizadoEn = DateTime.UtcNow;
            
            await _eventoRepository.ActualizarAsync(evento);
        }

        public async Task EliminarEventoAsync(Guid id, Guid userId, string userRole)
        {
            var evento = await _eventoRepository.ObtenerPorIdAsync(id);
            if (evento == null) throw new NotFoundException(nameof(Evento), id);

            if (userRole != Rol.Admin.ToString() && evento.OrganizadorId != userId)
            {
                throw new DomainException("No tienes permiso para eliminar este evento.");
            }

            await _eventoRepository.EliminarAsync(evento);
        }

        public async Task<EventoDto> ObtenerEventoPorIdAsync(Guid id)
        {
            var evento = await _eventoRepository.ObtenerPorIdAsync(id);
            if (evento == null) throw new NotFoundException(nameof(Evento), id);
            
            return _mapper.Map<EventoDto>(evento);
        }

        public async Task<IEnumerable<EventoDto>> ObtenerEventosPublicosAsync()
        {
            // Filtramos solo los publicados
            var eventos = (await _eventoRepository.ObtenerTodosAsync())
                            .Where(e => e.EsPublico && e.Estado == EstadoEvento.Publicado);
            
            return _mapper.Map<IEnumerable<EventoDto>>(eventos);
        }

        public async Task PublicarEventoAsync(Guid id, Guid userId, string userRole)
        {
            var evento = await _eventoRepository.ObtenerPorIdAsync(id);
            if (evento == null) throw new NotFoundException(nameof(Evento), id);
            
            if (userRole != Rol.Admin.ToString() && evento.OrganizadorId != userId)
            {
                throw new DomainException("No tienes permiso para publicar este evento.");
            }

            evento.Estado = EstadoEvento.Publicado;
            evento.ActualizadoEn = DateTime.UtcNow;
            await _eventoRepository.ActualizarAsync(evento);
        }
    }
}