using Conservacion.AccesoDatos;
using Conservacion.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace Conservacion_LogicaNegocio
{
    public class AutenticacionLN
    {
        private readonly UsuarioAD _usuarioAD;

        public AutenticacionLN(UsuarioAD usuarioAD)
        {
            _usuarioAD = usuarioAD;
        }

        public Usuario? ValidarUsuario(LoginDTO login)
        {
            if (login == null || string.IsNullOrWhiteSpace(login.NombreUsuario) || string.IsNullOrEmpty(login.Contrasena))
            {
                return null;
            }

            Usuario? usuario = _usuarioAD.ObtenerPorNombre(login.NombreUsuario);

            if (usuario == null || !usuario.Activo)
                return null;
            //Para mantener funcionando el administrador y agrega soporte para contraseñas almacenadas con PBKDF2

            string hashGuardado = usuario.ContrasenaHash;

            bool valido;

            if (hashGuardado.StartsWith("PBKDF2$"))
            {
                valido = SeguridadContrasena.Verificar(
                    login.Contrasena, hashGuardado);
            }
            else
            {
                // Compatibilidad temporal con el usuario admin.
                byte[] hashCalculado = SHA256.HashData(
                    Encoding.UTF8.GetBytes(login.Contrasena));

                try
                {
                    byte[] hashOriginal =
                        Convert.FromHexString(hashGuardado);

                    valido = CryptographicOperations.FixedTimeEquals(
                        hashCalculado, hashOriginal);
                }
                catch (FormatException)
                {
                    valido = false;
                }
            }

            return valido ? usuario : null;


            /*byte[] hashCalculado = SHA256.HashData(Encoding.UTF8.GetBytes(login.Contrasena));

            byte[] hashAlmacenado;

            try
            {
                hashAlmacenado = Convert.FromHexString(usuario.ContrasenaHash);
            }
            catch (FormatException)
            {
                return null;
            }

            if (hashAlmacenado.Length != hashCalculado.Length)
                return null;

            bool valido = CryptographicOperations.FixedTimeEquals(
                hashCalculado, hashAlmacenado);

            return valido ? usuario : null;*/
        }
    }
}
