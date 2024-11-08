using SysCoco._0.Models;

namespace SysCoco._0.Services
{
    public interface IUsuarioService
    {
        Task<Usuarios?> GetUsuario(string nombreUsuario, string contraseña);
        Task<string?> GetRolNombrePorUsuario(int usuarioId);
    }

}

