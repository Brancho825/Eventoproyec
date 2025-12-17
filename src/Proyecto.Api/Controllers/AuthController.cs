using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.DTOs;
using Proyecto.Application.Interfaces;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioRegistroDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegistrarUsuarioAsync(dto);
            
            if (!result)
            {
                return BadRequest(new { Message = "El email ya está registrado o el registro falló." });
            }
            
            return StatusCode(201); // Created
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto dto)
        {
            var authResponse = await _authService.LoginAsync(dto.Email, dto.Contrasena);

            if (authResponse == null)
            {
                return Unauthorized(new { Message = "Credenciales inválidas." });
            }

            return Ok(authResponse);
        }
    }
}