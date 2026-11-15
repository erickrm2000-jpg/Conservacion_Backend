using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;


namespace Conservacion.AccesoDatos
{
    public class Conexion
    {
        private readonly string _cadenaConexion;

        public Conexion(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }
    }
}


