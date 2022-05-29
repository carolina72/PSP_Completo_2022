using System;
using System.Threading; //Necesario para utilizar los métodos de la clase thread

namespace tareas
{
    public class Tarea1
    {
        public static void Main(string[] args)
        {
            Task tarea = new Task(TareaUno); //Creando nuestra primera tarea.
            tarea.Start(); //Iniciamos la tarea.

            
            //Creamos dos tareas y la máquina los ejecuta como mejor le convenga. Lo gestiona como le interesa.
            Task tarea2 = new Task(TareaUno); //Comenzamos con un programa concurrente
            tarea2.Start(); //Iniciamos la tarea.
            

            //Otra forma de ejecutar una tarea con expresión lambda

            Task tarea3 = new Task(() =>
            {

                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Esto tarea3 que está dentro main y se está ejecutando con el thread {0}", Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(1000);
                }
                
            });
            tarea3.Start();
            
            //Método Run
            Task tarea4 = Task.Run(() => TareaUno());
            Console.ReadLine();

        }
        private static void TareaUno()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Esto es una tarea y se está ejecutando con el thread {0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            }
            
        }
        
    }
}