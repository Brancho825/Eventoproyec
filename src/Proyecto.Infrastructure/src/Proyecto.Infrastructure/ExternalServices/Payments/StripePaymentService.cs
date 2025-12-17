using Proyecto.Application.Interfaces;

namespace Proyecto.Infrastructure.ExternalServices.Payments
{
    public class StripePaymentService : IPaymentGatewayService
    {
        public Task<bool> CrearIntentoDePagoAsync(decimal monto, Guid reservaId)
        {
            // Simulación de proceso de pago exitoso
            return Task.FromResult(true); 
        }
    }
}