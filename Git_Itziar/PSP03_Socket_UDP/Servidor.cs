using System;
using System.Net;
using System.Net.Sockets;
using System.Text;



namespace PSP03_Socket_UDP
{
    internal class Servidor
    {
        public static int Main(String[] args)
        {

            Servidor servidor = new Servidor();
            servidor.FuncionServidor();

            Console.WriteLine("Pulse intro para continuar");
            Console.ReadLine();

            return 0;
        }
        private void FuncionServidor()
        {
            Socket socketServidor = null;

            try
            {
                //declaramos el puerto
                int port = 2000;
                int numDatos = 0;
                string data = null;
                //Creamos el buffer para el envío y recepción de información
                byte[] bytesRecibidos = new Byte[1024];
                byte[] bytesEnviados = new Byte[1024];

                //Recogemos la IP del servidor
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[1];

                //Creación del socket para la escucha de peticiones
                socketServidor = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                Console.WriteLine("Programa servidor UDP iniciando.");

                //Asociamos el socket al puerto e ip del servidor
                IPEndPoint iPEndPointSrv = new IPEndPoint(ipAddress, port);
                socketServidor.Bind(iPEndPointSrv);

                //Creamos objetos con la información del receptor y preparamos el objeto EndPoint para recoger la información del emisor(cliente) cuando se realice la conexión.
                IPEndPoint receptor = new IPEndPoint(ipAddress, 0);
                EndPoint emisor = (EndPoint)(receptor);

                while(true)
                {
                    //Recepción de datos por parte del servidor
                    data = string.Empty;
                    numDatos = socketServidor.ReceiveFrom(bytesRecibidos, ref emisor); //bloqueante
                    Console.WriteLine("Datos recibidos del cliente: {0}", emisor.ToString());
                    data = Encoding.ASCII.GetString(bytesRecibidos, 0, numDatos);
                    Console.WriteLine(data);

                    //Envío de datos
                    data = string.Empty;
                    data = Console.ReadLine();
                    bytesEnviados = Encoding.ASCII.GetBytes(data);
                    socketServidor.SendTo(bytesEnviados, bytesEnviados.Length, SocketFlags.None, emisor);

                    if (data.Equals("Agur"))
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                socketServidor.Close();
            }
        }
    }
}

