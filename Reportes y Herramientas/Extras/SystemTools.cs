using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reportes_y_Herramientas.Extras
{
    internal class SystemTools
    {
        public static void EjecutarScript(string NombreScript)
        {
            string txtLog = "Ejecutar Script " + NombreScript;
            try
            {
                Log.Guardar(txtLog, "", 1);//MUESTRA Y ALMACENA UN MENSAJE
                Process p = Process.Start(Directory.GetCurrentDirectory() + @"\Archivos\Scripts\" + NombreScript);
                try
                {
                    p.WaitForInputIdle();
                }
                catch (Exception)
                {
                    Console.WriteLine("Sin interfaz grafca");
                }
                p.WaitForExit();

                Log.Guardar(txtLog, "", 2);//MUESTRA Y ALMACENA UN MENSAJE
            }
            catch (Exception ex)
            {
                Log.Guardar(txtLog, ex.Message, 99);//MUESTRA Y ALMACENA UN MENSAJE

            }

        }
    }
}
