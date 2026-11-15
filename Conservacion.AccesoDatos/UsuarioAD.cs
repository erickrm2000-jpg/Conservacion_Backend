using Conservacion.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Conservacion.AccesoDatos
{
    public class UsuarioAD
    {
        private readonly Conexion _conexion;

        public UsuarioAD(Conexion conexion)
        {
            _conexion = conexion;
        }

        // Buscar un usuario activo por nombre
        public Usuario? ObtenerPorNombre(string nombreUsuario)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @"  SELECT id_usuario, nombre_usuario, contrasena_hash, nombre_completo, correo, rol, activo, fecha_creacion
                             FROM Usuario
                             WHERE nombre_usuario = @NombreUsuario
                             AND activo = 1";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@NombreUsuario", SqlDbType.NVarChar, 50).Value = nombreUsuario;

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new Usuario
                {
                    IdUsuario = Convert.ToInt32(dr["id_usuario"]),

                    NombreUsuario = dr["nombre_usuario"].ToString()!,

                    ContrasenaHash = dr["contrasena_hash"].ToString()!,

                    NombreCompleto = dr["nombre_completo"].ToString()!,

                    Correo = dr["correo"] == DBNull.Value ? null : dr["correo"].ToString(),

                    Rol = dr["rol"].ToString()!,

                    Activo = Convert.ToBoolean(dr["activo"]),

                    FechaCreacion = Convert.ToDateTime(dr["fecha_creacion"])
                };
            }

            return null;
        }
    }
}
