using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaDatos
{
    public static class ConexionBD
    {
        public static string cadenaConexion = "Server=DESKTOP-7FTGLP1; database=dbagendaelectronica; Integrated security=true;";

        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            return conexion;
        }

        public static void EjecutarProcedimientoAlmacenado(string nombreProcedimiento, SqlParameter[] parametros)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand(nombreProcedimiento, conexion);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    comando.Parameters.AddRange(parametros);
                }
                comando.ExecuteNonQuery();
            }
        }
    }
}


