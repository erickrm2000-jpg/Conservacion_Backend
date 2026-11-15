
using Conservacion.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Conservacion.AccesoDatos
{
    public class EspecieAD
    {
        private readonly Conexion _conexion;

        public EspecieAD(Conexion conexion)
        {
            _conexion = conexion;
        }

        // Listar todas las especies
        public List<Especie> Listar()
        {
            List<Especie> lista = new();

            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @"SELECT id_especie, nombre_cientifico, nombre_comun, tipo, estado_conservacion, descripcion 
                        FROM Especie 
                        ORDER BY id_especie";

            using SqlCommand cmd = new(sql, cn);

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(MapearEspecie(dr));
            }

            return lista;
        }

        // Consultar especie por identificador
        public Especie? ObtenerPorId(int id)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @"
                SELECT id_especie, nombre_cientifico, nombre_comun, tipo, estado_conservacion, descripcion
                FROM Especie
                WHERE id_especie = @IdEspecie";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdEspecie", SqlDbType.Int).Value = id;

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return MapearEspecie(dr);
            }

            return null;
        }

        //Este método insertará una nueva especie y devolverá el identificador generado por SQL Server.
        public int Insertar(Especie especie)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @"INSERT INTO Especie (nombre_cientifico, nombre_comun, tipo, estado_conservacion, descripcion)
                            OUTPUT INSERTED.id_especie
                            VALUES
                            (@NombreCientifico, @NombreComun, @Tipo,
                             @EstadoConservacion, @Descripcion)";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@NombreCientifico", SqlDbType.NVarChar, 150).Value = especie.NombreCientifico;

            cmd.Parameters.Add("@NombreComun", SqlDbType.NVarChar, 100).Value = especie.NombreComun;

            cmd.Parameters.Add("@Tipo", SqlDbType.NVarChar, 10).Value = especie.Tipo;

            cmd.Parameters.Add("@EstadoConservacion", SqlDbType.NVarChar, 30).Value = especie.EstadoConservacion;

            cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 500).Value = (object?)especie.Descripcion ?? DBNull.Value;

            cn.Open();

            return Convert.ToInt32(cmd.ExecuteScalar());
        }


        // Convertir un registro SQL en entidad C#
        private static Especie MapearEspecie(SqlDataReader dr)
        {
            return new Especie
            {
                IdEspecie = Convert.ToInt32(dr["id_especie"]),

                NombreCientifico = dr["nombre_cientifico"].ToString()!,

                NombreComun = dr["nombre_comun"].ToString()!,

                Tipo = dr["tipo"].ToString()!,

                EstadoConservacion = dr["estado_conservacion"].ToString()!,

                Descripcion = dr["descripcion"] == DBNull.Value? null : dr["descripcion"].ToString()
            };
        }


        public bool Actualizar(Especie especie)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @"UPDATE Especie
                            SET nombre_cientifico = @NombreCientifico,
                                nombre_comun = @NombreComun,
                                tipo = @Tipo,
                                estado_conservacion = @EstadoConservacion,
                                descripcion = @Descripcion
                            WHERE id_especie = @IdEspecie";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdEspecie", SqlDbType.Int).Value = especie.IdEspecie;

            cmd.Parameters.Add("@NombreCientifico", SqlDbType.NVarChar, 150).Value = especie.NombreCientifico;

            cmd.Parameters.Add("@NombreComun", SqlDbType.NVarChar, 100).Value = especie.NombreComun;

            cmd.Parameters.Add("@Tipo", SqlDbType.NVarChar, 10).Value = especie.Tipo;

            cmd.Parameters.Add("@EstadoConservacion", SqlDbType.NVarChar, 30).Value = especie.EstadoConservacion;

            cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 500).Value = (object?)especie.Descripcion ?? DBNull.Value;

            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


        public bool Eliminar(int id)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @"DELETE FROM Especie
                            WHERE id_especie = @IdEspecie";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdEspecie", SqlDbType.Int).Value = id;

            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


    }
}
