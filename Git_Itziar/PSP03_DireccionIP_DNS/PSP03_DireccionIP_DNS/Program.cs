using System;
using System.Net;
namespace ComunicacionPrimerosPaso
{
    class Program
    {
        static void Main(string[] args)
        {
            //Recoge la dirección ip de google
            IPHostEntry ipHostInfo = Dns.GetHostEntry("www.google.es");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            Console.WriteLine("La direcion de google es: {0}", ipAddress.ToString());

            //Recoge la dirección ip de la máquina local
            ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[1];
            Console.WriteLine("La direcion de esta maquina es: {0}", ipAddress);

        }
    }
}