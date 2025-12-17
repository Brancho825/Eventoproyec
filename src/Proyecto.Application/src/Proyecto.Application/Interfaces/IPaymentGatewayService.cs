namespace Proyecto.Application.Interfaces
{
    // Gateway de pagos simulado (RF03, RF04)
    public interface IPaymentGatewayService
    {
        // Crea una intención de pago
        Task<bool> CrearIntentoDePagoAsync(decimal monto, Guid reservaId);
        // ...
    }
}