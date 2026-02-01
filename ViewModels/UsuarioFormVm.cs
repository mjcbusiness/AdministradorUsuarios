using System.ComponentModel.DataAnnotations;

namespace AdministradorUsuarios.ViewModels
{
    public class UsuarioFormVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Apellido { get; set; } = "";

        [Required(ErrorMessage = "El documento es obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public string Documento { get; set; } = "";

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string Rol { get; set; } = "Usuario";
    }
}
