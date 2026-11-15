using Conservacion.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Conservacion.AccesoDatos
{
    public class AmenazaAD
    {
        private readonly Conexion _conexion;

        public AmenazaAD(Conexion conexion)
        {
            _conexion = conexion;
        }

        // Listar todas las amenazas
        public List<Amenaza> Listar()
        {
            List<Amenaza> lista = new();

            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" SELECT id_amenaza, tipo, descripcion, nivel_riesgo, fecha_reporte, id_area
                            FROM Amenaza
                            ORDER BY id_amenaza";

            using SqlCommand cmd = new(sql, cn);

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(MapearAmenaza(dr));
            }

            return lista;
        }

        // Obtener una amenaza por su identificador
        public Amenaza? ObtenerPorId(int id)
        {
            using SqlConnection cn =
                _conexion.ObtenerConexion();

            string sql = @" SELECT id_amenaza, tipo, descripcion, nivel_riesgo, fecha_reporte, id_area
                            FROM Amenaza
                            WHERE id_amenaza = @IdAmenaza";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdAmenaza", SqlDbType.Int).Value = id;

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return MapearAmenaza(dr);
            }

            return null;
        }

        // Convertir un registro SQL en entidad C#
        private static Amenaza MapearAmenaza(SqlDataReader dr)
        {
            return new Amenaza
            {
                IdAmenaza =
                    Convert.ToInt32(dr["id_amenaza"]),

                Tipo =dr["tipo"].ToString()!,

                Descripcion =dr["descripcion"] == DBNull.Value ? null : dr["descripcion"].ToString(),

                NivelRiesgo = dr["nivel_riesgo"].ToString()!,

                FechaReporte =Convert.ToDateTime(dr["fecha_reporte"]),

                IdArea = Convert.ToInt32(dr["id_area"])
            };
        }

        public int Insertar(Amenaza amenaza)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" INSERT INTO Amenaza(tipo, descripcion, nivel_riesgo, fecha_reporte, id_area)
                            OUTPUT INSERTED.id_amenaza
                            VALUES
                            (@Tipo, @Descripcion, @NivelRiesgo,
                             @FechaReporte, @IdArea)";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@Tipo", SqlDbType.NVarChar, 60).Value = amenaza.Tipo;

            cmd.Parameters.Add("@Descripcion",SqlDbType.NVarChar, 500).Value = (object?)amenaza.Descripcion ?? DBNull.Value;

            cmd.Parameters.Add("@NivelRiesgo", SqlDbType.NVarChar, 10).Value = amenaza.NivelRiesgo;

            cmd.Parameters.Add("@FechaReporte", SqlDbType.Date).Value =  amenaza.FechaReporte;

            cmd.Parameters.Add("@IdArea", SqlDbType.Int).Value = amenaza.IdArea;

            cn.Open();

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool Actualizar(Amenaza amenaza)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" UPDATE Amenaza
                            SET tipo = @Tipo,
                                descripcion = @Descripcion,
                                nivel_riesgo = @NivelRiesgo,
                                fecha_reporte = @FechaReporte,
                                id_area = @IdArea
                            WHERE id_amenaza = @IdAmenaza";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdAmenaza", SqlDbType.Int).Value = amenaza.IdAmenaza;

            cmd.Parameters.Add("@Tipo", SqlDbType.NVarChar, 60).Value = amenaza.Tipo;

            cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 500).Value = (object?)amenaza.Descripcion ?? DBNull.Value;

            cmd.Parameters.Add("@NivelRiesgo",SqlDbType.NVarChar, 10).Value =amenaza.NivelRiesgo;

            cmd.Parameters.Add("@FechaReporte", SqlDbType.Date).Value = amenaza.FechaReporte;

            cmd.Parameters.Add("@IdArea", SqlDbType.Int).Value = amenaza.IdArea;

            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


        public bool Eliminar(int id)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" DELETE FROM Amenaza
                            WHERE id_amenaza = @IdAmenaza";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdAmenaza", SqlDbType.Int).Value = id;

            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


    }
}

