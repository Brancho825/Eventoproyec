using Proyecto.Application.Interfaces;
using BCrypt.Net;
using System;

namespace Proyecto.Infrastructure.Security
{
    public class HashService : IHashService
    {
        public string HashPassword(string password)
        {
            // Usamos la configuración por defecto de sal (salt) y complejidad
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerificarPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword))
            {
                return false;
            }
            try
            {
                // Verifica la contraseña plana contra el hash almacenado
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch (Exception)
            {
                // Manejo de errores si el hash es inválido
                return false;
            }
        }
    }
}