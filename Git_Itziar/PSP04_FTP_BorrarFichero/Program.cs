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
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://192.168.1.146/" + "/" + "fichBirt.txt");

            // Si no se especifican las credenciales se asignan unas credenciales de tipo anónimas. El servidor lo deberá permitir.
            request.Credentials = new NetworkCredential("ftpuser", "Birt123");

            //Recogemos en el atributo Method el tipo de acción que vamos a realizar: en este caso subir un fichero.
            request.Method = WebRequestMethods.Ftp.DeleteFile;


            //Se espera la respuesta y se muestra por consola.
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine("Fichero borrado  con código: " + response.StatusDescription);
            }
        }
    }
}