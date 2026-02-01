using AdministradorUsuarios.Domain;

namespace AdministradorUsuarios.Infrastructure
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ObtenerUsuariosAsync();
        Task<IEnumerable<Usuario>> ObtenerUsuariosPorRolAsync(string rol);
        Task<Usuario?> ObtenerUsuarioPorIdAsync(int id);   
        Task<int> CrearAsync(Usuario usuario);
        Task ActualizarAsync (Usuario usuario);
        Task EliminarAsync(int id);
        Task<bool> ExisteEmailAsync(string email,int? id =null);

    }
}
