namespace Proyecto.Application.DTOs
{
    // DTO para creación y visualización de Categorías
    public class CategoriaDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}