namespace Proyecto.Domain.Entities
{
    public class Perfil
    {
        public Guid Id { get; set; } 
        
        // Propiedades de texto
        public string Telefono { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; } // Acepta nulos, por eso no genera warning
        
        // Propiedad de navegación
        public Usuario Usuario { get; set; } = null!;
    }
}