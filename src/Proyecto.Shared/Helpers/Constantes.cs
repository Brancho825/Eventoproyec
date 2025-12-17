namespace Proyecto.Shared.Helpers
{
    public static class Constantes
    {
        // Límite máximo para reservas por evento o usuario (Regla de negocio blanda)
        public const int MaxReservasPorUsuario = 5;
        
        // Tiempo de expiración del token (para JWT, si no se lee desde appsettings)
        public const int JwtTokenExpirationMinutes = 60; 

        // Roles fijos para asegurar consistencia
        public const string RolAdmin = "Admin";
        public const string RolOrganizer = "Organizer";
    }
}