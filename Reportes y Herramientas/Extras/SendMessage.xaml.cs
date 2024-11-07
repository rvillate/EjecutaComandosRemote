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
using System.Windows.Shapes;

namespace Reportes_y_Herramientas.Extras
{
    /// <summary>
    /// Lógica de interacción para SendMessage.xaml
    /// </summary>
    public partial class SendMessage : Window
    {
        internal static string message = "";
        public SendMessage()
        {
            InitializeComponent();
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                textViewer.Text = message;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

        }
    }
}
