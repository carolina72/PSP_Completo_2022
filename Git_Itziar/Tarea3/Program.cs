using System;
using System.Diagnostics;
using System.Threading;
namespace tareas
{
    public class Programa
    {
        public static void Main(string[] args)
        {
            /*
            //Ejecutamos la primeraTarea
            //creamos objeto de tipo tarea 
            Task task = new Task(EjecutarPrimeraTarea);

            //arrancamos la tarea
            task.Start();
            */
            //Ejecutamos la segundaTarea
            /*
            Task task = new Task(EjecutarSegundaTarea);
            task.Start();
            Task task2 = new Task(EjecutarSegundaTarea);
            task2.Start();

            */

            //Ejecutamos la tarea de la tercera forma
            
            Task task = new Task(EjecutarSegundaTarea);
            task.Start();
            Task task2 = new Task(() =>
            {
                for (int j = 0; j < 100; j++)
                {
                    // guardamos en variable miThread el ID del Thread que se está ejecutando
                    var miThread = Thread.CurrentThread.ManagedThreadId;

                    //Le damos un segundo
                    Thread.Sleep(1000);

                    //Mostramos en consola el id del thread que se está ejecutando en ese mismo instante.
                    Console.WriteLine("Esta vuelta de for en el MAIN corresponde al Hilo o Thread:{0}", miThread);
                }
            });
           
            task2.Start();

            
            Console.ReadLine();
        }

        private static void EjecutarPrimeraTarea()
        {
            //Creamos una tarea lo más sencilla posible WriteLine
            Console.WriteLine("Esto es una tarea");
        }

        private static void EjecutarSegundaTarea()
        {
            //Creamos una tarea lo más sencilla posible WriteLine
            for (int i = 0; i < 100; i++)
            {
                // guardamos en variable miThread el ID del Thread que se está ejecutando
                var miThread = Thread.CurrentThread.ManagedThreadId;

                //Le damos un segundo
                Thread.Sleep(1000);

                //Mostramos en consola el id del thread que se está ejecutando en ese mismo instante.
                Console.WriteLine("Esta vuelta de for corresponde al Hilo o Thread:{0}", miThread);
            }
        }
    }
}

