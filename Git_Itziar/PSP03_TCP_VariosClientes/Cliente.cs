using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClienteSincrono
{

    public class TCPCliente
    {

        TcpClient socket = null;
        NetworkStream network = null;
        StreamWriter writer = null;
        StreamReader reader = null;
        public static int Main(String[] args)
        {
            TCPCliente appcliente = new TCPCliente();
            string servidor = "127.0.0.1";
            Int32 port = 13000;
            appcliente.Connect(servidor, port);
            appcliente.ControlDatos();
            appcliente.Cerrar();
            Console.Read();
            return 0;
        }
        public TCPCliente()
        {

        }
        private void Connect(String server, Int32 port)
        {
            try
            {

                this.socket = new TcpClient(server, port);
                Console.WriteLine("Socket Cliente creado.");
                network = socket.GetStream();
                this.writer = new StreamWriter(network);
                this.reader = new StreamReader(network);
                Console.WriteLine("Buffer de escritura y lectura creados.");


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        private void ControlDatos()
        {
            string datouser = string.Empty;
            int num = int.MaxValue;

            try
            {
                //Envia y recibe texto
                

                Console.WriteLine("Indica el texto que quieres pasar a mayúscula:\n");
                datouser = Console.ReadLine();
                writer.WriteLine(datouser+"<EOF>");
                writer.Flush();
                datouser = reader.ReadLine();
                Console.WriteLine(datouser);


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        private void Cerrar()
        {
            this.socket.Close();
            this.writer.Close();
            this.network.Close();
            this.socket.Close();
        }


    }
}