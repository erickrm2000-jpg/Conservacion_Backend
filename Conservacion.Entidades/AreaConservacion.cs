
using System;
using System.ComponentModel.DataAnnotations;

namespace Conservacion.Entidades
{
    public class AreaConservacion
    {
        public int IdArea { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        [StringLength(200)]
        public string Ubicacion { get; set; } = string.Empty;

        [Range(0.01, 9999999999.99,
            ErrorMessage = "La extensión debe ser mayor que cero.")]
        public decimal ExtensionHectareas { get; set; }

        [Required(ErrorMessage = "El tipo de área es obligatorio.")]
        [StringLength(50)]
        public string TipoArea { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime FechaCreacion { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression("^(Activa|Inactiva)$",
            ErrorMessage = "El estado debe ser Activa o Inactiva.")]
        public string Estado { get; set; } = "Activa";
    }
}
