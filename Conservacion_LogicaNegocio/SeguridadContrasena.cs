using System.Security.Cryptography;

namespace Conservacion_LogicaNegocio
{
    public static class SeguridadContrasena
    {
        private const int Iteraciones = 210000;

        public static string GenerarHash(string contrasena)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(contrasena, salt,Iteraciones,HashAlgorithmName.SHA256,32);

            return $"PBKDF2${Iteraciones}$" +
                   $"{Convert.ToBase64String(salt)}$" +
                   $"{Convert.ToBase64String(hash)}";
        }

        public static bool Verificar(
            string contrasena, string hashGuardado)
        {
            try
            {
                string[] partes = hashGuardado.Split('$');

                if (partes.Length != 4 || partes[0] != "PBKDF2")
                    return false;

                int iteraciones = int.Parse(partes[1]);
                if (iteraciones < 100000 || iteraciones > 1000000)
                    return false;

                byte[] salt = Convert.FromBase64String(partes[2]);
                byte[] hashOriginal = Convert.FromBase64String(partes[3]);

                if (salt.Length != 16 || hashOriginal.Length != 32)
                    return false;

                byte[] hashCalculado = Rfc2898DeriveBytes.Pbkdf2(contrasena, salt, iteraciones, HashAlgorithmName.SHA256,32);

                return CryptographicOperations.FixedTimeEquals(hashCalculado, hashOriginal);
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }
        }
    }
}

