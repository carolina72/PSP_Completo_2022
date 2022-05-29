using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClienteSincrono
{

    public class TCPServidor
    {
        public int num = 15;
        private Object o = new object();
        public static int Main(String[] args)
        {

            //Inicialización de variables

            TcpClient socketcliente = null;
            NetworkStream network = null;
            StreamWriter writer = null;
            StreamReader reader = null;
            


            //Preparación de datos para el listener
            TCPServidor appserver = new TCPServidor();
            string servidor = "127.0.0.1";
            IPAddress ipserver = IPAddress.Parse(servidor);
            Int32 port = 13000;

       


            TcpListener listener = new TcpListener(ipserver, port);
            Console.WriteLine("Socket lister creado");
            listener.Start();

            //Se crea un thread por cada cliente que se conecte
            while (true)
            {
                socketcliente = listener.AcceptTcpClient(); //linea bloqueante
                Console.WriteLine("Conexión con cliente establecida.");
                Thread t = new Thread(() => appserver.ControlDatos(socketcliente));
                t.Start();
            }

            socketcliente.Close();

            
            Console.Read();
            return 0;
        }
        public TCPServidor()
        {

        }

        private void ControlDatos(TcpClient socket)
        {
            NetworkStream network = socket.GetStream();
            StreamWriter writer = new StreamWriter(network);
            StreamReader reader = new StreamReader(network);
            string data = string.Empty;
            

            try
            {
                while (true)
                {
                    data += reader.ReadLine();
                    if (data.Contains("<EOF>"))
                    {
                        lock (o)
                        {
                            if ((num-7) > 0)
                            {
                                num-=7;
                            }
                            else
                            {
                                num = 0;
                            }
                        }

                        data = num.ToString();
                        break;
                    }
                    
                }
                writer.WriteLine(data);
                writer.Flush();
                Console.WriteLine(data);
                               


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            writer.Close();
            network.Close();
            reader.Close();

        }


    }
}
