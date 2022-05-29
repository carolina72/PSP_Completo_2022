using System;
using System.IO;
using System.Net;
using System.Text;


namespace HTTP
{
    class Program
    {
        public static void Main(String[] args)
        {

            // Creamos el objeto cliente

            WebClient client = new WebClient();
                      
            //Se abre la página y se guarda en objeto stream
            Stream data = client.OpenRead("https://www.google.com");
            //Se prepara para la lectura
            StreamReader lectura = new StreamReader(data);
            //Se lee el contenido
            string resultado = lectura.ReadToEnd();
            //Muestra el resultado
            Console.WriteLine(resultado);

            //Cerramos los objetos de tipo stream
            data.Close();
            lectura.Close();

        }
    }
}