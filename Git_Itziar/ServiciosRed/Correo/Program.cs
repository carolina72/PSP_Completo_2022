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
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Joey Tribbiani", "email"));
            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "email"));
            message.Subject = "How you doin'?";

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Chandler,

I just wanted to let you know that Monica and I were going to go play some paintball, you in?

-- Joey"
            };

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                // utilizar una cuenta de prueba y si el de google establecer permiso para las aplicaciones
                // poco seguras puedan tener acceso 
                client.Authenticate("user", "pass");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
