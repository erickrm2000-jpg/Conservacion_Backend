using System;
using System.ComponentModel.DataAnnotations;

namespace Conservacion.Entidades
{
    public class Amenaza
    {
        public int IdAmenaza { get; set; }

        [Required(ErrorMessage = "El tipo de amenaza es obligatorio.")]
        [StringLength(60)]
        public string Tipo { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage ="El nivel de riesgo es obligatorio.")]
        [StringLength(10)]
        [RegularExpression("^(Bajo|Medio|Alto|Crítico)$",
            ErrorMessage ="El nivel debe ser Bajo, Medio, Alto o Crítico.")]
        public string NivelRiesgo { get; set; } = string.Empty;

        [Required(ErrorMessage ="La fecha del reporte es obligatoria.")]
        public DateTime FechaReporte { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un área válida.")]
        public int IdArea { get; set; }
    }
}

