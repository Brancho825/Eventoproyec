namespace Proyecto.Domain.Enums
{
    // Corresponde a la especificación de Roles (admin, organizador, usuario)
    public enum Rol
    {
        Admin = 1, 
        Organizer = 2, // Se mantiene el nombre en inglés para la autorización JWT
        User = 3 // Se mantiene el nombre en inglés para la autorización JWT
    }
}