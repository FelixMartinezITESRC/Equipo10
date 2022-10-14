using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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


namespace ClienteTcp.Views
{
    /// <summary>
    /// Lógica de interacción para EnviarDatosView.xaml
    /// </summary>
    public partial class EnviarDatosView : UserControl
    {
        public EnviarDatosView()
        {
            InitializeComponent();
            MIREC.Fill = new SolidColorBrush(colorPick.SelectedColor);
        }

        private void colorPick_ColorChanged(object sender, RoutedEventArgs e)
        {
            if (MIREC!=null)
            {
                MIREC.Fill = new SolidColorBrush(colorPick.SelectedColor);
            }   
        }
        private void txtAncho_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MIREC!=null)
            {
                MIREC.Width = double.Parse(txtAncho.Text) / 2;
               
            }

           
        }

        private void txtAlto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MIREC!=null)
            {
                MIREC.Height = double.Parse(txtAlto.Text) / 2;
            }
          
        }

        private void txtX_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (MIREC != null)
            {
                Canvas.SetLeft(MIREC, double.Parse(txtX.Text) / 2);
            }
            

        }

        private void txtY_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MIREC != null)
            {
                Canvas.SetTop(MIREC, double.Parse(txtY.Text) / 2);
            }
        }

        private void slX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MIREC != null)
            {
                slX.Maximum = (350-MIREC.Width)*2;
            }
        }

        private void slAncho_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MIREC != null)
            {
                slX.Maximum = (350 - MIREC.Width) * 2;
            }
        }

       

        private void slAlto_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MIREC != null)
            {
                slY.Maximum = (250 - MIREC.Height) * 2;
            }
        }

        private void slY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MIREC != null)
            {
                slY.Maximum = (250 - MIREC.Height) * 2;
            }
        }

        private void colorPick_Loaded(object sender, RoutedEventArgs e)
        {

        }
        System.Timers.Timer t = new System.Timers.Timer(1000);
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            t.Start();

            btnDraw.IsEnabled = false;
            t.Elapsed += T_Elapsed;
        }

        private void T_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            t.Stop();
            App.Current.Dispatcher?.Invoke(()=>
                 btnDraw.IsEnabled = true
            );
           
        }
    }
}
