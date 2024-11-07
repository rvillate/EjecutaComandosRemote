using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reportes_y_Herramientas.Extras
{
    internal class Log
    {
        public static string Guardar(string texto, string msgError, int estado, Boolean muestraLog = true)
            {
            string nombreLog = Directory.GetCurrentDirectory() + "\\Archivos\\logs\\" + MainWindow.nombreLogActual;

            using (StreamWriter mylogs = File.AppendText(nombreLog))         //se crea el archivo
            {
                mylogs.Close();
            }


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(nombreLog, true))
            {


                if (muestraLog == true)
                {
                    if (estado == 1)
                    {

                        Console.ForegroundColor = ConsoleColor.Blue;
                        string line = DateTime.Now.ToString() + " - " + texto + ": Pendiente";
                        file.WriteLine(line);
                        Console.WriteLine(line);
                        Console.ResetColor();

                    }
                    else if (estado == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        string line = DateTime.Now.ToString() + " - " + texto + ": Finalizado";
                        file.WriteLine(line);
                        Console.WriteLine(line);

                        Console.ResetColor();


                    }
                    else if (estado == 99)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        string line = DateTime.Now.ToString() + " - " + texto + ": Error";
                        file.WriteLine(line);
                        Console.WriteLine(line);


                        line = DateTime.Now.ToString() + " - " + "Error: " + msgError;
                        file.WriteLine(line);
                        Console.WriteLine(line);

                        Console.ResetColor();


                    }
                    else if (estado == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        string line = DateTime.Now.ToString() + " - " + texto;
                        file.WriteLine(line);
                        Console.WriteLine(line);

                        Console.ResetColor();


                    }
                }
            }

            return texto;
        }
    }
}
