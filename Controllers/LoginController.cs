using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SysCoco._0.Models;
using SysCoco._0.Services;
using System.Security.Claims;

namespace SysCoco._0.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly syscocoContext _context;
        private readonly IUsuarioService _usuarioService;

        public LoginController(syscocoContext context, IUsuarioService usuarioService)
        {
            _context = context;
            _usuarioService = usuarioService;
        }

        // Registro de nuevos usuarios
        [HttpPost("registro")]
        public async Task<IActionResult> CrearUsuarios([FromBody] Usuarios usuario)
        {
            var rolExistente = await _context.Roles.FindAsync(usuario.rolesid); // Aquí debe buscarse por rolesid, no por roles
            if (rolExistente == null)
            {
                return BadRequest("El rol especificado no existe.");
            }

            // Encriptar la contraseña del usuario
            usuario.contraseña = Utilidades.EncriptarClave(usuario.contraseña);
            usuario.roles = rolExistente;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(usuario);
        }

        // Inicio de sesión de usuarios
        [HttpPost("iniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] IniciarSesionRequest request)
        {
            if (string.IsNullOrEmpty(request.correo) || string.IsNullOrEmpty(request.contraseña))
            {
                return BadRequest("Por favor, complete todos los campos.");
            }

            // Corregido: coma entre los parámetros
            var usuario = await _usuarioService.GetUsuario(request.correo, request.contraseña);

            if (usuario == null)
            {
                return Unauthorized("Nombre de usuario o contraseña incorrectos.");
            }

            // Obtener el nombre del rol
            var nombreRol = await _usuarioService.GetRolNombrePorUsuario(usuario.id);

            if (nombreRol == null)
            {
                return StatusCode(500, "Error al obtener el rol del usuario.");
            }


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.nombres),
                new Claim(ClaimTypes.Role, nombreRol),
            };


            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Ok(new { Message = "Inicio de sesión exitoso", usuario });
        }

        // Cerrar sesión
        [HttpPost("cerrarSesion")]
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { Message = "Sesión cerrada exitosamente" });
        }
    }

    // Clase para manejar la solicitud de inicio de sesió
    public class IniciarSesionRequest
    {
        public string correo { get; set; }
        public string contraseña { get; set; }
    }
}