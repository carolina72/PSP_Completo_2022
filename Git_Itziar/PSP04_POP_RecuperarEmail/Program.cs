
using System;
using OpenPop.Pop3;

namespace FTP
{
    class Program
    {
        static void  Main(string[] args)
        {

            //Creamos la instancia de la clase que hemos creado
            ConnectPop3 conPop3 = new ConnectPop3();

            //Descarga los mensajes
            List<OpenPop.Mime.Message> lstMensajes = conPop3.GetMessages();

            //Mostrará por consola el contenido de los emails
            if (lstMensajes != null)
                foreach (var mensaje in lstMensajes)
                {
                    
                    Console.WriteLine(mensaje.Headers.Subject); // Asunto.
                   // Console.WriteLine(mensaje.Headers.From); // Email del que te envi&#243; el mensaje.
                   // Console.WriteLine(mensaje.Headers.ContentDescription); //out: SevenBit.
                   // Console.WriteLine(mensaje.Headers.ContentType);
                   // Console.WriteLine(mensaje.Headers.Date); // Fecha larga.
                   // Console.WriteLine(mensaje.Headers.DateSent); // Fecha corta.
                   // Console.WriteLine(mensaje.Headers.Importance); // Out: Normal.
                   // Console.WriteLine(mensaje.Headers.MimeVersion); // Versi&#243;n.
                   //  Console.WriteLine(mensaje.Headers.MessageId);

                    Console.WriteLine("\n");
                }

        }
        public class ConnectPop3
        {
            //Definiremos los parámetros de conexión.
            public string usuario = "birtpsp@gmail.com";
            public string pass = "Birt2022";
            public int port = 995;
            public bool useSSL = true;
            public string Hostname = "pop.gmail.com";

            //colección de objetos de tipo Message(emails)
            public List<OpenPop.Mime.Message> GetMessages()
            {
                using(Pop3Client client = new Pop3Client())
                {
                    //Pasamos como parámetro datos para conexionado a servidor.
                    client.Connect(Hostname, port, useSSL);
                    client.Authenticate(usuario, pass);

                    //Recoge el número de emails que va a recibido y lo guardamos como contador en una variable.
                    int messageCount = (client.GetMessageCount());
                    List<OpenPop.Mime.Message> listaMensajes = new List<OpenPop.Mime.Message>(messageCount);

                    //Recorremos todos los mensajes mediante el índice
                    for(int i=messageCount; i>0; i--)
                    {
                        //Agregamos los correos en la lista, mediante el índice
                        listaMensajes.Add(client.GetMessage(i));
                    }
                    return listaMensajes;
                }
            }


        }
    }
}
