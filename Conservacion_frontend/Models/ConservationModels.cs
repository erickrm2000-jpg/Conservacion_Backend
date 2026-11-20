using System.ComponentModel.DataAnnotations;

namespace Conservacion_frontend.Models;

public sealed class AreaConservacion
{
    public int IdArea { get; set; }
    [Required, Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;
    [Required, Display(Name = "UbicaciÃ³n")]
    public string Ubicacion { get; set; } = string.Empty;
    [Range(0.01, double.MaxValue), Display(Name = "ExtensiÃ³n (hectÃ¡reas)")]
    public decimal ExtensionHectareas { get; set; }
    [Required, Display(Name = "Tipo de Ã¡rea")]
    public string TipoArea { get; set; } = string.Empty;
    [DataType(DataType.Date), Display(Name = "Fecha de creaciÃ³n")]
    public DateTime FechaCreacion { get; set; }
    public string Estado { get; set; } = "Activa";
}

public sealed class Especie
{
    public int IdEspecie { get; set; }
    [Required, Display(Name = "Nombre cientÃ­fico")]
    public string NombreCientifico { get; set; } = string.Empty;
    [Required, Display(Name = "Nombre comÃºn")]
    public string NombreComun { get; set; } = string.Empty;
    [Required]
    public string Tipo { get; set; } = string.Empty;
    [Required, Display(Name = "Estado de conservaciÃ³n")]
    public string EstadoConservacion { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
}

public sealed class Personal
{
    public int IdPersonal { get; set; }
    [Required] public string Nombre { get; set; } = string.Empty;
    [Required] public string Cargo { get; set; } = string.Empty;
    public string? Especialidad { get; set; }
    public string? Telefono { get; set; }
    [EmailAddress] public string? Correo { get; set; }
}

public sealed class ActividadConservacion
{
    public int IdActividad { get; set; }
    [Required] public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    [DataType(DataType.Date), Display(Name = "Fecha de inicio")]
    public DateTime FechaInicio { get; set; }
    [DataType(DataType.Date), Display(Name = "Fecha de fin")]
    public DateTime? FechaFin { get; set; }
    public int IdArea { get; set; }
    public int IdPersonal { get; set; }
    public string Area { get; set; } = string.Empty;
    public string Responsable { get; set; } = string.Empty;
}

public sealed class MonitoreoAmbiental
{
    public int IdMonitoreo { get; set; }
    [DataType(DataType.Date)] public DateTime Fecha { get; set; }
    [Required, Display(Name = "Tipo de monitoreo")]
    public string TipoMonitoreo { get; set; } = string.Empty;
    [Required] public string Resultados { get; set; } = string.Empty;
    public int IdArea { get; set; }
    public string Area { get; set; } = string.Empty;
}

public sealed class Amenaza
{
    public int IdAmenaza { get; set; }
    [Required] public string Tipo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    [Required, Display(Name = "Nivel de riesgo")]
    public string NivelRiesgo { get; set; } = string.Empty;
    [DataType(DataType.Date), Display(Name = "Fecha de reporte")]
    public DateTime FechaReporte { get; set; }
    public int IdArea { get; set; }
    public string Area { get; set; } = string.Empty;
}

public sealed class DashboardViewModel
{
    public int TotalAreas { get; set; }
    public int TotalEspecies { get; set; }
    public int TotalPersonal { get; set; }
    public int AmenazasActivas { get; set; }
    public IReadOnlyList<ActividadConservacion> ActividadesRecientes { get; set; } = [];
}

public sealed class LoginViewModel
{
    [Required, Display(Name = "Usuario")]
    public string Usuario { get; set; } = string.Empty;
    [Required, DataType(DataType.Password), Display(Name = "ContraseÃ±a")]
    public string Contrasena { get; set; } = string.Empty;
    public string? ReturnUrl { get; set; }
}

public sealed class LoginResult
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiracion { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
}

