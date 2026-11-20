using Microsoft.AspNetCore.Mvc;
using Conservacion_frontend.Services;

namespace Conservacion_frontend.Controllers;

public sealed class CatalogController(IConservationService service) : Controller
{
    public async Task<IActionResult> Areas() => View(await service.GetAreasAsync());
    public async Task<IActionResult> Especies() => View(await service.GetEspeciesAsync());
    public async Task<IActionResult> Personal() => View(await service.GetPersonalAsync());
    public async Task<IActionResult> Actividades() => View(await service.GetActividadesAsync());
    public async Task<IActionResult> Monitoreos() => View(await service.GetMonitoreosAsync());
    public async Task<IActionResult> Amenazas() => View(await service.GetAmenazasAsync());
}

