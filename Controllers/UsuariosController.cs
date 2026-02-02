using AdministradorUsuarios.Application;
using AdministradorUsuarios.Domain;
using AdministradorUsuarios.Infrastructure;
using AdministradorUsuarios.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdministradorUsuarios.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IRolActual _rolActual;
        public UsuariosController(IUsuarioService usuarioService, IRolActual rolActual)
        {
            _usuarioService = usuarioService;
            _rolActual = rolActual;
        }

        public async Task<IActionResult> Index()
        {
            var role = _rolActual.ObtenerRol();
            ViewBag.CurrentRole = role;

            var users = await _usuarioService.ListarAsync(role);
            return View(users);
        }

        public IActionResult Create()
        {
            var role = _rolActual.ObtenerRol();
            if (role != "Administrador") return Forbid();
            ViewBag.CurrentRole = _rolActual.ObtenerRol();
            return View(new UsuarioFormVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioFormVm vm)
        {
            var role = _rolActual.ObtenerRol();

            ViewBag.CurrentRole = role;

            if (!ModelState.IsValid) return View(vm);

            var u = new Usuario
            {
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Documento = vm.Documento,
                Email = vm.Email,
                Rol = vm.Rol
            };

            var result = await _usuarioService.CrearAsync(u, role);
            if (!result.ok)
            {
                ModelState.AddModelError(string.Empty, result.error!);
                return View(vm);
            }

            TempData["Success"] = "Usuario creado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var role = _rolActual.ObtenerRol();
            ViewBag.CurrentRole = role;

            var u = await _usuarioService.ObtenerUsuarioPorIdAsync(id, role);
            if (u is null) return NotFound();

            return View(new UsuarioFormVm
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Documento = u.Documento,
                Email = u.Email,
                Rol = u.Rol
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuarioFormVm vm)
        {
            var role = _rolActual.ObtenerRol();
            ViewBag.CurrentRole = role;

            if (!ModelState.IsValid) return View(vm);

            var u = new Usuario
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Documento = vm.Documento,
                Email = vm.Email,
                Rol = vm.Rol
            };

            var result = await _usuarioService.ActualizarAsync(u, role);
            if (!result.ok)
            {
                ModelState.AddModelError(string.Empty, result.error!);
                return View(vm);
            }

            TempData["Success"] = "Usuario actualizado.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var role = _rolActual.ObtenerRol();
            var result = await _usuarioService.EliminarAsync(id, role);

            if (!result.ok)
            {
                TempData["Error"] = result.error!;
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Usuario eliminado.";
            return RedirectToAction(nameof(Index));
        }
    }
}
