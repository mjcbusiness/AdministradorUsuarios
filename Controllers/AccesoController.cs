using AdministradorUsuarios.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AdministradorUsuarios.Controllers
{
    public class AccesoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Entrar(string rol)
        {
            // Validación simple
            if (rol != Roles.Admin && rol != Roles.User)
            {
                TempData["Error"] = "Rol inválido.";
                return RedirectToAction(nameof(Index));
            }

            HttpContext.Session.SetString(Roles.SessionKey, rol);
            return RedirectToAction("Index", "Usuarios");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Salir()
        {
            HttpContext.Session.Remove(Roles.SessionKey);
            return RedirectToAction(nameof(Index));
        }
    }
}
