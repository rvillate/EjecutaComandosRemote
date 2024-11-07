using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using Reportes_y_Herramientas.Extras;

using Reportes_y_Herramientas.Connections;
using System.Threading;
using MySql.Data.MySqlClient;


//202207042053
//Nombre de BD: ReportesYHerramientas_202207042053
//ID Documento: IK9CjoNUevf4k1aqZ4dE

namespace Reportes_y_Herramientas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        internal static Extras.SendMessage shw = new Extras.SendMessage();





        internal static string nombreLogActual = "log.txt";
        MySqlConnection con = new MySqlConnection();
        Boolean bandera1Stoped = true;
        string id_app = "";
        public MainWindow()
        {
            InitializeComponent();
            id_app = Extras.LoadData.Id_app();
            name_id_app.Content = "id: " + id_app;
            ExecuteProgram();

            if (Extras.LoadData.visible_app().ToLower() == "false")
            {
                this.Visibility = Visibility.Hidden;
            }


            //Carga al metodo de Interfaz

        }
        private void UserIntefaceText(string addText)
        {

            this.Dispatcher.Invoke(() =>
            {
                if (txt_preview_code.Text.Length > 10000)
                {
                    txt_preview_code.Text = "Se limpia por ser mayor a 10000 caracteres.";
                }
                txt_preview_code.Text = addText + "\r\n" + txt_preview_code.Text;
            });
        }

        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                bandera1Stoped = false;
            }
            else
            {
                bandera1Stoped = true;
            }
            ExecuteProgram();

        }

        private async void ExecuteProgram()
        {
            MySqlDataReader dr;

            if (con.State != System.Data.ConnectionState.Open)
            {
                ellipseButton.IsEnabled = false;
                status_color_DB.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFBD00");
                await Task.Run(() =>
               {
                   con = MyDBSQL.ConectarDB("host", port, "database", "user", "pass");
               });
                if (con.State == System.Data.ConnectionState.Open && bandera1Stoped == true)
                {
                    ellipseButton.IsEnabled = true;
                    status_color_DB.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FF04");
                    UserIntefaceText("Conectado a Base de Datos.");

                    //Actions Start

                    await Task.Run(() =>
                    {

                        while (bandera1Stoped)
                        {
                            UpdateStateCircles(true);

                            if (con.State != System.Data.ConnectionState.Open)
                            {
                                con = MyDBSQL.ConectarDB("host", port, "database", "user", "pass");

                                if (con.State == System.Data.ConnectionState.Open)
                                {
                                    UserIntefaceText("Conectado a Base de Datos.");
                                }

                                this.Dispatcher.Invoke(() =>
                                {
                                    status_color_DB.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FF04");
                                });
                                //break;
                            }
                            MySqlCommand cmd = new MySqlCommand("Select * From ReportesYHerramientas_202207042053 WHERE Estado='Corriendo' OR Estado='Pendiente' AND id_app = '" + id_app + "'", con);

                            try
                            {
                                dr = cmd.ExecuteReader();

                            }
                            catch (Exception ex)
                            {
                                if (con.State != System.Data.ConnectionState.Open)
                                {
                                    UserIntefaceText("Base de datos no está abierta: Reintentando...");

                                }
                                else
                                {
                                    UserIntefaceText("Error: " + ex.Message);
                                }
                                this.Dispatcher.Invoke(() =>
                                {
                                    status_color_DB.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF0000");
                                });
                                continue;
                            }


                            if (dr.Read())
                            {

                                int IDReporte = Convert.ToInt32(dr["id"]);
                                string estado = Convert.ToString(dr["estado"]);
                                string solicitud = Convert.ToString(dr["solicitud"]);

                                UserIntefaceText(IDReporte + " - " + solicitud);


                                DateTime hora_solicitud = Convert.ToDateTime(Convert.ToString(dr["hora_solicitud"]));
                                string argumentos_1 = Convert.ToString(dr["argumentos_1"]);
                                string argumentos_2 = Convert.ToString(dr["argumentos_2"]);
                                string argumentos_3 = Convert.ToString(dr["argumentos_3"]);
                                string log_estado = Convert.ToString(dr["log_estado"]);
                                dr.Close();

                                //Aqui va el codigo a ejecutar luego de haber leido el registro
                                this.Dispatcher.Invoke(() =>
                                {
                                    string a = class_router.selection_program(argumentos_1, argumentos_2);
                                    UserIntefaceText("Finaliza ejecución, se ejecutó por '" + a + "'");
                                });
                                //Aqui va el codigo a ejecutar luego de haber leido el registro

                                MyDBSQL.ActualizarDB("UPDATE `ReportesYHerramientas_202207042053` SET `estado`='Terminado',`log_estado`='Proceso finalizado sin inconvenientes' WHERE id=" + IDReporte, con);

                            }
                            else
                            {
                                try
                                {
                                    dr.Close();
                                }
                                catch (Exception)
                                {
                                }
                                Thread.Sleep(1000);
                            }
                        }
                    });

                    //Actions End
                }
                else
                {
                    //Intento conectar, pero no lo logró
                    UserIntefaceText("No se logró conectar a la Base de Datos");
                    status_color_DB.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF0000");


                    await Task.Run(() =>
                    {
                        Thread.Sleep(5000);
                    });

                    UserIntefaceText("Reintentando...");
                    ExecuteProgram();
                }

            }
            else
            {
                await Task.Run(() =>
                {
                    UpdateStateCircles(false);

                    MyDBSQL.DesconectarDB(con);
                    UserIntefaceText("Desconectado de Base de Datos");

                });
                ellipseButton.IsEnabled = true;
                status_color_DB.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF0000");
            }
        }

        int count = 0;
        private void UpdateStateCircles(Boolean status)
        {
            this.Dispatcher.Invoke(() =>
            {


                if (status)
                {
                    switch (count)
                    {
                        case 0:
                            fillStatus1.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#D6D6D6");
                            fillStatus2.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#D6D6D6");
                            fillStatus3.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#00AAFF");
                            fillStatus4.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#D6D6D6");
                            fillStatus5.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#D6D6D6");
                            count++;
                            break;
                        case 1:
                            fillStatus1.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#D6D6D6");
                            fillStatus2.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#00AAFF");
                            fillStatus3.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#D6D6D6");
                            fillStatus4.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#00AAFF");
                            fillStatus5.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#D6D6D6");
                            count++;
                            break;
                        case 2:
                            fillStatus1.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#00AAFF");
                            fillStatus2.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#D6D6D6");
                            fillStatus3.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#D6D6D6");
                            fillStatus4.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#D6D6D6");
                            fillStatus5.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#00AAFF");
                            count = 0;
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    fillStatus1.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF0000");
                    fillStatus2.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF0000");
                    fillStatus3.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF0000");
                    fillStatus4.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF0000");
                    fillStatus5.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF0000");
                    count = 0;
                }
                //Thread.Sleep(500);
            });
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Log.Guardar("Finaliza aplicación", "", 0);
            SystemTools.EjecutarScript("closeMe.bat");
        }
    }
}
