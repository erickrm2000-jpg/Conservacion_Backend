using Conservacion.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Conservacion.AccesoDatos
{
    public class MonitoreoAmbientalAD
    {
        private readonly Conexion _conexion;

        public MonitoreoAmbientalAD(Conexion conexion)
        {
            _conexion = conexion;
        }

        // Listar todos los monitoreos ambientales
        public List<MonitoreoAmbiental> Listar()
        {
            List<MonitoreoAmbiental> lista = new();

            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" SELECT id_monitoreo, fecha,tipo_monitoreo, resultados, id_area
                            FROM Monitoreo_Ambiental
                            ORDER BY id_monitoreo";

            using SqlCommand cmd = new(sql, cn);

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(MapearMonitoreo(dr));
            }

            return lista;
        }

        // Obtener un monitoreo por su identificador
        public MonitoreoAmbiental? ObtenerPorId(int id)
        {
            using SqlConnection cn =
                _conexion.ObtenerConexion();

            string sql = @" SELECT id_monitoreo, fecha, tipo_monitoreo, resultados, id_area
                            FROM Monitoreo_Ambiental
                            WHERE id_monitoreo = @IdMonitoreo";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdMonitoreo", SqlDbType.Int).Value = id;

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return MapearMonitoreo(dr);
            }

            return null;
        }

        // Convertir una fila SQL en una entidad C#
        private static MonitoreoAmbiental MapearMonitoreo(SqlDataReader dr)
        {
            return new MonitoreoAmbiental
            {
                IdMonitoreo = Convert.ToInt32(dr["id_monitoreo"]),

                Fecha = Convert.ToDateTime(dr["fecha"]),

                TipoMonitoreo = dr["tipo_monitoreo"].ToString()!,

                Resultados = dr["resultados"].ToString()!,

                IdArea = Convert.ToInt32(dr["id_area"])
            };
        }


        public int Insertar(MonitoreoAmbiental monitoreo)
        {
            using SqlConnection cn =_conexion.ObtenerConexion();

            string sql = @" INSERT INTO Monitoreo_Ambiental(fecha, tipo_monitoreo, resultados, id_area)
                            OUTPUT INSERTED.id_monitoreo
                            VALUES
                            (@Fecha, @TipoMonitoreo, @Resultados, @IdArea)";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = monitoreo.Fecha;

            cmd.Parameters.Add("@TipoMonitoreo", SqlDbType.NVarChar, 60).Value = monitoreo.TipoMonitoreo;

            cmd.Parameters.Add("@Resultados", SqlDbType.NVarChar, 1000).Value = monitoreo.Resultados;

            cmd.Parameters.Add("@IdArea",SqlDbType.Int).Value = monitoreo.IdArea;

            cn.Open();

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool Actualizar(MonitoreoAmbiental monitoreo)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @"  UPDATE Monitoreo_Ambiental
                            SET fecha = @Fecha,
                                tipo_monitoreo = @TipoMonitoreo,
                                resultados = @Resultados,
                                id_area = @IdArea
                            WHERE id_monitoreo = @IdMonitoreo";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdMonitoreo", SqlDbType.Int).Value = monitoreo.IdMonitoreo;

            cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = monitoreo.Fecha;

            cmd.Parameters.Add("@TipoMonitoreo", SqlDbType.NVarChar, 60).Value = monitoreo.TipoMonitoreo;

            cmd.Parameters.Add("@Resultados", SqlDbType.NVarChar, 1000).Value = monitoreo.Resultados;

            cmd.Parameters.Add("@IdArea", SqlDbType.Int).Value = monitoreo.IdArea;

            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Eliminar(int id)
        {
            using SqlConnection cn =_conexion.ObtenerConexion();

            string sql = @" DELETE FROM Monitoreo_Ambiental
                             WHERE id_monitoreo = @IdMonitoreo";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdMonitoreo", SqlDbType.Int).Value = id;

            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }



    }
}

