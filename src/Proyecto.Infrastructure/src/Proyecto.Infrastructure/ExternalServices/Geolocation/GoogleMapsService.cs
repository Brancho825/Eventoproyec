using Proyecto.Application.Interfaces;

namespace Proyecto.Infrastructure.ExternalServices.Geolocation
{
    public class GoogleMapsService : IGeolocalizacionService
    {
        public Task<(decimal latitud, decimal longitud)> GeocodificarAsync(string direccion)
        {
            // Simulación de respuesta de geocodificación
            return Task.FromResult((latitud: 10.0m, longitud: -60.0m));
        }
    }
}