namespace Proyecto.Application.Interfaces
{
    // Servicio para geocodificación de eventos (RF02, RF04)
    public interface IGeolocalizacionService
    {
        Task<(decimal latitud, decimal longitud)> GeocodificarAsync(string direccion);
        // ...
    }
}