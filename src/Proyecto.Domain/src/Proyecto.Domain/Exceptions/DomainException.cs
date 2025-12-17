namespace Proyecto.Domain.Exceptions
{
    // Clase base para excepciones controladas del dominio o reglas de negocio.
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}