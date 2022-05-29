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
            // create an FTP client
            FtpClient client = new FtpClient("192.168.33.10");

            // if you don't specify login credentials, we use the "anonymous" user account
            client.Credentials = new NetworkCredential("user123", "user123");

            // begin connecting to the server
            client.Connect();

            //await client.UploadFileAsync("prueba.txt", "/home/user123/cliente/prueba.txt");
            bool subido = client.UploadFile("prueba.txt", "prueba.txt", existsMode: FtpExists.Overwrite, createRemoteDir: true);
            bool ok = await client.FileExistsAsync("/home/user123/prueba.txt");
            if (ok)
                Console.WriteLine(ok);
            client.Disconnect();


        }
    }
}
