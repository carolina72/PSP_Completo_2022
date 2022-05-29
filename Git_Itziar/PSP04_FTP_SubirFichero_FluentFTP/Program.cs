using System;
using System.Net;
using System.Threading.Tasks;
using FluentFTP;

namespace FTP
{
    class Program
    {
        public static async Task Main()
        {
 
            // crear un objeto cliente FTP
            FtpClient client = new FtpClient("192.168.1.146");

            // Se especifican las credenciales
            client.Credentials = new NetworkCredential("ftpuser", "Birt123");

            // Conectar a servidor
            client.Connect();

            //Subimos un fichero local y le cambiamos el nombre en el servidor
            client.UploadFile(@"C:\Users\ulhi\source\repos\PSP\PSP04_FTP_SubirFichero_FluentFTP\fichero1Birt.txt", "fichero1BirtFluentFTP.txt");
            
            
            //Renombramos el fichero del servidor
            //client.Rename("fichero1BirtFluentFTP.txt", "fichero1BirtFluentFTPCambioNombre.txt");

            //Creamos un directorio en el servidor
            //client.CreateDirectory("DirectorioFluentFTP");

            //Movemos el fichero dentro del directorio que hemos creado
            //client.MoveFile("fichero1BirtFluentFTPCambioNombre.txt", "/DirectorioFluentFTP/fichero1BirtFluentFTPCambioNombre.txt");

            //Chequea de forma asíncrona si existe un fichero en el servidor con el nombre especificado
            bool ok = await client.FileExistsAsync("fichero1BirtFluentFTP.txt");

            //bool ok = await client.FileExistsAsync("fichero1BirtFluentFTPCambioNombre.txt");

            //bool ok = await client.FileExistsAsync("/DirectorioFluentFTP/fichero1BirtFluentFTPCambioNombre.txt");

            if (ok)
                Console.WriteLine(ok);
            client.Disconnect();


        }
    }
}
