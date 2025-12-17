using Microsoft.EntityFrameworkCore;
using Proyecto.Application.Interfaces;
using Proyecto.Domain.Entities;
using Proyecto.Infrastructure.Persistence.Context;

namespace Proyecto.Infrastructure.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AgregarAsync(Reserva reserva) => await _context.Reservas.AddAsync(reserva);
        
        public async Task<Reserva?> ObtenerPorIdAsync(Guid id) => await _context.Reservas.FindAsync(id);
        
        public async Task ActualizarAsync(Reserva reserva) => _context.Reservas.Update(reserva);
        
        // Método que se usa en ReservaService para verificar la cantidad de reservas
        public async Task<int> ContarReservasPorEventoYUsuarioAsync(Guid eventoId, Guid usuarioId) => 
            await _context.Reservas.CountAsync(r => r.EventoId == eventoId && r.UsuarioId == usuarioId);
        
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}