using Microsoft.AspNetCore.Mvc;
using ERPJULCAR.Models; // Asegúrate de que el modelo Usuario está en esta ruta
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ERPJULCAR.Data;

namespace ERPJULCAR.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario model)
        {
            // Busca el usuario en la base de datos
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.NombreUsuario == model.NombreUsuario && u.Contraseña == model.Contraseña);

            if (usuario != null)
            {
                // Si el usuario es válido, crear los claims para la autenticación
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                    new Claim("UsuarioId", usuario.IdUsuario.ToString())
                };

                // Crear la identidad y principal para la autenticación
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Autenticar al usuario
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Redirige a la página principal u otra página según sea necesario
                return RedirectToAction("Index", "Home");
            }
            
            // Si la autenticación falla, muestra un mensaje de error
            ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            // Cierra la sesión
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
