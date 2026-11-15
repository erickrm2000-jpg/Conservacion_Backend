
using System.ComponentModel.DataAnnotations;

namespace Conservacion.Entidades
{
    public class Especie
    {
        public int IdEspecie { get; set; }

        [Required(ErrorMessage = "El nombre científico es obligatorio.")]
        [StringLength(150)]
        public string NombreCientifico { get; set; }= string.Empty;

        [Required(ErrorMessage = "El nombre común es obligatorio.")]
        [StringLength(100)]
        public string NombreComun { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo es obligatorio.")]
        [RegularExpression("^(Flora|Fauna)$", ErrorMessage = "El tipo debe ser Flora o Fauna.")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado de conservación es obligatorio.")]
        [StringLength(30)]
        public string EstadoConservacion { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }
    }
}

