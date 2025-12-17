namespace Proyecto.Application.Interfaces
{
    public interface IExternalNotificationService
    {
        Task EnviarNotificacionAsync(Guid usuarioId, string asunto, string contenido);
    }
}