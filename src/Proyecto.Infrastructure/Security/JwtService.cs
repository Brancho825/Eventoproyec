using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Proyecto.Application.DTOs;
using Proyecto.Application.Interfaces;
using Proyecto.Domain.Entities;
using Proyecto.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Proyecto.Infrastructure.Security
{
    public class JwtService : IJwtService
    {
        
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerarAccessToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSettings:SecretKey").Value!);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerarRefreshToken()
        {
            // Implementación simple de un refresh token para fines de prueba
            return Guid.NewGuid().ToString();
        }

        public AuthResponseDto GenerarTokens(Usuario usuario)
        {
            var accessToken = GenerarAccessToken(usuario);
            var refreshToken = GenerarRefreshToken();

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = 3600 // 1 hora en segundos
            };
        }

        public (Guid? UsuarioId, string Rol) ValidarAccessToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSettings:SecretKey").Value!);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var usuarioId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var rol = jwtToken.Claims.First(x => x.Type == ClaimTypes.Role).Value;

                return (Guid.Parse(usuarioId), rol);
            }
            catch
            {
                return (null, string.Empty);
            }
            
        }
    }
}