using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClienteSincrono
{

    public class TCPCliente
    {

        public static int Main(String[] args)
        {
            string servidor = "52.174.65.187";
            //string servidor = "127.0.0.1";

            string msg = "Quiero que me conviertas a mayúscula este texto";


            Connect(servidor, msg);
            return 0;
        }
        static void Connect(String server, String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                //Int32 port = 13000;
                Int32 port = 80;
                TcpClient client = new TcpClient(server, port);

                

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);

                Console.WriteLine("[Cliente]Enviado: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("[Cliente]Recibido: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("[Cliente]Excepción de argumento nulo: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("[Cliente]Excepción de Socket: {0}", e);
            }

            Console.WriteLine("\n Pulsa intro para continuar");
            Console.Read();
        }
    }
}