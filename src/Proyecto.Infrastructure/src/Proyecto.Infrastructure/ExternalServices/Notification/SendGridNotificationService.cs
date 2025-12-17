using Proyecto.Application.Interfaces;

namespace Proyecto.Infrastructure.ExternalServices.Notification
{
    public class SendGridNotificationService : IExternalNotificationService
    {
        public Task EnviarNotificacionAsync(Guid usuarioId, string asunto, string contenido)
        {
            // Simulación de envío de notificación
            return Task.CompletedTask;
        }
    }
}