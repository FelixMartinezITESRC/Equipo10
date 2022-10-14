using ClienteTcp.Models;
using ColorPicker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClienteTcp.Services
{
    public class SendRectangleService
    {
        TcpClient cliente = new TcpClient();
       
        public SendRectangleService()
        {
          
        }

        public bool Conectar(string ip)
        {
            try
            {
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), 27082);
                cliente.Connect(ipe);
                Thread.Sleep(100);
            }
            catch (Exception)
            {
                return true;
            }
            return false;
        }     

        public void EnviarRectangulo(Rectangulo r)
        {
            try
            {
                var stream = cliente.GetStream();
                var json = JsonConvert.SerializeObject(r);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception)
            {
                cliente.Close();
            }
           
        }
    }
}
