using Microsoft.AspNetCore.Mvc;

namespace Conservacion_frontend.Controllers;

public sealed class CrudController : Controller
{
    [HttpGet]
    public IActionResult Create(string modulo) => View(new CrudViewModel { Modulo = modulo });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CrudViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        TempData["Mensaje"] = $"Registro de {model.Modulo} preparado. La persistencia se conectarÃ¡ a la API.";
        return RedirectToAction(model.Modulo, "Catalog");
    }
}

public sealed class CrudViewModel
{
    [System.ComponentModel.DataAnnotations.Required]
    public string Modulo { get; set; } = string.Empty;
    [System.ComponentModel.DataAnnotations.Required]
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
}

