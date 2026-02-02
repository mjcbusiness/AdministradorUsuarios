using AdministradorUsuarios.Domain;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AdministradorUsuarios.Infrastructure
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;
        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        private IDbConnection CrearConexion()
        {
            return new SqlConnection(_connectionString);
        }
        public async Task<IEnumerable<Usuario>> ObtenerUsuariosAsync()
        {
            const string sql = "SELECT * FROM Usuarios WHERE Eliminado=0";
            using var conn = CrearConexion();
            return await conn.QueryAsync<Usuario>(sql);
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosPorRolAsync(string rol)
        {
            const string sql = "SELECT * FROM Usuarios WHERE Rol = @rol AND Eliminado=0";
            using var conn = CrearConexion();
            return await conn.QueryAsync<Usuario>(sql, new { rol});
        }
        public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
        {
            const string sql = "SELECT * FROM Usuarios WHERE Id=@id AND Eliminado=0";
            using var conn = CrearConexion();
            return await conn.QueryFirstOrDefaultAsync<Usuario>(sql,new { id});
        }
        public async Task<int> CrearAsync(Usuario usuario)
        {
            const string sql = @"INSERT INTO Usuarios (Nombre,Apellido,Documento, Email, Rol,Eliminado) 
                                 VALUES (@Nombre,@Apellido,@Documento, @Email, @Rol,0);
                                 SELECT CAST(SCOPE_IDENTITY() as int);";
            using var conn = CrearConexion();   
            return await conn.ExecuteScalarAsync<int>(sql, usuario);
        }
        public async Task ActualizarAsync(Usuario usuario)
        {
            const string sql = @"
            UPDATE dbo.Usuarios
            SET Nombre=@Nombre, Apellido=@Apellido, Documento=@Documento, Email=@Email, Rol=@Rol
            WHERE Id=@Id";
            using var conn = CrearConexion();
            await conn.ExecuteAsync(sql, usuario);
        }


        public async Task EliminarAsync(int id)
        {
            const string sql = @"
            UPDATE dbo.Usuarios
            SET Eliminado=1
            WHERE Id=@Id";
            using var conn = CrearConexion();
            await conn.ExecuteAsync(sql,new { Id=id });
        }

        public async Task<bool> ExisteEmailAsync(string email,int? excludeId =null)
        {
            const string sql = @"
            SELECT COUNT(1)
            FROM dbo.Usuarios
            WHERE Email = @email AND Eliminado=0
              AND (@excludeId IS NULL OR Id <> @excludeId);";

            using var conn = CrearConexion();
            var count = await conn.ExecuteScalarAsync<int>(sql, new { email,excludeId });
            return count > 0;
        }

    }
}
