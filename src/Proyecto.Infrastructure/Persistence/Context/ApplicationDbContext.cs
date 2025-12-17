using Microsoft.EntityFrameworkCore;
using Proyecto.Domain.Entities;

namespace Proyecto.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Evento> Eventos { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Reserva> Reservas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Perfil>()
                .HasOne(p => p.Usuario)
                .WithOne(u => u.Perfil)
                .HasForeignKey<Perfil>(p => p.Id);

            base.OnModelCreating(modelBuilder);
            

            // Configuración clave: Asegura que el Email sea único en la tabla Usuarios
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();
            
            // Relaciones
            modelBuilder.Entity<Evento>()
                .HasOne(e => e.Organizador)
                .WithMany(u => u.EventosOrganizados)
                .HasForeignKey(e => e.OrganizadorId);
                
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.MisReservas)
                .HasForeignKey(r => r.UsuarioId);
        }
    }
}