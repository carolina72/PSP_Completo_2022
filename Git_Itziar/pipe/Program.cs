using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace pipe
{
    class Program
    {
        static void Main(string[] args)
        {
            Process p;
            StartServer(out p);
            Task.Delay(1000).Wait();
            Console.WriteLine("Arrancando Servidor");


            //Client
            var client = new NamedPipeClientStream("PipesOfPiece");
            Console.WriteLine("Conectando a Servidor");
            client.Connect();
            StreamReader reader = new StreamReader(client);
            StreamWriter writer = new StreamWriter(client);

            while (true)
            {
                Console.WriteLine("Escriba el texto que quiera procesar: ");
                string input = Console.ReadLine();
                if (String.IsNullOrEmpty(input)) break;
                Console.WriteLine(String.Join("","Enviado: ",input));
                writer.WriteLine(input);
                writer.Flush();
                Console.WriteLine(reader.ReadLine());
            }
        }

        static Process StartServer(out Process p1)
        {

            // El objeto info de tipo ProcessStartInfo recoge la información con las características que va a tener el proceso que se quiera crear.

            // creamos el objeto y la pasamos como argumento el ejecutable con el que se creará el proceso.
            // en este caso en concreto, este proceso se crea con un ejecutable creado previamente del proyecto pipesServidor.
            ProcessStartInfo info = new ProcessStartInfo(@"C:\Users\ulhi\source\repos\PSP\pipesServidor\bin\Release\netcoreapp2.1\publish\win-x64\pipesServidor.exe");

            // Método CreateNoWindow si es false (crea una ventana nueva) y si es true (no crea una ventana nueva). En nuestro caso sí que lo creará. 
            info.CreateNoWindow=false;

            // Se guarda información sobre si la ventana que creamos está maximizada, minimizada, oculta o de forma normal (en nuestro caso será una ventana normal).
            info.WindowStyle = ProcessWindowStyle.Normal;

            // UseShellExecute = true si queremos que se ejecute cuando iniciemos el proceso.
            info.UseShellExecute = true;

            // Arrancamos el proceso y guarda los datos de su creación en la variable p1 de tipo process
            // Este valor nos ayudará en un futuro a MATAR o ver el ESTADO del proceso.
            p1 = Process.Start(info);

            return p1;
        }
    }
}
