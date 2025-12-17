namespace Proyecto.Domain.Entities
{
    public class Categoria
    {
        public Guid Id { get; set; } 
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool EstaActiva { get; set; } 
        
        // Relación 1:N con Evento
        public ICollection<Evento> Eventos { get; set; } = new List<Evento>();
    }
}