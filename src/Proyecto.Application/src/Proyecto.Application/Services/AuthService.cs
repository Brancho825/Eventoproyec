using Proyecto.Application.Interfaces;
using Proyecto.Application.DTOs;
using Proyecto.Domain.Entities;
using Proyecto.Domain.Enums;
using Proyecto.Domain.Exceptions;

namespace Proyecto.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IHashService _hashService;
        private readonly IJwtService _jwtService; 

        public AuthService(IUsuarioRepository usuarioRepository, IHashService hashService, IJwtService jwtService)
        {
            _usuarioRepository = usuarioRepository;
            _hashService = hashService;
            _jwtService = jwtService;
        }

        public async Task<bool> RegistrarUsuarioAsync(UsuarioRegistroDto registroDto)
        {
            if (await _usuarioRepository.EmailExisteAsync(registroDto.Email))
            {
                throw new ValidationException("El correo electrónico ya está registrado.");
            }

            var passwordHash = _hashService.HashPassword(registroDto.Contrasena);

            var nuevoUsuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Email = registroDto.Email,
                ContrasenaHash = passwordHash, 
                Nombre = registroDto.Nombre,
                Apellido = registroDto.Apellido,
                Rol = Rol.User, 
                EstaActivo = true,
                CreadoEn = DateTime.UtcNow,
                ActualizadoEn = DateTime.UtcNow
            };

            await _usuarioRepository.AgregarAsync(nuevoUsuario);
            return true;
        }

        public async Task<AuthResponseDto> LoginAsync(string email, string contrasena)
        {
            var usuario = await _usuarioRepository.ObtenerPorEmailAsync(email);

            if (usuario == null || !usuario.EstaActivo)
            {
                throw new NotFoundException("Credenciales inválidas o usuario inactivo.");
            }
            
            var esValido = _hashService.VerificarPassword(contrasena, usuario.ContrasenaHash);

            if (!esValido)
            {
                throw new ValidationException("Credenciales inválidas.");
            }

            return _jwtService.GenerarTokens(usuario);
        }
        
        public Task<AuthResponseDto> RefrescarTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}