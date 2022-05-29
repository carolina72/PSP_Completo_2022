using System;
using System.IO;
using System.Net;
using System.Text;

namespace FTP
{
    class Program
    {
        public static void Main(String[] args)
        {

            // creamos una conexión FTP
            //Indicamos servidor al que nos vamos a conectar. Nuestro servidor no dispone de ninguna dirección URL o ULI y por lo tanto lo adaptamos a la dirección IP
            //En este caso vamos a subir un fichero local y necesitamos indicarle el nombre del fichero que va a recibir en el servidor.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.dlptest.com/" + "/" + "fichBirtPublico.txt");

            // Si no se especifican las credenciales se asignan unas credenciales de tipo anónimas. El servidor lo deberá permitir.
            request.Credentials = new NetworkCredential("dlpuser", "rNrKYTX9g7z3RgJRmxWuGHbeu");

            //Recogemos en el atributo Method el tipo de acción que vamos a realizar: en este caso subir un fichero.
            request.Method = WebRequestMethods.Ftp.UploadFile;


            byte[] fileContents;

            //Recogemos el contenido del fichero en un buffer
            using (StreamReader sourceStream = new StreamReader("fichero.txt"))
            {
                fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            }

            //Recogemos en el atributo ContentLength del objeto request
            request.ContentLength = fileContents.Length;

            //Creamos un objeto de tipo Stream para enviar la información
            //Hacemos uso del método write, pasamos el contenido del fichero, offset(0) indicando el comienzo del fichero  y longitud del fichero.
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            //Se espera la respuesta y se muestra por consola.
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine("Fichero subido con código: " + response.StatusDescription);
            }
        }
    }
}