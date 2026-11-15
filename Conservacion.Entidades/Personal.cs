using System.ComponentModel.DataAnnotations;

namespace Conservacion.Entidades
{
    public class Personal
    {
        public int IdPersonal { get; set; }

        [Required(ErrorMessage ="El nombre es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage ="El cargo es obligatorio.")]
        [StringLength(60)]
        public string Cargo { get; set; } = string.Empty;

        [StringLength(80)]
        public string? Especialidad { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }

        [StringLength(100)]
        [EmailAddress(ErrorMessage ="El correo electrónico no es válido.")]
        public string? Correo { get; set; }
    }
}
