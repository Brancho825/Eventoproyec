using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.DTOs;
using Proyecto.Application.Interfaces;
using Proyecto.Domain.Enums;
using Proyecto.Domain.Exceptions;
using System.Security.Claims;
using System.Net;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/v1/events")]
    public class EventsController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventsController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        private Guid ObtenerUsuarioId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string ObtenerRol() => User.FindFirstValue(ClaimTypes.Role) ?? Rol.User.ToString();

        // RF02: Crear Evento (Solo Organizers y Admins)
        [HttpPost]
        [Authorize(Roles = "Organizer,Admin")]
        [ProducesResponseType(typeof(EventoDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateEvent([FromBody] EventoCreacionDto dto)
        {
            var eventoDto = await _eventoService.CrearEventoAsync(dto, ObtenerUsuarioId());
            return CreatedAtAction(nameof(GetEventById), new { id = eventoDto.Id }, eventoDto);
        }

        // RF02: Obtener Evento por ID
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(EventoDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            try
            {
                var evento = await _eventoService.ObtenerEventoPorIdAsync(id);
                return Ok(evento);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        
        // RF02: Obtener listado de eventos públicos
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<EventoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPublicEvents()
        {
            var eventos = await _eventoService.ObtenerEventosPublicosAsync();
            return Ok(eventos);
        }

        // RF02: Actualizar Evento (Solo dueños y Admins)
        [HttpPut("{id}")]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventoCreacionDto dto)
        {
            try
            {
                await _eventoService.ActualizarEventoAsync(id, dto, ObtenerUsuarioId(), ObtenerRol());
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DomainException ex)
            {
                return Forbid(ex.Message); // 403 Forbidden
            }
        }
    }
}