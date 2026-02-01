using AdministradorUsuarios.Domain;

namespace AdministradorUsuarios.Application
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> ListarAsync(string rolActual);
        Task<Usuario?> ObtenerUsuarioPorIdAsync(int id, string rolActual);

        Task<(bool ok, string? error)> CrearAsync(Usuario usuario, string rolActual);
        Task<(bool ok, string? error)> ActualizarAsync(Usuario usuario, string rolActual);
        Task<(bool ok, string? error)> EliminarAsync(int id, string rolActual);
    }
}
