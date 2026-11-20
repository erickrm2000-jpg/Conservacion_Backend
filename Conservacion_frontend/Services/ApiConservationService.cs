using System.Net.Http.Headers;
using System.Net.Http.Json;
using Conservacion_frontend.Models;

namespace Conservacion_frontend.Services;

public sealed class ApiConservationService(
    IHttpClientFactory httpClientFactory,
    IHttpContextAccessor httpContextAccessor) : IConservationService
{
    private const string TokenKey = "ConservacionApiToken";

    private HttpClient CreateClient()
    {
        var client = httpClientFactory.CreateClient("ConservacionAPI");
        var token = httpContextAccessor.HttpContext?.Session.GetString(TokenKey);
        if (!string.IsNullOrWhiteSpace(token))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return client;
    }

    public async Task<LoginResult?> LoginAsync(string usuario, string contrasena)
    {
        var response = await httpClientFactory.CreateClient("ConservacionAPI").PostAsJsonAsync(
            "api/auth/login",
            new { nombreUsuario = usuario, contrasena });

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<LoginResult>();
        if (result is not null)
            httpContextAccessor.HttpContext?.Session.SetString(TokenKey, result.Token);

        return result;
    }

    public void Logout() => httpContextAccessor.HttpContext?.Session.Remove(TokenKey);

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        var areas = await GetAsync<AreaConservacion>("api/areas");
        var especies = await GetAsync<Especie>("api/especies");
        var personal = await GetAsync<Personal>("api/personal");
        var actividades = await GetAsync<ActividadConservacion>("api/actividades");
        var amenazas = await GetAsync<Amenaza>("api/amenazas");

        return new DashboardViewModel
        {
            TotalAreas = areas.Count,
            TotalEspecies = especies.Count,
            TotalPersonal = personal.Count,
            AmenazasActivas = amenazas.Count,
            ActividadesRecientes = actividades.Take(3).ToList()
        };
    }

    public Task<IReadOnlyList<AreaConservacion>> GetAreasAsync() => GetAsync<AreaConservacion>("api/areas");
    public Task<IReadOnlyList<Especie>> GetEspeciesAsync() => GetAsync<Especie>("api/especies");
    public Task<IReadOnlyList<Personal>> GetPersonalAsync() => GetAsync<Personal>("api/personal");
    public Task<IReadOnlyList<ActividadConservacion>> GetActividadesAsync() => GetAsync<ActividadConservacion>("api/actividades");
    public Task<IReadOnlyList<MonitoreoAmbiental>> GetMonitoreosAsync() => GetAsync<MonitoreoAmbiental>("api/monitoreos");
    public Task<IReadOnlyList<Amenaza>> GetAmenazasAsync() => GetAsync<Amenaza>("api/amenazas");

    private async Task<IReadOnlyList<T>> GetAsync<T>(string endpoint)
    {
        var result = await CreateClient().GetFromJsonAsync<List<T>>(endpoint);
        return result ?? [];
    }
}
