using Conservacion_frontend.Models;

namespace Conservacion_frontend.Services;

public interface IConservationService
{
    Task<LoginResult?> LoginAsync(string usuario, string contrasena);
    void Logout();
    Task<DashboardViewModel> GetDashboardAsync();
    Task<IReadOnlyList<AreaConservacion>> GetAreasAsync();
    Task<IReadOnlyList<Especie>> GetEspeciesAsync();
    Task<IReadOnlyList<Personal>> GetPersonalAsync();
    Task<IReadOnlyList<ActividadConservacion>> GetActividadesAsync();
    Task<IReadOnlyList<MonitoreoAmbiental>> GetMonitoreosAsync();
    Task<IReadOnlyList<Amenaza>> GetAmenazasAsync();
}

