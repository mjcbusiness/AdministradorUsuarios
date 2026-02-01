namespace AdministradorUsuarios.Infrastructure
{
    public class RolActual : IRolActual
    {
        private readonly IConfiguration configuration;
        public RolActual(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string ObtenerRol()
        {
            return configuration["RolActual"] ?? "Usuario";
        }
    }
}
