using AutoMapper;
using Proyecto.Domain.Entities;
using Proyecto.Application.DTOs;

namespace Proyecto.Application.Mappings
{
    public class MapeoPerfil : Profile
    {
        public MapeoPerfil()
        {
            // === Usuarios ===
            CreateMap<UsuarioRegistroDto, Usuario>()
                .ForMember(dest => dest.ContrasenaHash, opt => opt.Ignore()) 
                .ForMember(dest => dest.Rol, opt => opt.Ignore()); 
            
            CreateMap<Usuario, UsuarioDto>();
            
            // === Categorías ===
            CreateMap<Categoria, CategoriaDto>();
            CreateMap<CategoriaDto, Categoria>();

            // === Eventos ===
            CreateMap<EventoCreacionDto, Evento>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Estado, opt => opt.Ignore()) 
                .ForMember(dest => dest.CuposDisponibles, opt => opt.Ignore()) 
                .ForMember(dest => dest.OrganizadorId, opt => opt.Ignore());

            CreateMap<Evento, EventoDto>()
                .ForMember(dest => dest.NombreOrganizador, 
                           opt => opt.MapFrom(src => src.Organizador.Nombre + " " + src.Organizador.Apellido))
                .ForMember(dest => dest.NombreCategoria, 
                           opt => opt.MapFrom(src => src.Categoria.Nombre));

            // === Reservas ===
            // CORRECCIÓN: Aseguramos que las DTOs y Entidades se resuelvan correctamente.
            CreateMap<ReservaCreacionDto, Reserva>()
                .ForMember(dest => dest.UsuarioId, opt => opt.Ignore())
                .ForMember(dest => dest.MontoTotal, opt => opt.Ignore())
                .ForMember(dest => dest.Estado, opt => opt.Ignore());

            CreateMap<Reserva, ReservaDto>()
                .ForMember(dest => dest.TituloEvento, opt => opt.MapFrom(src => src.Evento.Titulo))
                .ForMember(dest => dest.FechaEvento, opt => opt.MapFrom(src => src.Evento.FechaInicio));
        }
    }
}