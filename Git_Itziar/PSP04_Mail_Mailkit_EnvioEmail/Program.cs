using System;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace Correo
{
    class Program
    {
        public static void Main(string[] args)
        {

            //La clase MimeMessage, recoge los parámetros del mensaje que se va a enviar. Equivaldría al MailMessage de smtpClient de .NET
            var message = new MimeMessage();

            //Indicamos mediante el objeto MailboxAddress y el método From desde qué cuenta queremos enviar el email.
            message.From.Add(new MailboxAddress("CuentaBirtEnvio", "BirtPSP@gmail.com"));

            //Indicamos mediante el objeto MailboxAddress y el método To destino al que queremos enviar el mail
            message.To.Add(new MailboxAddress("Profesora", "igortazar@birt.eus"));

            //Método para el asunto
            message.Subject = "Cabecera de prueba Birt Mailkit";

            //TextPart nos ayudará a crear el cuerpo del email. Al constructor se le puede indica si el mensaje lo queremos enviar en texto plano o html.
            message.Body = new TextPart("plain")
            {
                Text = @"Hola desde Mailkit,

                        Quería probar el mensaje. 

                -- Saludos"
            };


            //Clase SmtpClient para enviar los emails (establecimiento de conexión con servidor)
            using (var client = new SmtpClient())
            {
                // Acepta todos los certificados de tipo  SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                //Nos conectaESpemos al servidor smtp de gmail
                //@param1: servidor
                //@param2: puerto
                //@param3: uso de SSL
                client.Connect("smtp.gmail.com", 587, false);

                // Note: Solo necesario si SMTP requiere de autenticación
                // utilizar una cuenta de prueba y si el de google establecer permiso para las aplicaciones
                client.Authenticate("BirtPSP@gmail.com", "Birt2022");

                //Método que envía el email.
                client.Send(message);

                //Desconexión para el envío de email.
                client.Disconnect(true);
            }
        }
    }
}
