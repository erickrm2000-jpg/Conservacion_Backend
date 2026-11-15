using Conservacion.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Conservacion.AccesoDatos
{
    public class RegistroEspecieAD
    {
        private readonly Conexion _conexion;

        public RegistroEspecieAD(Conexion conexion)
        {
            _conexion = conexion;
        }

        // Listar todos los registros de especies
        public List<RegistroEspecie> Listar()
        {
            List<RegistroEspecie> lista = new();

            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" SELECT id_registro, id_area, id_especie,
                            cantidad_estimada, fecha_registro
                            FROM Registro_Especie
                            ORDER BY id_registro";

            using SqlCommand cmd = new(sql, cn);

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(MapearRegistro(dr));
            }

            return lista;
        }

        // Obtener un registro por su identificador
        public RegistroEspecie? ObtenerPorId(int id)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" SELECT id_registro, id_area, id_especie,
                            cantidad_estimada, fecha_registro
                            FROM Registro_Especie
                            WHERE id_registro = @IdRegistro";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdRegistro", SqlDbType.Int).Value = id;

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return MapearRegistro(dr);
            }

            return null;
        }



        // Convertir una fila SQL en entidad C#
        private static RegistroEspecie MapearRegistro(SqlDataReader dr)
        {
            return new RegistroEspecie
            {
                IdRegistro = Convert.ToInt32(dr["id_registro"]),

                IdArea = Convert.ToInt32(dr["id_area"]),

                IdEspecie = Convert.ToInt32(dr["id_especie"]),

                CantidadEstimada = Convert.ToInt32(dr["cantidad_estimada"]),

                FechaRegistro = Convert.ToDateTime(dr["fecha_registro"])
            };
        }

        //inserta registro y devuelve identificador generado por SQL Server.
        public int Insertar(RegistroEspecie registro)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" INSERT INTO Registro_Especie(id_area, id_especie,cantidad_estimada, fecha_registro)
                            OUTPUT INSERTED.id_registro
                            VALUES
                            (@IdArea, @IdEspecie,@CantidadEstimada, @FechaRegistro)";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdArea", SqlDbType.Int).Value = registro.IdArea;

            cmd.Parameters.Add("@IdEspecie", SqlDbType.Int).Value = registro.IdEspecie;

            cmd.Parameters.Add("@CantidadEstimada", SqlDbType.Int).Value = registro.CantidadEstimada;

            cmd.Parameters.Add("@FechaRegistro", SqlDbType.Date).Value = registro.FechaRegistro;

            cn.Open();

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool Actualizar(RegistroEspecie registro)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" UPDATE Registro_Especie
                            SET id_area = @IdArea, id_especie = @IdEspecie, cantidad_estimada = @CantidadEstimada, fecha_registro = @FechaRegistro
                            WHERE id_registro = @IdRegistro";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdRegistro", SqlDbType.Int).Value = registro.IdRegistro;

            cmd.Parameters.Add("@IdArea", SqlDbType.Int).Value = registro.IdArea;

            cmd.Parameters.Add("@IdEspecie", SqlDbType.Int).Value = registro.IdEspecie;

            cmd.Parameters.Add("@CantidadEstimada", SqlDbType.Int).Value = registro.CantidadEstimada;

            cmd.Parameters.Add("@FechaRegistro", SqlDbType.Date).Value = registro.FechaRegistro;

            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Eliminar(int id)
        {
            using SqlConnection cn = _conexion.ObtenerConexion();

            string sql = @" DELETE FROM Registro_Especie
                            WHERE id_registro = @IdRegistro";

            using SqlCommand cmd = new(sql, cn);

            cmd.Parameters.Add("@IdRegistro", SqlDbType.Int).Value = id;

            cn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


    }
}
