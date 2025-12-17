namespace Proyecto.Application.Interfaces
{
    // API de clima para eventos (RF02, RF04)
    public interface IWeatherService
    {
        Task<string> ObtenerPronosticoAsync(decimal latitud, decimal longitud);
        // ...
    }
}