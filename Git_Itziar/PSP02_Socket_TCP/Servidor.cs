using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PSP03_Socket_TCP
{
    internal class Servidor
    {
        public static int Main(String[] args)
        {

            Servidor cliente = new Servidor();
            cliente.FuncionCliente();

            Console.WriteLine("Pulse intro para continuar");
            Console.ReadLine();

            return 0;
        }
        private void FuncionCliente()
        {
            Socket listener = null;
            Socket handler = null;
            try
            {
                //declaramos el puerto
                int port = 12000;
                string data = null;
                //Creamos el buffer para el envío y recepción de información
                byte[] bytes = new Byte[1024];

                //Recogemos la IP del servidor
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[1];

                //Creación del socket listener para recepcionar las peticiones del cliente
                listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                Console.WriteLine("Programa servidor iniciando.");

                //Asociamos el socket al puerto e ip del servidor
                IPEndPoint iPEndPoint = new IPEndPoint(ipAddress.Address, port);
                listener.Bind(iPEndPoint);

                //Quedamos a la escucha de un máximo de peticiones de cliente de 10 (en este caso sólo se trabajará con 1 cliente).
                listener.Listen(10);

                //Se establece la conexión con el cliente y abre un segundo socket para la comunicación
                handler = listener.Accept(); //Bloqueante.
                Console.WriteLine("Aceptada la conexión con el  cliente.");


                //Recepción de información
                while (true)
                {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }

                }

                //Procesamiento y envío de información.
                byte[] msg = Encoding.ASCII.GetBytes(data.ToUpper());
                handler.Send(msg);

                


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                handler.Close();
                listener.Close();
            }



        }
    }
}

