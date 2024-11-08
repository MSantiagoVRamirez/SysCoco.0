using static SysCoco._0.Services.UsuarioService;
using SysCoco._0.Models;
using Microsoft.EntityFrameworkCore;

namespace SysCoco._0.Services
{
    public class UsuarioService : IUsuarioService
    {
       
            private readonly syscocoContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UsuarioService(syscocoContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }


            public async Task<Usuarios?> GetUsuario(string correo, string contraseña)
            {
                string contraseñaEncriptada = Utilidades.EncriptarClave(contraseña);

                var usuario = await _context.Usuarios
                    .Include(u => u.roles)
                    .FirstOrDefaultAsync(u => u.correo == correo && u.contraseña == contraseñaEncriptada);

                return usuario;
            }


            public async Task<string?> GetRolNombrePorUsuario(int usuarioId)
            {
                var usuario = await _context.Usuarios
                    .Include(u => u.roles)
                    .FirstOrDefaultAsync(u => u.id == usuarioId);


                return usuario?.roles?.Nombre ?? "Rol no especificado";
            }


        }
    }
