using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Conservacion.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; } = string.Empty;

        [JsonIgnore]
        public string ContrasenaHash { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;

        [StringLength(100)]
        [EmailAddress]
        public string? Correo { get; set; }

        [Required]
        public string Rol { get; set; } = "Usuario";

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}

