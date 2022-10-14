using Newtonsoft.Json;
using ServerTcp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ServerTcp.Services
{
    public class RectangleService
    {
        private TcpListener? servidor { get; set; }
        public event Action<Rectangulo>? RectanguloRecibido;

        public void Iniciar()
        {
            if (servidor==null)
            {
                
                IPEndPoint host = new IPEndPoint(IPAddress.Any, 27082);
                servidor=new TcpListener(host);

                Thread subProcesoEscuhar = new Thread(new ThreadStart(Escuchar));
                subProcesoEscuhar.Start();
            }
        }

        public void Detener()
        {
            if (servidor!=null)
            {
                
                servidor.Stop();
                servidor= null;
            }
        }

        public void Recibir(object? tcpCliente)
        {
            try
            {
                if (tcpCliente != null)
                {
                    TcpClient cliente = (TcpClient)tcpCliente;
                    var stream = cliente.GetStream();

                    while (cliente.Connected)
                    {
                        if (cliente.Available > 0)
                        {
                            byte[] buffer = new byte[cliente.Available];
                            stream.Read(buffer, 0, buffer.Length);
                            Rectangulo? rectangulo = JsonConvert.DeserializeObject<Rectangulo>(Encoding.UTF8.GetString(buffer));
                            if (rectangulo != null)
                            {
                                RectanguloRecibido?.Invoke(rectangulo);
                            }
                        }
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception)
            {

            }
                
            
           
           
        }

        private void Escuchar()
        {
            Thread hiloRecibir;
            try
            {
                if (servidor != null)
                {
                    servidor.Start();
                    while (servidor.Server.IsBound)
                    {
                        var clienteNuevo = servidor.AcceptTcpClient();
                        hiloRecibir= new Thread(new ParameterizedThreadStart(Recibir));
                        hiloRecibir.Start(clienteNuevo);
                        hiloRecibir.IsBackground=true;
                    }
                }
            }
            catch (Exception)
            {
                Detener();
            }
            
        }
    }
}
