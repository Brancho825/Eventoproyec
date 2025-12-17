using Proyecto.Application.Interfaces;
using Proyecto.Application.DTOs;
using Proyecto.Domain.Entities;
using Proyecto.Domain.Enums;
using Proyecto.Domain.Exceptions;
using Proyecto.Shared.Helpers;
using AutoMapper;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq; // Necesario para Task.FromResult(Enumerable.Empty<ReservaDto>())

namespace Proyecto.Application.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPaymentGatewayService _paymentService;
        private readonly IMapper _mapper;

        public ReservaService(
            IReservaRepository reservaRepository, 
            IEventoRepository eventoRepository, 
            IUsuarioRepository usuarioRepository, 
            IPaymentGatewayService paymentService, 
            IMapper mapper)
        {
            _reservaRepository = reservaRepository;
            _eventoRepository = eventoRepository;
            _usuarioRepository = usuarioRepository;
            _paymentService = paymentService;
            _mapper = mapper;
        }

        public async Task<ReservaDto> CrearReservaAsync(ReservaCreacionDto dto, Guid usuarioId)
        {
            var evento = await _eventoRepository.ObtenerPorIdAsync(dto.EventoId);

            if (evento == null || evento.Estado != EstadoEvento.Publicado)
            {
                throw new NotFoundException(nameof(Evento), dto.EventoId);
            }

            if (evento.CuposDisponibles < dto.CantidadTickets)
            {
                throw new DomainException("No hay suficientes cupos disponibles para este evento.");
            }

            // Regla de Negocio: Limite de tickets por usuario (RNF03)
            var reservasExistentes = await _reservaRepository.ContarReservasPorEventoYUsuarioAsync(dto.EventoId, usuarioId);

            if (reservasExistentes + dto.CantidadTickets > Constantes.MaxReservasPorUsuario)
            {
                throw new DomainException($"Ha excedido el límite de {Constantes.MaxReservasPorUsuario} tickets por usuario para este evento.");
            }

            // Cálculo y Pago
            var montoTotal = evento.Precio * dto.CantidadTickets;
            var reservaId = Guid.NewGuid();
            
            // Simular proceso de pago (RF04)
            var pagoExitoso = await _paymentService.CrearIntentoDePagoAsync(montoTotal, reservaId);

            if (!pagoExitoso)
            {
                throw new DomainException("El proceso de pago falló. Intente nuevamente.");
            }

            var reserva = _mapper.Map<Reserva>(dto);
            reserva.Id = reservaId;
            reserva.UsuarioId = usuarioId;
            reserva.MontoTotal = montoTotal;
            reserva.Estado = EstadoReserva.Confirmada; // Se confirma tras el pago

            // Actualizar cupos del evento
            evento.CuposDisponibles -= dto.CantidadTickets;
            evento.ActualizadoEn = DateTime.UtcNow;


            await _reservaRepository.AgregarAsync(reserva);
            await _eventoRepository.ActualizarAsync(evento);
            await _reservaRepository.SaveChangesAsync(); 

            return _mapper.Map<ReservaDto>(reserva);
        }
        
        // --- Implementaciones de la Interfaz IReservaService ---
        
        // Método que ahora SÍ cumple la firma (Task<ReservaDto?>)
        public async Task<ReservaDto?> ObtenerReservaPorIdAsync(Guid id)
        {
            var reserva = await _reservaRepository.ObtenerPorIdAsync(id);
            return _mapper.Map<ReservaDto>(reserva);
        }

        // Método que ahora SÍ cumple la firma (Task)
        public async Task CancelarReservaAsync(Guid id, Guid usuarioId)
        {
            var reserva = await _reservaRepository.ObtenerPorIdAsync(id);

            if (reserva == null || reserva.UsuarioId != usuarioId)
            {
                throw new NotFoundException(nameof(Reserva), id);
            }

            reserva.Estado = EstadoReserva.Cancelada;
            await _reservaRepository.ActualizarAsync(reserva);
            await _reservaRepository.SaveChangesAsync();
        }
        
        // CORRECCIÓN FINAL: Los métodos que lanzan NotImplementedException deben ser ASYNC o devolver un Task válido.

        public async Task<IEnumerable<ReservaDto>> ObtenerMisReservasAsync(Guid usuarioId)
        {
            // Implementación pendiente: Devolvemos una lista vacía dentro de un Task
            await Task.CompletedTask; // Simula una operación asíncrona
            return Enumerable.Empty<ReservaDto>();
        }

        public async Task<IEnumerable<ReservaDto>> ObtenerReservasPorEventoAsync(Guid eventoId, Guid usuarioId, string rol)
        {
            // Implementación pendiente: Devolvemos una lista vacía dentro de un Task
            await Task.CompletedTask; // Simula una operación asíncrona
            return Enumerable.Empty<ReservaDto>();
        }
    }
}