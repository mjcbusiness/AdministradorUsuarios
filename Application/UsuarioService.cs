using AdministradorUsuarios.Domain;
using AdministradorUsuarios.Infrastructure;

namespace AdministradorUsuarios.Application
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository) 
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<IEnumerable<Usuario>> ListarAsync(string rolActual)
        {
            return  rolActual == "Administrador"
            ? await _usuarioRepository.ObtenerUsuariosAsync()
            : await _usuarioRepository.ObtenerUsuariosPorRolAsync(rolActual);
        }
        public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id, string rolActual)
        {
            var u = await _usuarioRepository.ObtenerUsuarioPorIdAsync(id);
            if (u is null) return null;

            if (rolActual == "Administrador") return u;
            return u.Rol == rolActual ? u : null;
        }
        public async Task<(bool ok, string? error)> CrearAsync(Usuario usuario, string rolActual)
        {
            // Usuario común: no puede asignar roles distintos
            if (rolActual != "Administrador")
                usuario.Rol = rolActual;

            // Validación de email único
            if (await _usuarioRepository.ExisteEmailAsync(usuario.Email))
                return (false, "El email ya está registrado.");

            var id = await _usuarioRepository.CrearAsync(usuario);
            return (true, null);
        }
        public async Task<(bool ok, string? error)> ActualizarAsync(Usuario usuario, string rolActual)
        {
            var u = await _usuarioRepository.ObtenerUsuarioPorIdAsync(usuario.Id);
            if (u is null) return (false, "Usuario inexistente.");

            // Permisos
            if (rolActual != "Administrador")
            {
                if (u.Rol != rolActual)
                    return (false, "No tenés permisos para editar este usuario.");

                // Evitar elevar permisos
                usuario.Rol = u.Rol;
            }

            // Validación email único excluyendo el mismo Id
            if (await _usuarioRepository.ExisteEmailAsync(usuario.Email, usuario.Id))
                return (false, "El email ya está registrado por otro usuario.");

            await _usuarioRepository.ActualizarAsync(usuario);
            return (true, null);
        }

        public async Task<(bool ok, string? error)> EliminarAsync(int id, string rolActual)
        {
            if (rolActual != "Administrador")
                return (false, "No tenés permisos para eliminar usuarios.");

            var existing = await _usuarioRepository.ObtenerUsuarioPorIdAsync(id);
            if (existing is null) return (false, "Usuario inexistente.");

            await _usuarioRepository.EliminarAsync(id);
            return (true, null);
        }


    }
}
