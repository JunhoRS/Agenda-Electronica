using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaNegocio
{
    public class Contacto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Celular { get; set; }
    }

    public static class GestorContactos
    {
        private static string cadenaConexion = "Server=DESKTOP-7FTGLP1; database=dbagendaelectronica; Integrated security = true";

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

        public static void InsertarContacto(Contacto contacto)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Nombre", contacto.Nombre),
                new SqlParameter("@Apellido", contacto.Apellido),
                new SqlParameter("@Direccion", contacto.Direccion),
                new SqlParameter("@FechaNacimiento", contacto.FechaNacimiento),
                new SqlParameter("@Celular", contacto.Celular)
            };

            using (SqlConnection conexion = ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_InsertarContacto", conexion);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddRange(parametros);
                comando.ExecuteNonQuery();
            }
        }

        public static void ModificarContacto(Contacto contacto)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@ID", contacto.ID),
                new SqlParameter("@Nombre", contacto.Nombre),
                new SqlParameter("@Apellido", contacto.Apellido),
                new SqlParameter("@Direccion", contacto.Direccion),
                new SqlParameter("@FechaNacimiento", contacto.FechaNacimiento),
                new SqlParameter("@Celular", contacto.Celular)
            };

            using (SqlConnection conexion = ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_ModificarContacto", conexion);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddRange(parametros);
                comando.ExecuteNonQuery();
            }
        }

        public static Contacto BuscarContacto(int id)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_BuscarContactoPorID", conexion);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    Contacto contacto = new Contacto
                    {
                        ID = (int)reader["ID"],
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        FechaNacimiento = (DateTime)reader["FechaNacimiento"],
                        Celular = reader["Celular"].ToString()
                    };

                    return contacto;
                }

                return null; // Si no se encuentra ningún contacto con el ID especificado
            }
        }

        public static void EliminarContacto(int id)
        {
            SqlParameter parametro = new SqlParameter("@ID", id);

            using (SqlConnection conexion = ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_EliminarContacto", conexion);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add(parametro);
                comando.ExecuteNonQuery();
            }
        }
    }
}




