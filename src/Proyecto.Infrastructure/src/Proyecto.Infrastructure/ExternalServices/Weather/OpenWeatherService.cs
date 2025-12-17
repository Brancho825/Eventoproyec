using Proyecto.Application.Interfaces;

namespace Proyecto.Infrastructure.ExternalServices.Weather
{
    public class OpenWeatherService : IWeatherService
    {
        public Task<string> ObtenerPronosticoAsync(decimal latitud, decimal longitud)
        {
            // Simulación de respuesta de clima
            return Task.FromResult("Soleado con 25°C");
        }
    }
}