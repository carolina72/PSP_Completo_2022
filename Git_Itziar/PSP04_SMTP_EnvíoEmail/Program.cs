using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace FTP
{
    class Program
    {
        public static void Main(String[] args)
        {

            //Dirección de cuenta desde la cual se quiere enviar un correo electrónico
            MailAddress origen = new MailAddress("birtpsp@gmail.com", "From BirtPSP");

            //Dirección de cuenta a la cual se quiere enviar un correo electrónico
            MailAddress destino = new MailAddress("igortazar@birt.eus", "To Profesora");

            //Se especifica información del servidor, protocolo, credenciales, ...de la conexión que se va a realizar
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(origen.Address, "Birt2022"),
                EnableSsl = true

            };

            //Se escribe el mensage que vamos a enviar indicando cual será el receptor y el emisor
            using (MailMessage mezua = new MailMessage(origen, destino)
            {
                Subject = "Mi primer mensaje:)",
                Body = "Hola, espero que este sea el primer mensaje de muchos"
            })

            //Se ejecuta el envío del mensaje.
            try
            {
                smtp.Send(mezua);

            }catch (Exception ex)
            {
                    //En caso de error se muestra por la consola.
                    Console.WriteLine(ex.ToString());
            }
            
        }
    }
}
