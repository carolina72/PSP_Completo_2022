using System;
using System.Threading; //Necesario para utilizar los métodos de la clase thread

namespace tareas
{
    public class Tarea1
    {
        public static void Main(string[] args)
        {
            
            /*
            //Ejercicio1: Ejecución simultánea de tarea1 y tarea2


            //Creamos tarea1
            Task tarea1 = Task.Run(() => TareaUno());


            //Creamos tarea2    
            Task tarea2 = Task.Run(() => TareaDos());
            */
        
            
           // Ejercicio2: Ejecución simultánea de tarea1 y tarea2


            //Creamos tarea1
            Task tarea1 = Task.Run(() => TareaUno());


            //Creamos tarea2 MAL creado   
            //Task tarea2 = tarea1.ContinueWith(TareaDos);

            //Requiere de objeto
            Task tarea2 = tarea1.ContinueWith(TareaTres);

            


            Console.ReadLine();

        }
        private static void TareaUno()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Método TAREAUNO y se está ejecutando con el thread {0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            }

        }
        
        //Ejercicio2 mal creado
        private static void TareaDos()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Metodo TAREADOS y se está ejecutando con el thread {0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            }

        }
        
        
        
        //Ejercicio2 método bien creado
        private static void TareaTres(Task objeto)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Metodo TAREATRES y se está ejecutando con el thread {0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            }

        }
        

    }
}
