using System;
using System.Diagnostics;
using System.Threading;
namespace tareas
{
    public class TareasSecuenciales
    {
        public static void Main(string[] args)
        {

            //Método simple de ejecutar una tarea

            /*
            Task task = new Task(EjecutarSegundaTarea);
            task.Start();
            */

            //Método Run , crea el constructor y arranca la tarea

            /*
            Task task = Task.Run(() => EjecutarSegundaTarea());
            */

            //Ejecución de tareas simultáneas con método Run()
            /*
            Task task = Task.Run(() => EjecutarTareaSecuencial1());
            Task task2 = Task.Run(() => EjecutarTareaSecuencial2());
            */


            //Ejecución secuencial de tareas con método ContinueWith

            Task task = Task.Run(() => EjecutarTareaSecuencial1());
            Task task2 = task.ContinueWith(EjecutarTareaSecuencial2); //para poder ejecutar con tareas consecutivas tiene que informar la tarea 1 a la 2.

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
            for (int i = 0; i < 10; i++)
            {
                // guardamos en variable miThread el ID del Thread que se está ejecutando
                var miThread = Thread.CurrentThread.ManagedThreadId;

                //Le damos un segundo
                Thread.Sleep(1000);

                //Mostramos en consola el id del thread que se está ejecutando en ese mismo instante.
                Console.WriteLine("Esta vuelta de for corresponde al Hilo o Thread:{0}", miThread);
            }
        }
        private static void EjecutarTareaSecuencial1()
        {
            //Creamos una tarea lo más sencilla posible WriteLine
            for (int i = 0; i < 10; i++)
            {
                // guardamos en variable miThread el ID del Thread que se está ejecutando
                var miThread = Thread.CurrentThread.ManagedThreadId;

                //Le damos un segundo
                Thread.Sleep(1000);

                //Mostramos en consola el id del thread que se está ejecutando en ese mismo instante.
                Console.WriteLine("Tarea1 SECUENCIAL FOR corresponde al Hilo o Thread:{0}", miThread);
            }
        }
        private static void EjecutarTareaSecuencial2(Task obj) //parámetro ojo, para que se entere que la tarea1 ha finalizado
        {
            //Creamos una tarea lo más sencilla posible WriteLine
            for (int i = 0; i < 10; i++)
            {
                // guardamos en variable miThread el ID del Thread que se está ejecutando
                var miThread = Thread.CurrentThread.ManagedThreadId;

                //Le damos un segundo
                Thread.Sleep(1000);

                //Mostramos en consola el id del thread que se está ejecutando en ese mismo instante.
                Console.WriteLine("Tarea2 SECUENCIAL FOR corresponde al Hilo o Thread:{0}", miThread);
               
            }
        }
    }
}

