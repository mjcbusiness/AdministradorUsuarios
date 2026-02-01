using System.ComponentModel.DataAnnotations;

namespace AdministradorUsuarios.ViewModels
{
    public class UsuarioFormVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Apellido { get; set; } = "";

        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public string Documento { get; set; } = "";

        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "*")]
        public string Rol { get; set; } = "Usuario";
    }
}
