using ClienteTcp.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Newtonsoft.Json;
using System.Buffers;
using System.Threading;
using System.Drawing;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
using ClienteTcp.Services;
using ClienteTcp.Views;
using System.Windows.Controls;

namespace ClienteTcp.ViewModels
{
    public class RectangleViewModel : INotifyPropertyChanged
    {
        //Servicios
        SendRectangleService? service;

        //Vistas
        AccederServerView accederServerView;
        EnviarDatosView enviarDatosView;

        //Propiedades notificables
        private Rectangulo miRectangulo = new Rectangulo();
        public Rectangulo MiRectangulo
        {
            get { return miRectangulo; }
            set { miRectangulo = value; Actualizar(nameof(MiRectangulo)); }
        }

        private Cliente miCliente = new Cliente();
        public Cliente MiCliente
        {
            get { return miCliente; }
            set { miCliente = value; Actualizar(nameof(MiCliente)); }
        }

        private string error=string.Empty;

        public string Error
        {
            get { return error; }
            set { error = value; Actualizar(nameof(Error)); }
        }


        private Control? control;

        public Control? Control
        {
            get { return control; }
            set { control = value; Actualizar(nameof(Control)); }
        }


        //Comandos
        public ICommand EnviarDatosCommand { get; set; }
        public ICommand ConectarCommand { get; set; }

        //Metodos
        private void Conectar()
        {
            if (service!=null)
            {
                if (MiCliente.Nombre=="" || string.IsNullOrWhiteSpace(MiCliente.Nombre))
                {
                    Error = "Escriba su nombre";
                }
                else
                {
                                            
                    if (!service.Conectar(MiCliente.Ip))
                    {
                        Control = enviarDatosView;
                    }
                    else
                    {
                        Error = "Escriba una ip valida";
                    }
                    
                }
            }
           
        }
      
        private void EnviarDatos(Color c)
        {
            
            MiRectangulo.ColorDeFondo = c.ToString();
            if (service != null)
            {
                service.EnviarRectangulo(MiRectangulo);
            }
            
        }
        public void Actualizar(string? nombre = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        //Constructor
        public RectangleViewModel()
        {
            accederServerView = new AccederServerView() { DataContext=this};
            enviarDatosView = new EnviarDatosView() { DataContext = this };
            Control = accederServerView;
            EnviarDatosCommand = new RelayCommand<Color>(EnviarDatos);
            ConectarCommand = new RelayCommand(Conectar);
            service = new SendRectangleService();
        }

        
        //Eventos
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
