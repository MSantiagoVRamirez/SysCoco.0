using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SysCoco._0.Models;
using System.Security.Claims;
using SysCoco._0.Services;

namespace SysCoco._0.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IFilesService _FileService;
        private readonly syscocoContext _context;

        public LoginController(IUsuarioService usuarioService, IFilesService filesService, syscocoContext context)
        {
            _usuarioService = usuarioService;
            _FileService = filesService;
            _context = context;
        }

        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registro(Usuarios usuario, IFormFile Imagen)
        {
            if (Imagen == null || Imagen.Length == 0)
            {
                ViewData["Mensaje"] = "Debe cargar una imagen.";
                return View(usuario);
            }

            if (usuario.rolesid < 1 || usuario.rolesid > 4)
            {
                ViewData["Mensaje"] = "El rol seleccionado no es válido. Debe ser 1 (Administrador), 2 (Cliente) o 3 (Empresa).";
                return View(usuario);
            }

            Stream image = Imagen.OpenReadStream();
            string urlImagen = await _FileService.SubirArchivo(image, Imagen.FileName);

            usuario.Contraseña = Utilidades.EncriptarClave(usuario.Contraseña);
            usuario.fotoPerfil = urlImagen;
            usuario.FechaCreacion = DateTime.Now;
            usuario.FechaExpira = DateTime.Now.AddYears(1);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                ViewData["Mensaje"] = "Todos los campos obligatorios deben ser completados correctamente.";
                return View(usuario);
            }

            Usuarios usuarioCreado = await _usuarioService.SaveUsuario(usuario);

            if (usuarioCreado.Id > 0)
            {
                return View("~/Views/Login/RegistroExitoso.cshtml");
            }

            ViewData["Mensaje"] = "No se pudo crear el usuario.";
            return View(usuario);
        }


        [HttpGet]
        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string NombreUsuario, string Contraseña)
        {
            if (string.IsNullOrEmpty(NombreUsuario) || string.IsNullOrEmpty(Contraseña))
            {
                ViewData["Mensaje"] = "Por favor, complete todos los campos.";
                return View();
            }

            var usuario = await _usuarioService.GetUsuario(NombreUsuario, Contraseña);

            if (usuario == null)
            {
                ViewData["Mensaje"] = "Nombre de usuario o contraseña incorrectos.";
                return View();
            }


            var nombreRol = await _usuarioService.GetRolNombrePorUsuario(usuario.Id);

            if (nombreRol == null)
            {
                ViewData["Mensaje"] = "Error al obtener el rol del usuario.";
                return View();
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, usuario.NombreUsuario),
        new Claim(ClaimTypes.Role, nombreRol),
        new Claim("fotoPerfil", usuario.fotoPerfil ?? string.Empty)
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("CerrarSesion")]
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSesion", "Login");
        }
    }
}