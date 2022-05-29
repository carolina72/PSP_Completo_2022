using System;
using System.IO;
using System.IO.Pipes;


namespace pipeServidor
{
    
    class PipeServidor
    {
        private const String FICHEROSALIDA = @"..\..\..\..\cuentos\resultado.txt";

        //Método Main
        //Descripción: Se encarga de abrir un pipe con el cliente, genera unos buffer de comunicación y gestiona la lectura y escritura de unos cuentos MadLib.
        static void Main(string[] args)
        {
            try
            {
                // Establecimiento de conexión con pipe y creación de buffer
                var server = new NamedPipeServerStream("PSP01_UD01_Pipes");
                server.WaitForConnection();
                Console.WriteLine("Conexión a servidor establecida.");
                Console.WriteLine("Pipe Servidor esperando datos.\n");
                StreamReader reader = new StreamReader(server);
                StreamWriter writer = new StreamWriter(server);

               
                //Inicialización de objetos
                StreamReader srfich = StreamReader.Null;

                //Procesamiento de datos
                //Comunicación con cliente
                while (true)
                {

                    //Crea un Stream para la lectura del cuento
                    srfich = AbrirCuento(reader, writer);
                    //Procesa las líneas del cuento y lo guarda en un fichero.
                    ProcesarLinea(srfich, writer,reader, FICHEROSALIDA);
                    //Envía el contenido del fichero para que lo muestre el cliente por consola.
                    EnviarCuento(writer, FICHEROSALIDA);
                }

                //Cierre de todos los Stream
                srfich.Close();
                reader.Close();
                writer.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error: {0}  Apangado servidor por error de fichero", e.Message);
                Console.ReadLine();
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}  Apangado servidor por error", e.Message);
                Console.ReadLine();
            }
           
        }

        //Método AbrirCuento
        //Descripción: Abre un cuento si existe el fichero y sino le envía al cliente para que vuelva a solicitar otro nombre
        //@StreamReader read: Buffer de lectura del pipe.
        //@StreamWriter write: Buffer de escritura del pipe.
        //Devuelve parámetro StreamReader, buffer para manipular el contenido del cuento (fichero).

        private static StreamReader AbrirCuento(StreamReader read, StreamWriter write)
        {
            String line = String.Empty;
            String fichero = String.Empty;
            StreamReader str = StreamReader.Null;
            
            try
            { 
                do
                {
                    line = read.ReadLine().ToLower();
                    Console.WriteLine("Intentando acceder al  fichero:" + (line.Substring(2, line.Length - 2) + ".txt\n"));
                    fichero = @"..\..\..\..\cuentos\" + line.Substring(2, line.Length - 2) + ".txt";
                    
                    if (!File.Exists(fichero))
                    {
                        Console.WriteLine("El fichero no existe");
                        write.WriteLine("El fichero no existe");
                        write.Flush();
                    }

                } while (!File.Exists(fichero));

                str = new StreamReader(fichero);
                Console.WriteLine("Fichero abierto. {0}\n", fichero);
                write.WriteLine("Ficheros abierto.");
                write.Flush();
            }
            catch (Exception e)
                    {
                        Console.WriteLine("Error: {0}  en la lectura de fichero", e.Message);
                        Console.ReadLine();
            }
            return str;

        }

        //Método ProcesarLinea
        //Descripción:Procesa las líneas que va leyendo del buffer del cuento.
        //Envía al cliente las palabras a modificar, recepciona del buffer su contenido y guarda la línea en el fichero.
        //@StreamReader readfich: Buffer de lectura del cuento
        //@StreamWriter writebuffer: Buffer de escritura del pipe.
        //@StreamReader readbuffer: Buffer de lectura del pipe.
        //@String fichero: Nombre del fichero donde se debe guardar el cuento.
        
        private static void ProcesarLinea(StreamReader readfich, StreamWriter writebuffer, StreamReader readbuffer, String fichero)
        {
            String proclinea = String.Empty;
            String linea = String.Empty;
            StreamWriter fichsalida = StreamWriter.Null;
            try
            {
                proclinea = readfich.ReadLine();
                Console.WriteLine("Tubo Servidor procesando datos: '{0}'\n", proclinea);
                fichsalida = new StreamWriter(fichero);

                while (proclinea != null)
                {


                    while (proclinea.Contains("<") && proclinea.Contains(">"))
                    {

                        int indice1 = proclinea.IndexOf('<');
                        int indice2 = proclinea.IndexOf('>');
                        var texto = proclinea.Substring((indice1 + 1), ((indice2 - indice1) - 1));


                        writebuffer.WriteLine("P " + proclinea.Substring((indice1 + 1), ((indice2 - indice1) - 1)));
                        writebuffer.Flush();
                        Console.WriteLine("Tubo Servidor emitiendo datos: '{0}'\n", proclinea.Substring((indice1 + 1), ((indice2 - indice1) - 1)));
                        linea = readbuffer.ReadLine();
                        Console.WriteLine("Tubo Servidor recibiendo datos: '{0}'\n", linea);


                        var palabra1 = proclinea.Substring(indice1, ((indice2 - indice1) + 1));
                        var palabra2 = linea.Substring(2, linea.Length - 2);
                        proclinea = proclinea.Replace(palabra1, palabra2);


                    }

                    fichsalida.WriteLine(proclinea);

                    proclinea = readfich.ReadLine();
                    Console.WriteLine("Leyendo datos de fichero para procesar: '{0}'\n", proclinea);
                    linea = "";
                }
                fichsalida.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}  al procesar los datos", e.Message);
                Console.ReadLine();
            }

        }

        //Método EnviarCuento
        //Descripción:Lee el cuento procesado desde el fichero y se lo envía al cliente para que lo muestre.
        //@StreamWriter write: Buffer de escritura del pipe.
        //@String fichero: Nombre del fichero donde se debe guardar el cuento.
        private static void EnviarCuento(StreamWriter write, String fichero)
        {
            String proclinea = String.Empty;
            try { 
                var read = new StreamReader(fichero);
                proclinea = read.ReadToEnd();


                Console.WriteLine("Tubo Servidor emitiendo datos cuento.\n");
                write.WriteLine(proclinea);
                write.Flush();
                read.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}  en el envio de cuento", e.Message);
                Console.ReadLine();
            }
}
    }
}      

