using Microsoft.EntityFrameworkCore;
using Proyecto.Application.Interfaces;
using Proyecto.Domain.Entities;
using Proyecto.Infrastructure.Persistence.Context;

namespace Proyecto.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> ObtenerTodosAsync() => 
            await _context.Categorias.ToListAsync();

        public async Task<Categoria> ObtenerPorIdAsync(Guid id) => 
            await _context.Categorias.FindAsync(id) ?? throw new InvalidOperationException("Categoría no encontrada");

        public async Task AgregarAsync(Categoria categoria) => await _context.Categorias.AddAsync(categoria);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        // Dentro de la clase CategoriaRepository { ... }

// Implementación de métodos faltantes
public Task ActualizarAsync(Categoria categoria) => throw new NotImplementedException();
public Task EliminarAsync(Categoria categoria) => throw new NotImplementedException();
public async Task<bool> ExisteAsync(Guid id) => await _context.Categorias.AnyAsync(c => c.Id == id);
    }
}