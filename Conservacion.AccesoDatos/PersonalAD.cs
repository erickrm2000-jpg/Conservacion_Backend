using Conservacion.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Conservacion.AccesoDatos
{
    public class PersonalAD
    {
        private readonly Conexion _conexion;

        public PersonalAD(Conexion conexion)
        {
            _conexion = conexion;
        }

        // Obtener todo el personal
        public List<Personal> Listar()
        {
            List<Personal> lista = new();

            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @"SELECT id_personal, nombre, cargo,
                            especialidad, telefono, correo
                            FROM Personal
                            ORDER BY id_personal";

            using SqlCommand cmd = new(sql, cn);

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(MapearPersonal(dr));
            }

            return lista;
        }

        // Obtener personal por identificador
        public Personal? ObtenerPorId(int id)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" SELECT id_personal, nombre, cargo,
                            especialidad, telefono, correo
                            FROM Personal
                            WHERE id_personal = @IdPersonal";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdPersonal", SqlDbType.Int).Value = id;

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return MapearPersonal(dr);
            }

            return null;
        }
        //insertar nueva persona y devolver identificador de SQL Server.
        public int Insertar(Personal personal)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" INSERT INTO Personal
                            (nombre, cargo, especialidad, telefono, correo)
                            OUTPUT INSERTED.id_personal
                            VALUES
                            (@Nombre, @Cargo, @Especialidad,
                             @Telefono, @Correo)";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = personal.Nombre;
            cmd.Parameters.Add("@Cargo", SqlDbType.NVarChar, 60).Value = personal.Cargo;
            cmd.Parameters.Add("@Especialidad", SqlDbType.NVarChar, 80).Value = (object?)personal.Especialidad ?? DBNull.Value;
            cmd.Parameters.Add("@Telefono", SqlDbType.NVarChar, 20).Value = (object?)personal.Telefono ?? DBNull.Value;
            cmd.Parameters.Add("@Correo", SqlDbType.NVarChar, 100).Value = (object?)personal.Correo ?? DBNull.Value;
            cn.Open();

            return Convert.ToInt32(cmd.ExecuteScalar());
        }


        // Convertir el registro SQL en entidad
        private static Personal MapearPersonal(SqlDataReader dr)
        {
            return new Personal
            {
                IdPersonal =
                    Convert.ToInt32(dr["id_personal"]),

                Nombre = dr["nombre"].ToString()!,
                Cargo = dr["cargo"].ToString()!,
                Especialidad = dr["especialidad"] == DBNull.Value ? null : dr["especialidad"].ToString(),
                Telefono = dr["telefono"] == DBNull.Value ? null : dr["telefono"].ToString(),
                Correo = dr["correo"] == DBNull.Value ? null : dr["correo"].ToString()
            };
        }


        public bool Actualizar(Personal personal)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" UPDATE Personal
                            SET nombre = @Nombre, cargo = @Cargo, especialidad = @Especialidad, telefono = @Telefono, correo = @Correo
                            WHERE id_personal = @IdPersonal";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdPersonal", SqlDbType.Int).Value = personal.IdPersonal;

            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = personal.Nombre;

            cmd.Parameters.Add("@Cargo", SqlDbType.NVarChar, 60).Value = personal.Cargo;

            cmd.Parameters.Add("@Especialidad", SqlDbType.NVarChar, 80).Value = (object?)personal.Especialidad ?? DBNull.Value;

            cmd.Parameters.Add("@Telefono", SqlDbType.NVarChar, 20).Value = (object?)personal.Telefono ?? DBNull.Value;

            cmd.Parameters.Add("@Correo", SqlDbType.NVarChar, 100).Value = (object?)personal.Correo ?? DBNull.Value;

            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


        public bool Eliminar(int id)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" DELETE FROM Personal
                            WHERE id_personal = @IdPersonal";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdPersonal", SqlDbType.Int).Value = id;

            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


    }
}
