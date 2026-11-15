using System;
using System.ComponentModel.DataAnnotations;

namespace Conservacion.Entidades
{
    public class ActividadConservacion : IValidatableObject
    {
        public int IdActividad { get; set; }

        [Required(ErrorMessage = "El nombre de la actividad es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un área válida.")]
        public int IdArea { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un responsable válido.")]
        public int IdPersonal { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FechaInicio == DateTime.MinValue)
            {
                yield return new ValidationResult(
                    "La fecha de inicio es obligatoria.", new[] { nameof(FechaInicio) });
            }

            if (FechaFin.HasValue &&
                FechaFin.Value.Date < FechaInicio.Date)
            {
                yield return new ValidationResult( "La fecha de finalización no puede ser " +  "anterior a la fecha de inicio.", new[] { nameof(FechaFin) });
            }
        }
    }
}

