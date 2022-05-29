using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PSP03
{
    public class ServidorTCP
    {
        private readonly TcpListener listener;
        private bool listening;

        public static int Main(String[] args)
        {
            Int32 port = 80;
            //Int32 port = 13000;
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress localAddr = ipHostInfo.AddressList[1];
            //IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            ServidorTCP servidor = new ServidorTCP(localAddr, port);

            ///string msg = "Quiero que me conviertas a mayúscula este texto";
            servidor.StartAsync();

            return 0;
        }
        public ServidorTCP(IPAddress address, int port)
        {
            listener = new TcpListener(address, port);
            StartAsync();
        }

        //SocketInformationOptions
        public bool Listening => listening;
        
        private void StartAsync()
        {
            listener.Start();
            listening = true;

            Console.WriteLine("[Servidor] Servidor arrancado.");
            try
            {
                while (true)
                {
                    Task.Run(async () =>
                    {
                        TcpClient cliente = await listener.AcceptTcpClientAsync();
                        Console.WriteLine("[Servidor] Cliente conectado.");
                        using (var networkStream = cliente.GetStream())
                        {
                            var buffer = new byte[4096];
                            Console.WriteLine("[Servidor] Leyendo desde el cliente.");
                            var byteCount = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                            string request = Encoding.UTF8.GetString(buffer, 0, byteCount);
                            Console.WriteLine("[Servidor] El cliente ha escrito: {0}", request);
                            byte[] ServerResponseBytes = Encoding.UTF8.GetBytes(request.ToUpper());
                            await networkStream.WriteAsync(ServerResponseBytes, 0, ServerResponseBytes.Length);
                            Console.WriteLine("[Servidor] Se ha la enviado respuesta.");

                        }

                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            finally
            {
                //Cuando el thread finalice se pone el listening a false
                listening = false;
            }
        }
    }
}