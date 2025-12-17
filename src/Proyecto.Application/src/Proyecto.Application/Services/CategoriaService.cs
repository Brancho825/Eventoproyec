using Proyecto.Application.DTOs;
using Proyecto.Application.Interfaces;
using Proyecto.Domain.Entities;
using Proyecto.Domain.Exceptions;
using AutoMapper;

namespace Proyecto.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<CategoriaDto> CrearCategoriaAsync(CategoriaDto dto)
        {
            var nuevaCategoria = _mapper.Map<Categoria>(dto);
            nuevaCategoria.Id = Guid.NewGuid();
            nuevaCategoria.EstaActiva = true;

            await _categoriaRepository.AgregarAsync(nuevaCategoria);
            return _mapper.Map<CategoriaDto>(nuevaCategoria);
        }

        public async Task ActualizarCategoriaAsync(Guid id, CategoriaDto dto)
        {
            var categoria = await _categoriaRepository.ObtenerPorIdAsync(id);
            if (categoria == null) throw new NotFoundException(nameof(Categoria), id);

            categoria.Nombre = dto.Nombre;
            categoria.Descripcion = dto.Descripcion;

            await _categoriaRepository.ActualizarAsync(categoria);
        }

        public async Task EliminarCategoriaAsync(Guid id)
        {
            var categoria = await _categoriaRepository.ObtenerPorIdAsync(id);
            if (categoria == null) throw new NotFoundException(nameof(Categoria), id);

            // Desactivar en lugar de eliminar
            categoria.EstaActiva = false;
            await _categoriaRepository.ActualizarAsync(categoria);
        }
        
        public async Task<IEnumerable<CategoriaDto>> ObtenerTodasAsync()
        {
            var categorias = await _categoriaRepository.ObtenerTodosAsync();
            return _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
        }
    }
}