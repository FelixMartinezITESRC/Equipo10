using ServerTcp.ViewModels;
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

namespace ServerTcp.Views
{
    /// <summary>
    /// Lógica de interacción para CanvasView.xaml
    /// </summary>
    public partial class CanvasView : Window
    {
        public CanvasView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((RectangleViewModel)this.DataContext).IniciarCommand.Execute(null);
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((RectangleViewModel)this.DataContext).DetenerCommand.Execute(null);
        }
    }
}
