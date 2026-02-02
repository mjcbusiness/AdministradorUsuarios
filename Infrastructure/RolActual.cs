namespace AdministradorUsuarios.Infrastructure
{
    public class RolActual : IRolActual
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RolActual(IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public string ObtenerRol()
        {
            var role =_httpContextAccessor.HttpContext?.Session.GetString(Roles.SessionKey);
            if (!string.IsNullOrEmpty(role))
            {
                return role;
            }
            return _configuration["AppRole:RolActual"] ?? Roles.User;
        }
    }
}
