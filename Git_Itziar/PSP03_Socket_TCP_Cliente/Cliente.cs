using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PSP03_Socket_TCP
{
    internal class Cliente
    {
        public static int Main(String[] args)
        {

            Cliente servidor = new Cliente();
            servidor.FuncionServidor();

            Console.WriteLine("Pulse intro para continuar");
            Console.ReadLine();

            return 0;
        }
        private void FuncionServidor()
        {
            Socket sender = null;

            try
            {
                int port = 12000;
                string data = null;
                byte[] bytes = new Byte[1024];

                
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[1];
                sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                Console.WriteLine("Programa cliente iniciando.\n");

                //Conexión de socket al servidor
                IPEndPoint iPEndPoint = new IPEndPoint(ipAddress.Address, port); //Indicamos IP de servidor y puerto del servidor
                sender.Connect(iPEndPoint); //Se establece la conexión
                Console.WriteLine("Socket conectado a servidor {0}\n", sender.RemoteEndPoint.ToString()); //Mostramos por pantalla que todo ha ido correcto



                //Recepción de información
                Console.WriteLine("Cliente transfiriendo datos.\n");

    
                data = "Hola esto es un mensaje del cliente\n"; //Texto que queremos pasar a mayúsculas.
                byte[] msg = Encoding.ASCII.GetBytes(data + "<EOF>"); //Añadimos fin de fichero al texto
                sender.Send(msg); //Enviamos el texto

                //Esperamos la respuesta del servidor
                while (true)
                {
                    int bytesRec = sender.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }

                }
                //Mostramos la respuesta por pantalla
                Console.WriteLine(data);  

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                //Cerramos el socket
                sender.Close();

            }



        }
    }
}
