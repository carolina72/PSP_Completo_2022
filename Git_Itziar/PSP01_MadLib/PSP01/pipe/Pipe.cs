using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;


namespace pipe
{
    class Pipe
    {
        //Método Main
        //Descripción: Se encarga de abrir un proceso con el servidor, abrir un pipe con el servidor para su posterior comunicación,
        //genera unos buffer de comunicación y gestiona la lectura y escritura de unos cuentos MadLib. Es la parte cliente del programa.
        //Sobre todo se encarga de la comunicación con el usuario.
        static void Main(string[] args)
        {

            //Lanza el proceso servidor para la comunicación con el mismo.
            Process p;
            StartServer(out p);
            Task.Delay(1000).Wait();
            string operacion;
            try
            {

                //Client
                //Creación de conexión con Pipe y creación de buffer para la comunicación.
                var client = new NamedPipeClientStream("PSP01_UD01_Pipes");
                client.Connect();
                Console.WriteLine("Estableciendo conexión con el servidor");
                StreamReader reader = new StreamReader(client);
                StreamWriter writer = new StreamWriter(client);

                while (true)
                {
                    String palabra = String.Empty;
                    operacion = String.Empty;
                    bool lecturafichero = false;

                    do
                    {
                        //Se solicita nombre del cuento.
                        operacion = SolicitarNombreDelCuento();

                        //Gestión de si el fichero es válido o no.
                        Console.WriteLine("Tubo Cliente procesando datos: '{0}'", operacion);
                        writer.WriteLine(operacion);
                        writer.Flush();
                        operacion = reader.ReadLine();

                        if (operacion.Contains("Ficheros abierto."))
                        {
                            lecturafichero = true;

                        }
                        else
                        {
                            lecturafichero = false;

                        }
                        operacion = String.Empty;
                    } while (lecturafichero == false);


                    //Procesamiendo de los datos entre usuario y servidor
                    operacion= ProcesaDatos(writer, reader);

                    //Muestra el contenido del cuento por consola.
                    MostrarCuento(operacion,reader);

                }

                //Cierre de los buffer
                reader.Close();
                writer.Close();

                //Parar pipes
                if (p != null && !p.HasExited)
                {
                    p.Kill();
                    p = null;
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error: {0}  Apangado servidor por error", e.Message);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}  Apangado servidor por error", e.Message);
                Console.ReadLine();
            }
        }


        //Método  StartServer
        //Lanza el proceso servidor 
        //@ Process p1: Se devuelve como parámetro el proceso lanzado.
        static void StartServer(out Process p1)
        {
            // iniciar un proceso con el servidor y devolver
            ProcessStartInfo info = new ProcessStartInfo(@"..\..\..\..\pipeServidor\bin\Release\netcoreapp3.1\publish\win-x64\pipeServidor.exe");
            // su valor por defecto el false, si se establece a true no se "crea" ventana
            info.CreateNoWindow = false;
            info.WindowStyle = ProcessWindowStyle.Normal;
            // indica si se utiliza el cmd para lanzar el proceso
            info.UseShellExecute = true;
            p1 = Process.Start(info);
            
        }

        //Método  SolicitarNombreDelCuento
        //Solicita el nombre del cuento por teclado y devuelve un String 
        
        private static String SolicitarNombreDelCuento()
        {
            Console.WriteLine("\nIndica el nombre del cuento elegido:\n");
            String nombre = Console.ReadLine();
            nombre = "N " + nombre;
            return nombre;

        }

        //Método  SolicitarPalabra
        //Solicita el nombre del cuento por teclado y devuelve un String
        //@string operacion: palabra que se quiere enviar al servidor
        //devuelve un string: La palabra con palabra clave "P" por delante.
        private static String SolicitarPalabra(string operacion)
        {
            var input = String.Empty;
            Console.WriteLine("{0}:", operacion.Substring(2, operacion.Length - 2));
            input = Console.ReadLine();
            input = "P " + input;
            return input;
        }

        //Método  EscribirCabecera
        //Muestra cabecera del cuento
        private static void EscribirCabecera()
        {
            Console.WriteLine();
            Console.WriteLine("********************\n");
            Console.WriteLine("El cuento creado es:\n");
            Console.WriteLine("********************\n");
        }


        //Método  MostrarCuento
        //Muestra el cuento procesado por consola
        //@StreamReader read: Lectura del buffer
        //@string op: String al que se le pasa el contenido del buffer.
        private static void MostrarCuento(string op, StreamReader read)
        {
            
            EscribirCabecera();
            do
            {
                Console.WriteLine(op);
                op = read.ReadLine();
            }while (!string.IsNullOrEmpty(op));
        }

        //Método  ProcesaDatos
        //Procesa las palabras de intercambio entre el cliente y servidor.
        //@StreamReader read: Lectura del buffer
        //StreamWriter write: Escritura del buffer
        private static string ProcesaDatos(StreamWriter write, StreamReader read)
        {
            String operacion = String.Empty;
            String palabra = String.Empty;
            try { 
                operacion = read.ReadLine();
                Console.WriteLine("{0}",operacion);
                while ('P'.Equals(operacion[0]))
                {
                     palabra = SolicitarPalabra(operacion);
                     Console.WriteLine("Tubo Cliente procesando datos: '{0}'", palabra);
                     write.WriteLine(palabra);
                                //Console.ReadLine();
                     write.Flush();
                     operacion = read.ReadLine();
                     Console.WriteLine("Tubo Cliente recibiendo datos: '{0}'", operacion);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}  Error al procesar datos", e.Message);
                Console.ReadLine();
            }
            return operacion;
            
        }

    }
}
