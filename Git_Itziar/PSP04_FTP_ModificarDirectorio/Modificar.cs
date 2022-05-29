using System;
using System.IO;
using System.Net;
using System.Text;

namespace FTP
{
    class Modificar
    {
        public static void Main(String[] args)
        {
            FtpWebRequest request = null;

        

            // creamos una conexión FTP
            //Indicamos servidor al que nos vamos a conectar. Nuestro servidor no dispone de ninguna dirección URL o ULI y por lo tanto lo adaptamos a la dirección IP
            //En este caso vamos a crear un directorio
            string serverUri = "ftp://192.168.1.146/BirtLHDir";
            request = (FtpWebRequest)WebRequest.Create(serverUri);

            request.Method = WebRequestMethods.Ftp.Rename;
            // Si no se especifican las credenciales se asignan unas credenciales de tipo anónimas. El servidor lo deberá permitir.
            request.Credentials = new NetworkCredential("ftpuser", "Birt123");

            request.RenameTo = "BirtLHDirNombreCambio";
            //using (request.GetRequestStream())
            //Se espera la respuesta y se muestra por consola.
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine("Modificado el directorio con código: " + response.StatusDescription);
            }



        }
    }
}