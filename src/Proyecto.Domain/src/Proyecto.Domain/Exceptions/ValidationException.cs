using System.Collections.Generic;

namespace Proyecto.Domain.Exceptions
{
    // Excepción para errores de validación de negocio (idealmente mapeada a HTTP 400).
    public class ValidationException : DomainException
    {
        // Diccionario para guardar fallas específicas { "Campo": ["Error 1", "Error 2"] }
        public IDictionary<string, string[]> Failures { get; }

        public ValidationException()
            : base("Ocurrieron uno o más errores de validación.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(IDictionary<string, string[]> failures)
            : this()
        {
            Failures = failures;
        }

        public ValidationException(string message) : base(message)
        {
            Failures = new Dictionary<string, string[]>();
        }
    }
}