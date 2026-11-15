
using Conservacion.Entidades;
using Conservacion_LogicaNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConservacionAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly AutenticacionLN _autenticacionLN;
        private readonly IConfiguration _configuracion;

        public AuthController(AutenticacionLN autenticacionLN, IConfiguration configuracion)
        {
            _autenticacionLN = autenticacionLN;
            _configuracion = configuracion;
        }

        // POST: api/auth/login
        [AllowAnonymous]
        [HttpPost("login")]

        public ActionResult<LoginRespuestaDTO> Login(
            [FromBody] LoginDTO login)
        {
            Usuario? usuario;

            try
            {
                usuario = _autenticacionLN.ValidarUsuario(login);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al procesar el inicio de sesión.");
            }

            if (usuario == null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            string clave = _configuracion["Jwt:Key"] ?? throw new InvalidOperationException("No se configuró la clave JWT.");

            string emisor = _configuracion["Jwt:Issuer"] ?? throw new InvalidOperationException("No se configuró el emisor JWT.");

            string audiencia = _configuracion["Jwt:Audience"] ?? throw new InvalidOperationException("No se configuró la audiencia JWT.");

            int minutos = int.Parse(_configuracion["Jwt:ExpireMinutes"] ?? "60");

            DateTime expiracion = DateTime.UtcNow.AddMinutes(minutos);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),

                new Claim(ClaimTypes.Name,usuario.NombreUsuario),

                new Claim(ClaimTypes.Role,usuario.Rol),

                new Claim("NombreCompleto",usuario.NombreCompleto)
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave));

            var credenciales = new SigningCredentials(llave,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: emisor,audience: audiencia,claims: claims,expires: expiracion,signingCredentials: credenciales);

            string tokenGenerado =new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new LoginRespuestaDTO
            {
                Token = tokenGenerado,
                Expiracion = expiracion,
                NombreUsuario = usuario.NombreUsuario,
                NombreCompleto = usuario.NombreCompleto,
                Rol = usuario.Rol
            });
        }
    }
}

