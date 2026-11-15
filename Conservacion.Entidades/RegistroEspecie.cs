using System;
using System.ComponentModel.DataAnnotations;

namespace Conservacion.Entidades
{
    public class RegistroEspecie
    {
        public int IdRegistro { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un área válida.")]
        public int IdArea { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una especie válida.")]
        public int IdEspecie { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa.")]
        public int CantidadEstimada { get; set; }

        [Required(ErrorMessage = "La fecha de registro es obligatoria.")]
        public DateTime FechaRegistro { get; set; }
    }
}
