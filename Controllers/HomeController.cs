// Controllers/HomeController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize] // Asegúrate de que solo los usuarios autenticados puedan acceder a este controlador
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}