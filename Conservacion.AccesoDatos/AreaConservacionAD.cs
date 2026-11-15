using Conservacion.Entidades;
using Microsoft.Data.SqlClient;

namespace Conservacion.AccesoDatos
{
    public class AreaConservacionAD
    {
        private readonly Conexion _conexion;

        public AreaConservacionAD(Conexion conexion)
        {
            _conexion = conexion;
        }

        public List<AreaConservacion> Listar()
        {
            List<AreaConservacion> lista = new();

            using SqlConnection cn =_conexion.ObtenerConexion();

            string sql = @"SELECT id_area, nombre,
                           ubicacion, extension_hectareas,
                           tipo_area, fecha_creacion, estado
                           FROM Area_Conservacion";

            using SqlCommand cmd =new SqlCommand(sql, cn);

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                AreaConservacion area = new()
                {
                    IdArea = Convert.ToInt32(dr["id_area"]),
                    Nombre = dr["nombre"].ToString()!,
                    Ubicacion = dr["ubicacion"].ToString()!,
                    ExtensionHectareas = Convert.ToDecimal(dr["extension_hectareas"]),
                    TipoArea = dr["tipo_area"].ToString()!,
                    FechaCreacion =Convert.ToDateTime(dr["fecha_creacion"]),
                    Estado = dr["estado"].ToString()!
                };

                lista.Add(area);
            }

            return lista;
        }

        public AreaConservacion? ObtenerPorId(int id)
        {
            using SqlConnection cn =_conexion.ObtenerConexion();

            string sql = @"SELECT id_area, nombre,
                   ubicacion, extension_hectareas,
                   tipo_area, fecha_creacion, estado
                   FROM Area_Conservacion
                   WHERE id_area = @IdArea";

            using SqlCommand cmd = new SqlCommand(sql, cn);

            cmd.Parameters.Add("@IdArea",
                System.Data.SqlDbType.Int).Value = id;

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new AreaConservacion
                {
                    IdArea = Convert.ToInt32(dr["id_area"]),
                    Nombre = dr["nombre"].ToString()!,
                    Ubicacion = dr["ubicacion"].ToString()!,
                    ExtensionHectareas = Convert.ToDecimal(dr["extension_hectareas"]),
                    TipoArea = dr["tipo_area"].ToString()!,
                    FechaCreacion = Convert.ToDateTime(dr["fecha_creacion"]),
                    Estado = dr["estado"].ToString()!
                };
            }

            return null;
        }


        public int Insertar(AreaConservacion area)
        {
            using SqlConnection cn =
                _conexion.ObtenerConexion();

            string sql = @"INSERT INTO Area_Conservacion(nombre,ubicacion,extension_hectareas,tipo_area,fecha_creacion, estado)
            OUTPUT INSERTED.id_area
            VALUES
            (@Nombre,@Ubicacion,@Extension,@TipoArea,@FechaCreacion,@Estado)";

            using SqlCommand cmd = new SqlCommand(sql, cn);

            cmd.Parameters.Add("@Nombre",System.Data.SqlDbType.NVarChar, 100).Value =area.Nombre;

            cmd.Parameters.Add("@Ubicacion",System.Data.SqlDbType.NVarChar, 200).Value =area.Ubicacion;

            var extension = cmd.Parameters.Add("@Extension",System.Data.SqlDbType.Decimal);
            extension.Precision = 12;
            extension.Scale = 2;
            extension.Value = area.ExtensionHectareas;

            cmd.Parameters.Add("@TipoArea",System.Data.SqlDbType.NVarChar, 50).Value =area.TipoArea;
            cmd.Parameters.Add("@FechaCreacion",System.Data.SqlDbType.Date).Value =area.FechaCreacion;
            cmd.Parameters.Add("@Estado",System.Data.SqlDbType.NVarChar, 20).Value =area.Estado;
            cn.Open();

            return Convert.ToInt32(cmd.ExecuteScalar());
        }


        public bool Actualizar(AreaConservacion area)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @"UPDATE Area_Conservacion
        SET nombre = @Nombre,
            ubicacion = @Ubicacion,
            extension_hectareas = @Extension,
            tipo_area = @TipoArea,
            fecha_creacion = @FechaCreacion,
            estado = @Estado
        WHERE id_area = @IdArea";

            using SqlCommand cmd = new SqlCommand(sql, cn);

            cmd.Parameters.Add("@IdArea", System.Data.SqlDbType.Int).Value = area.IdArea;

            cmd.Parameters.Add("@Nombre",System.Data.SqlDbType.NVarChar, 100).Value = area.Nombre;

            cmd.Parameters.Add("@Ubicacion",System.Data.SqlDbType.NVarChar, 200).Value = area.Ubicacion;

            var extension = cmd.Parameters.Add("@Extension",System.Data.SqlDbType.Decimal);
            extension.Precision = 12;
            extension.Scale = 2;
            extension.Value = area.ExtensionHectareas;

            cmd.Parameters.Add("@TipoArea", System.Data.SqlDbType.NVarChar, 50).Value = area.TipoArea;

            cmd.Parameters.Add("@FechaCreacion",System.Data.SqlDbType.Date).Value = area.FechaCreacion;

            cmd.Parameters.Add("@Estado", System.Data.SqlDbType.NVarChar, 20).Value = area.Estado;

            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


        public bool Eliminar(int id)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @"DELETE FROM Area_Conservacion WHERE id_area = @IdArea";

            using SqlCommand cmd = new SqlCommand(sql, cn);

            cmd.Parameters.Add("@IdArea", System.Data.SqlDbType.Int).Value = id;

            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


    }
}
