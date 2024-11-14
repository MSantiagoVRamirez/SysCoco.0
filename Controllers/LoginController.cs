using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SysCoco._0.Models;
using SysCoco._0.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SysCoco._0.Controllers
{
    public class LoginController : Controller
    {
        private readonly syscocoContext _context;
        private readonly IUsuarioService _usuarioService;

        public LoginController(syscocoContext context, IUsuarioService usuarioService)
        {
            _context = context;
            _usuarioService = usuarioService;
        }

        // Acción para mostrar el formulario de registro
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        // Registro de nuevos usuarios
        [HttpPost]
        public async Task<IActionResult> Registro(Usuarios usuario)
        {
            var rolExistente = await _context.Roles.FindAsync(usuario.rolesid);
            if (rolExistente == null)
            {
                ModelState.AddModelError("", "El rol especificado no existe.");
                return View(usuario);
            }

            // Encriptar la contraseña del usuario
            usuario.contraseña = Utilidades.EncriptarClave(usuario.contraseña);
            usuario.roles = rolExistente;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Usuario registrado con éxito.";
            return RedirectToAction("IniciarSesion");
        }

        // Acción para mostrar el formulario de inicio de sesión
        [HttpGet]
        public IActionResult IniciarSesion()
        {
            return View();
        }

        // Inicio de sesión de usuarios
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(IniciarSesionRequest request)
        {
            if (string.IsNullOrEmpty(request.correo) || string.IsNullOrEmpty(request.contraseña))
            {
                ModelState.AddModelError("", "Por favor, complete todos los campos.");
                return View(request);
            }

            var usuario = await _usuarioService.GetUsuario(request.correo, request.contraseña);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                return View(request);
            }

            var nombreRol = await _usuarioService.GetRolNombrePorUsuario(usuario.id);

            if (nombreRol == null)
            {
                ModelState.AddModelError("", "Error al obtener el rol del usuario.");
                return View(request);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.nombres),
                new Claim(ClaimTypes.Role, nombreRol),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            TempData["SuccessMessage"] = "Inicio de sesión exitoso.";
            return RedirectToAction("Index", "Home"); // Redirige al controlador o vista principal después del inicio de sesión
        }

        // Cerrar sesión
        [HttpPost]
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["SuccessMessage"] = "Sesión cerrada exitosamente.";
            return RedirectToAction("IniciarSesion");
        }
    }

    // Clase para manejar la solicitud de inicio de sesión
    public class IniciarSesionRequest
    {
        public string correo { get; set; }
        public string contraseña { get; set; }
    }
}
