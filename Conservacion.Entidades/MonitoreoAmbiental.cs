using System;
using System.ComponentModel.DataAnnotations;

namespace Conservacion.Entidades
{
    public class MonitoreoAmbiental
    {
        public int IdMonitoreo { get; set; }

        [Required(ErrorMessage = "La fecha del monitoreo es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El tipo de monitoreo es obligatorio.")]
        [StringLength(60)]
        public string TipoMonitoreo { get; set; }
            = string.Empty;

        [Required(ErrorMessage ="Los resultados son obligatorios.")]
        [StringLength(1000)]
        public string Resultados { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un área válida.")]
        public int IdArea { get; set; }
    }
}
