using Conservacion.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Conservacion.AccesoDatos
{
    public class ActividadConservacionAD
    {
        private readonly Conexion _conexion;

        public ActividadConservacionAD(Conexion conexion)
        {
            _conexion = conexion;
        }

        // Listar todas las actividades
        public List<ActividadConservacion> Listar()
        {
            List<ActividadConservacion> lista = new();

            using SqlConnection cn =
                _conexion.ObtenerConexion();

            string sql = @" SELECT id_actividad, nombre, descripcion,fecha_inicio, fecha_fin,id_area, id_personal
                            FROM Actividad_Conservacion
                            ORDER BY id_actividad";

            using SqlCommand cmd = new(sql, cn);

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(MapearActividad(dr));
            }

            return lista;
        }

        // Obtener una actividad por su ID
        public ActividadConservacion? ObtenerPorId(int id)
        {
            using SqlConnection cn =
                _conexion.ObtenerConexion();

            string sql = @" SELECT id_actividad, nombre, descripcion,fecha_inicio, fecha_fin,id_area, id_personal
                            FROM Actividad_Conservacion
                            WHERE id_actividad = @IdActividad";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdActividad", SqlDbType.Int).Value = id;

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return MapearActividad(dr);
            }

            return null;
        }

        // Convertir los datos SQL en un objeto C#
        private static ActividadConservacion MapearActividad(SqlDataReader dr)
        {
            return new ActividadConservacion
            {
                IdActividad = Convert.ToInt32(dr["id_actividad"]),

                Nombre = dr["nombre"].ToString()!,

                Descripcion = dr["descripcion"] == DBNull.Value ? null  : dr["descripcion"].ToString(),

                FechaInicio = Convert.ToDateTime(dr["fecha_inicio"]),

                FechaFin = dr["fecha_fin"] == DBNull.Value ? null : Convert.ToDateTime(dr["fecha_fin"]),

                IdArea = Convert.ToInt32(dr["id_area"]),

                IdPersonal = Convert.ToInt32(dr["id_personal"])
            };
        }

        public int Insertar(ActividadConservacion actividad)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" INSERT INTO Actividad_Conservacion(nombre, descripcion, fecha_inicio,fecha_fin, id_area, id_personal)
                            OUTPUT INSERTED.id_actividad
                            VALUES
                            (@Nombre, @Descripcion, @FechaInicio,
                             @FechaFin, @IdArea, @IdPersonal)";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = actividad.Nombre;

            cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 500).Value = (object?)actividad.Descripcion ?? DBNull.Value;

            cmd.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = actividad.FechaInicio;

            cmd.Parameters.Add("@FechaFin",  SqlDbType.Date).Value =(object?)actividad.FechaFin ?? DBNull.Value;

            cmd.Parameters.Add("@IdArea", SqlDbType.Int).Value = actividad.IdArea;

            cmd.Parameters.Add("@IdPersonal",SqlDbType.Int).Value =actividad.IdPersonal;

            cn.Open();

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool Actualizar(ActividadConservacion actividad)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" UPDATE Actividad_Conservacion
                            SET nombre = @Nombre,
    descripcion = @Descripcion,
    fecha_inicio = @FechaInicio,
    fecha_fin = @FechaFin,
    id_area = @IdArea,
    id_personal = @IdPersonal
                            WHERE id_actividad = @IdActividad";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdActividad",SqlDbType.Int).Value = actividad.IdActividad;

            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = actividad.Nombre;

            cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 500).Value = (object?)actividad.Descripcion ?? DBNull.Value;

            cmd.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = actividad.FechaInicio;

            cmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = (object?)actividad.FechaFin ?? DBNull.Value;

            cmd.Parameters.Add("@IdArea",SqlDbType.Int).Value = actividad.IdArea;

            cmd.Parameters.Add("@IdPersonal",SqlDbType.Int).Value = actividad.IdPersonal;

            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Eliminar(int id)
        {
            using SqlConnection cn =_conexion.ObtenerConexion();

            string sql = @" DELETE FROM Actividad_Conservacion
                            WHERE id_actividad = @IdActividad";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdActividad", SqlDbType.Int).Value = id;

            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }



    }
}

