using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Reportes_y_Herramientas.Extras;

namespace Reportes_y_Herramientas.Connections
{
    internal class MyDBSQL
    {
        static MySqlConnection conexion;

        public static MySqlConnection ConectarDB(string server, uint port, string DB, string user, string password, Boolean muestraLog = true)
        {
            string txtLog = "Conectar a Base de datos";

            try
            {
                Log.Guardar(txtLog, "", 1, muestraLog);//MUESTRA Y ALMACENA UN MENSAJE COMO PENDIENTE POR TENER ARGUMENTO 1
                MySqlConnectionStringBuilder cadenaConexion = new MySqlConnectionStringBuilder();
                cadenaConexion.Server = server;
                cadenaConexion.Port = port;
                cadenaConexion.Database = DB;
                cadenaConexion.UserID = user;
                cadenaConexion.Password = password;
                cadenaConexion.ConnectionTimeout = 10;

                conexion = new MySqlConnection(cadenaConexion.ToString());

                conexion.Open();
                Log.Guardar(txtLog, "", 2, muestraLog);//MUESTRA Y ALMACENA UN MENSAJE COMO PENDIENTE POR TENER ARGUMENTO 1

            }
            catch (Exception e)
            {
                Log.Guardar(txtLog, e.Message, 99, muestraLog);//MUESTRA Y ALMACENA UN MENSAJE COMO PENDIENTE POR TENER ARGUMENTO 1
                try
                {
                    if (e.Message == "Unable to connect to any of the specified MySQL hosts.")
                    {
                        Thread.Sleep(5000);
                    }
                    conexion.Dispose();
                }
                catch (Exception ex)
                {
                    Log.Guardar("Error al cerrar conexión", ex.Message, 99);
                }
            }
            return conexion;
        }
        public static void DesconectarDB(MySqlConnection con, Boolean muestraLog = true)
        {
            string txtLog = "Desconectar a Base de datos";

            try
            {
                Log.Guardar(txtLog, "", 1, muestraLog);//MUESTRA Y ALMACENA UN MENSAJE COMO PENDIENTE POR TENER ARGUMENTO 1
                con.Close();
                con.Dispose();
                Log.Guardar(txtLog, "", 2, muestraLog);//MUESTRA Y ALMACENA UN MENSAJE COMO PENDIENTE POR TENER ARGUMENTO 1
            }
            catch (Exception e)
            {
                Log.Guardar(txtLog, e.Message, 99, muestraLog);//MUESTRA Y ALMACENA UN MENSAJE COMO PENDIENTE POR TENER ARGUMENTO 1
                con.Dispose();
            }
        }

        public static string ActualizarDB(string query, MySqlConnection conexion, Boolean muestraLog = true)
        {
            MySqlCommand cmdUp;
            int rows = 0;
            string txtLog = "Actualizando registro";
            try
            {
                if (conexion.State != System.Data.ConnectionState.Open)
                {
                    conexion.Open();
                }
                Log.Guardar(txtLog, "", 1, muestraLog);//MUESTRA Y ALMACENA UN MENSAJE COMO PENDIENTE POR TENER ARGUMENTO 1
                Log.Guardar("Query: " + query, "", 0, muestraLog);
                using (cmdUp = new MySqlCommand(query, conexion))
                {
                    rows = cmdUp.ExecuteNonQuery();
                }
                //conexion.Dispose();
                Log.Guardar(txtLog, "", 2, muestraLog);//MUESTRA Y ALMACENA UN MENSAJE COMO PENDIENTE POR TENER ARGUMENTO 1
            }
            catch (Exception e)
            {
                Log.Guardar(txtLog, e.Message, 99, muestraLog);//MUESTRA Y ALMACENA UN MENSAJE COMO PENDIENTE POR TENER ARGUMENTO 1
            }

            return "";
        }
    }
}
