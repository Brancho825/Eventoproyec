namespace Proyecto.Domain.Exceptions
{
    // Excepción para recursos que no existen (idealmente mapeada a HTTP 404).
    public class NotFoundException : DomainException
    {
        public NotFoundException(string name, object key)
            : base($"La entidad \"{name}\" ({key}) no fue encontrada.")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
}