using System.ComponentModel.DataAnnotations;

namespace Conservacion.Entidades
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Contrasena { get; set; } = string.Empty;
    }
}
