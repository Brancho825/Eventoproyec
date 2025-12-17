using Proyecto.Application.DTOs;

namespace Proyecto.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<CategoriaDto> CrearCategoriaAsync(CategoriaDto dto); 
        Task ActualizarCategoriaAsync(Guid id, CategoriaDto dto); 
        Task EliminarCategoriaAsync(Guid id); 
        Task<IEnumerable<CategoriaDto>> ObtenerTodasAsync(); 
    }
}