using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Reportes_y_Herramientas.Extras
{
    internal class LoadData
    {
        internal static string Id_app()
        {
            String line = "";
            try
            {
                StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\Archivos\\id_app");
                line = sr.ReadLine();
                sr.Close();
                if (line.IndexOf("_") < 0)
                {
                    Log.Guardar("id leida, pero está vacia.", "Revisar, esto no puede estar así!!", 99);
                }
            }
            catch (Exception ex)
            {
                Log.Guardar("Error al leer el archivo de id_app", ex.Message, 99);
            }
            finally
            {
                Log.Guardar("id_app leida.", "", 2);
            }
            return line;
        }

        internal static string visible_app()
        {
            String line = "";
            try
            {
                StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\Archivos\\visible_app");
                line = sr.ReadLine();
                sr.Close();
                if (line.IndexOf("_") < 0)
                {
                    Log.Guardar("Visibilidad leida, pero está vacia.", "Revisar, esto no puede estar así!!", 99);
                }
            }
            catch (Exception ex)
            {
                Log.Guardar("Error al leer el archivo de visible_app", ex.Message, 99);
            }
            finally
            {
                Log.Guardar("visible_app leida.", "", 2);
            }
            return line;
        }
    }
}
