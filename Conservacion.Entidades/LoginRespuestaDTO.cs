using System;

namespace Conservacion.Entidades
{
    public class LoginRespuestaDTO
    {
        public string Token { get; set; } = string.Empty;

        public DateTime Expiracion { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;

        public string NombreCompleto { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;
    }
}
