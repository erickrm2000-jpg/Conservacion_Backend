using Microsoft.AspNetCore.Mvc;
using Conservacion_frontend.Models;
using Conservacion_frontend.Services;

namespace Conservacion_frontend.Controllers;

public sealed class HomeController(IConservationService service) : Controller
{
    public async Task<IActionResult> Index() => View(await service.GetDashboardAsync());
}

