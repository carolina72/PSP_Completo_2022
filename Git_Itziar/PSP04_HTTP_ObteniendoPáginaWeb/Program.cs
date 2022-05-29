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
            
            // Obtenemos el objeto solicitud a una petición WebRequest.

            WebRequest solicitud = WebRequest.Create("https://www.google.com");

            // Obtenemos la respuesta a la solicitud.
            WebResponse respuesta = solicitud.GetResponse(); //Cuidado es bloqueante para que no lo sea necesitamos el método GestResponseAsync().

            //Recogemos la respuesta en un objeto Stream para mostrarlo por consola
            Stream data = respuesta.GetResponseStream();
            string html = string.Empty;



            //Creamos el objeto StreamReader para tratar la respuesta mediante métodos de tipo Stream
            using (StreamReader lectura = new StreamReader(data))
            {
                html = lectura.ReadToEnd();
            }

            //Mostramos el resultado como respuesta.
            Console.WriteLine(html);
           
        }
    }
}