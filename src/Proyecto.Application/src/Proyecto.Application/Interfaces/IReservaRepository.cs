using Proyecto.Domain.Entities;

namespace Proyecto.Application.Interfaces
{
    public interface IReservaRepository
    {
        /// <summary>
        /// Agrega una nueva reserva a la base de datos.
        /// </summary>
        /// <param name="reserva">La entidad Reserva a agregar.</param>
        Task AgregarAsync(Reserva reserva);

        /// <summary>
        /// Actualiza el estado o detalles de una reserva existente.
        /// </summary>
        /// <param name="reserva">La entidad Reserva con los datos actualizados.</param>
        Task ActualizarAsync(Reserva reserva);

        /// <summary>
        /// Obtiene una reserva por su identificador único.
        /// </summary>
        /// <param name="id">El GUID de la reserva.</param>
        /// <returns>La entidad Reserva o null si no se encuentra.</returns>
        Task<Reserva?> ObtenerPorIdAsync(Guid id);

        /// <summary>
        /// Cuenta cuántas reservas tiene un usuario para un evento específico.
        /// Esto se usa para aplicar límites (reglas de negocio).
        /// </summary>
        /// <param name="eventoId">El GUID del evento.</param>
        /// <param name="usuarioId">El GUID del usuario.</param>
        /// <returns>El número de reservas realizadas.</returns>
        Task<int> ContarReservasPorEventoYUsuarioAsync(Guid eventoId, Guid usuarioId);

        /// <summary>
        /// Guarda todos los cambios pendientes en el contexto de la base de datos.
        /// </summary>
        Task SaveChangesAsync();
    }
}