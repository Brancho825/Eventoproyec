using Microsoft.EntityFrameworkCore;
using Proyecto.Application.Interfaces;
using Proyecto.Domain.Entities;
using Proyecto.Infrastructure.Persistence.Context;

namespace Proyecto.Infrastructure.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly ApplicationDbContext _context;

        public EventoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementación de métodos básicos de IEventoRepository
        public async Task AgregarAsync(Evento evento) => await _context.Eventos.AddAsync(evento);
        public async Task ActualizarAsync(Evento evento) => _context.Eventos.Update(evento);
        public async Task<Evento> ObtenerPorIdAsync(Guid id) => 
            await _context.Eventos.Include(e => e.Reservas).FirstOrDefaultAsync(e => e.Id == id) ?? throw new InvalidOperationException("Evento no encontrado");
        
        // Asume la existencia del método para el ReporteService
        public async Task<IEnumerable<Evento>> ObtenerEventosPorOrganizadorAsync(Guid organizadorId) => 
            await _context.Eventos.Where(e => e.OrganizadorId == organizadorId).ToListAsync();

        public async Task<IEnumerable<Evento>> ObtenerEventosPublicosAsync() => 
            await _context.Eventos.Where(e => e.EsPublico && e.FechaInicio >= DateTime.Now).ToListAsync();
        
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        // Dentro de la clase EventoRepository { ... }

// Implementación de métodos faltantes
public async Task<IEnumerable<Evento>> ObtenerTodosAsync() => await _context.Eventos.ToListAsync();
public Task EliminarAsync(Evento evento) => throw new NotImplementedException();
public async Task<bool> ExisteAsync(Guid id) => await _context.Eventos.AnyAsync(e => e.Id == id);
    }
}