using GalaSoft.MvvmLight.Command;
using ServerTcp.Models;
using ServerTcp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ServerTcp.ViewModels
{
    public class RectangleViewModel : INotifyPropertyChanged
    {
        //Servicios
        RectangleService? servidor;
        Dispatcher dispatcher;
        //Propiedades notificables
        public ObservableCollection<Rectangulo> ListaDeRectangulos { get; set; } = new();

        //Comandos
        public ICommand IniciarCommand { get; set; }
        public ICommand DetenerCommand { get; set; }

        //Metodos
        public void Iniciar()
        {
            if (servidor!=null)
            {
                servidor.Iniciar();
            }
        }
        public void Detener()
        {
            if (servidor!=null)
            {
                servidor.Detener();
            }
        }
        public void Actualizar(string? nombre = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(nombre)));
        }

        //Constructor
        public RectangleViewModel()
        {
            
            dispatcher = Dispatcher.CurrentDispatcher;
            servidor = new RectangleService();
            servidor.RectanguloRecibido += Servidor_RectanguloRecibido;
            IniciarCommand = new RelayCommand(Iniciar);
            DetenerCommand = new RelayCommand(Detener);
            
        }

        //Metodos de suscripciones a eventos
        private void Servidor_RectanguloRecibido(Rectangulo obj)
        {
            dispatcher?.Invoke(() =>
            {
                if (obj != null)
                {
                    ListaDeRectangulos.Add(obj);
                }
            }
            );
            
        }

        //Eventos
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
