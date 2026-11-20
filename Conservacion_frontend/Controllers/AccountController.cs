using Microsoft.AspNetCore.Mvc;
using Conservacion_frontend.Models;
using Conservacion_frontend.Services;

namespace Conservacion_frontend.Controllers;

public sealed class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login(string? returnUrl = null) => View(new LoginViewModel { ReturnUrl = returnUrl });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, [FromServices] IConservationService service)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.Usuario == "admin" && model.Contrasena == "Admin123*")
        {
            TempData["Mensaje"] = "SesiÃ³n iniciada correctamente. Este acceso serÃ¡ conectado al JWT de la API.";
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Usuario o contraseÃ±a incorrectos.");
        return View(model);
    }

    public IActionResult Logout([FromServices] IConservationService service)
    {
        service.Logout();
        return RedirectToAction(nameof(Login));
    }
}

