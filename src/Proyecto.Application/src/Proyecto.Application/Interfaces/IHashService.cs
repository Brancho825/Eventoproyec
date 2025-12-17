namespace Proyecto.Application.Interfaces
{
    public interface IHashService
    {
        string HashPassword(string password);
        bool VerificarPassword(string password, string hashedPassword);
    }
}