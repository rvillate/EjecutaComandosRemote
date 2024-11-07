using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Reportes_y_Herramientas
{
    internal class class_router
    {
        internal static Boolean shwView = false;

        internal static string selection_program(string args1, string args2)
        {
            string returnValue = "";




            if (args1.IndexOf("<<type>>") >= 0)
            {
                switch (args1.Split("<<type>>")[1])
                {
                    case "SendCMD":


                        //string command = "/C " + args2;
                        //Process.Start("cmd.exe", command);

                        string path = Directory.GetCurrentDirectory() + "\\Archivos\\TempFiles\\cmdReceived.bat";

                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
                        {
                            string line = args2;
                            file.WriteLine(line);

                        }

                        Process p = Process.Start(path);
                        try
                        {
                            p.WaitForInputIdle();
                        }
                        catch (Exception)
                        {
                            returnValue = "Sin interfaz grafca";
                        }
                        p.WaitForExit();

                        returnValue = "Finalizado con exito - " + args1.Split("<<type>>")[1];


                        break;
                    case "SendMenssage":

                        //Opcion 1
                        MainWindow.shw = new Extras.SendMessage();
                        Extras.SendMessage.message = args2;
                        MainWindow.shw.Show();
                        //Fin Opcion 1

                        //Opcion 2
                        //if (shwView == true && MainWindow.shw.IsLoaded == true)
                        //{

                        //    MainWindow.shw.Visibility = Visibility.Hidden;
                        //    Extras.SendMessage.message = args2;

                        //    MainWindow.shw.Visibility = Visibility.Visible;
                        //}
                        //else
                        //{
                        //    if (MainWindow.shw.IsLoaded != true)
                        //    {
                        //        MainWindow.shw = new Extras.SendMessage();
                        //    }
                        //    Extras.SendMessage.message = args2;
                        //    MainWindow.shw.Show();
                        //}
                        //Fin opcion 2
                        shwView = true;
                        returnValue = "Finalizado con exito - " + args1.Split("<<type>>")[1];
                        break;
                    default:
                        break;
                }
            }
            return returnValue;
        }
    }
}
