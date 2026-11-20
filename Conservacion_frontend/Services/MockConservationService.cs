using Conservacion_frontend.Models;

namespace Conservacion_frontend.Services;

public sealed class MockConservationService : IConservationService
{
    private readonly List<AreaConservacion> _areas =
    [
        new() { IdArea = 1, Nombre = "Parque Nacional Corcovado", Ubicacion = "PenÃ­nsula de Osa, Puntarenas", ExtensionHectareas = 54539, TipoArea = "Parque Nacional", FechaCreacion = new(1975, 10, 24), Estado = "Activa" },
        new() { IdArea = 2, Nombre = "Reserva BiolÃ³gica Monteverde", Ubicacion = "Monteverde, Puntarenas", ExtensionHectareas = 10500, TipoArea = "Reserva BiolÃ³gica", FechaCreacion = new(1972, 1, 1), Estado = "Activa" },
        new() { IdArea = 3, Nombre = "Parque Nacional Tortuguero", Ubicacion = "PococÃ­, LimÃ³n", ExtensionHectareas = 26156, TipoArea = "Parque Nacional", FechaCreacion = new(1975, 9, 26), Estado = "Activa" },
        new() { IdArea = 4, Nombre = "Refugio Nacional CaÃ±o Negro", Ubicacion = "Los Chiles, Alajuela", ExtensionHectareas = 9969, TipoArea = "Refugio de Vida Silvestre", FechaCreacion = new(1984, 1, 1), Estado = "Activa" }
    ];

    private readonly List<Especie> _especies =
    [
        new() { IdEspecie = 1, NombreCientifico = "Panthera onca", NombreComun = "Jaguar", Tipo = "Fauna", EstadoConservacion = "Casi amenazada", Descripcion = "Mayor felino de AmÃ©rica, depredador tope." },
        new() { IdEspecie = 2, NombreCientifico = "Ara macao", NombreComun = "Lapa roja", Tipo = "Fauna", EstadoConservacion = "PreocupaciÃ³n menor", Descripcion = "Ave de plumaje rojo, amarillo y azul." },
        new() { IdEspecie = 3, NombreCientifico = "Chelonia mydas", NombreComun = "Tortuga verde", Tipo = "Fauna", EstadoConservacion = "En peligro", Descripcion = "Tortuga marina que anida en el Caribe." },
        new() { IdEspecie = 4, NombreCientifico = "Guaiacum sanctum", NombreComun = "GuayacÃ¡n real", Tipo = "Flora", EstadoConservacion = "En peligro", Descripcion = "Ãrbol de madera muy densa." },
        new() { IdEspecie = 5, NombreCientifico = "Cattleya skinneri", NombreComun = "Guaria morada", Tipo = "Flora", EstadoConservacion = "PreocupaciÃ³n menor", Descripcion = "OrquÃ­dea, flor nacional de Costa Rica." }
    ];

    private readonly List<Personal> _personal =
    [
        new() { IdPersonal = 1, Nombre = "MarÃ­a FernÃ¡ndez Rojas", Cargo = "Directora de Ãrea", Especialidad = "GestiÃ³n Ambiental", Telefono = "8811-2233", Correo = "mfernandez@conservacion.cr" },
        new() { IdPersonal = 2, Nombre = "Carlos JimÃ©nez Mora", Cargo = "BiÃ³logo de Campo", Especialidad = "Fauna Silvestre", Telefono = "8822-3344", Correo = "cjimenez@conservacion.cr" },
        new() { IdPersonal = 3, Nombre = "Laura Vargas SolÃ­s", Cargo = "BotÃ¡nica", Especialidad = "Flora Tropical", Telefono = "8833-4455", Correo = "lvargas@conservacion.cr" }
    ];

    private readonly List<ActividadConservacion> _actividades =
    [
        new() { IdActividad = 1, Nombre = "ReforestaciÃ³n con especies nativas", Descripcion = "Siembra de Ã¡rboles nativos en zonas degradadas.", FechaInicio = new(2026, 2, 1), FechaFin = new(2026, 4, 30), IdArea = 1, IdPersonal = 3, Area = "Parque Nacional Corcovado", Responsable = "Laura Vargas SolÃ­s" },
        new() { IdActividad = 2, Nombre = "Conteo de nidos de tortuga", Descripcion = "Recorridos nocturnos para registrar y proteger nidos.", FechaInicio = new(2026, 6, 1), IdArea = 3, IdPersonal = 2, Area = "Parque Nacional Tortuguero", Responsable = "Carlos JimÃ©nez Mora" },
        new() { IdActividad = 3, Nombre = "Patrullaje contra caza ilegal", Descripcion = "Rondas de vigilancia en sectores de alto riesgo.", FechaInicio = new(2026, 1, 15), IdArea = 1, IdPersonal = 1, Area = "Parque Nacional Corcovado", Responsable = "MarÃ­a FernÃ¡ndez Rojas" }
    ];

    private readonly List<MonitoreoAmbiental> _monitoreos =
    [
        new() { IdMonitoreo = 1, Fecha = new(2026, 3, 5), TipoMonitoreo = "Calidad del agua", Resultados = "pH 7.1, oxÃ­geno disuelto 8.2 mg/L. ParÃ¡metros dentro del rango normal.", IdArea = 1, Area = "Parque Nacional Corcovado" },
        new() { IdMonitoreo = 2, Fecha = new(2026, 4, 10), TipoMonitoreo = "Temperatura y humedad", Resultados = "Temperatura promedio 17Â°C, humedad relativa 92%.", IdArea = 2, Area = "Reserva BiolÃ³gica Monteverde" }
    ];

    private readonly List<Amenaza> _amenazas =
    [
        new() { IdAmenaza = 1, Tipo = "Caza ilegal", Descripcion = "Se detectaron trampas en el sector sur del parque.", NivelRiesgo = "Alto", FechaReporte = new(2026, 2, 14), IdArea = 1, Area = "Parque Nacional Corcovado" },
        new() { IdAmenaza = 2, Tipo = "Saqueo de nidos", Descripcion = "ExtracciÃ³n de huevos de tortuga en playas no vigiladas.", NivelRiesgo = "CrÃ­tico", FechaReporte = new(2026, 7, 3), IdArea = 3, Area = "Parque Nacional Tortuguero" },
        new() { IdAmenaza = 3, Tipo = "Incendio forestal", Descripcion = "Riesgo elevado por Ã©poca seca prolongada.", NivelRiesgo = "Medio", FechaReporte = new(2026, 3, 28), IdArea = 2, Area = "Reserva BiolÃ³gica Monteverde" }
    ];

    public Task<LoginResult?> LoginAsync(string usuario, string contrasena) =>
        Task.FromResult<LoginResult?>(null);

    public void Logout()
    {
    }

    public Task<DashboardViewModel> GetDashboardAsync() => Task.FromResult(new DashboardViewModel
    {
        TotalAreas = _areas.Count,
        TotalEspecies = _especies.Count,
        TotalPersonal = _personal.Count,
        AmenazasActivas = _amenazas.Count,
        ActividadesRecientes = _actividades.Take(3).ToList()
    });

    public Task<IReadOnlyList<AreaConservacion>> GetAreasAsync() => Task.FromResult<IReadOnlyList<AreaConservacion>>(_areas);
    public Task<IReadOnlyList<Especie>> GetEspeciesAsync() => Task.FromResult<IReadOnlyList<Especie>>(_especies);
    public Task<IReadOnlyList<Personal>> GetPersonalAsync() => Task.FromResult<IReadOnlyList<Personal>>(_personal);
    public Task<IReadOnlyList<ActividadConservacion>> GetActividadesAsync() => Task.FromResult<IReadOnlyList<ActividadConservacion>>(_actividades);
    public Task<IReadOnlyList<MonitoreoAmbiental>> GetMonitoreosAsync() => Task.FromResult<IReadOnlyList<MonitoreoAmbiental>>(_monitoreos);
    public Task<IReadOnlyList<Amenaza>> GetAmenazasAsync() => Task.FromResult<IReadOnlyList<Amenaza>>(_amenazas);
}

