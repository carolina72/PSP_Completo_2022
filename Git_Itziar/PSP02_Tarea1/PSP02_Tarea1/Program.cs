using System;
using System.Threading; //Necesario para utilizar los métodos de la clase thread

namespace tareas
{
    public class Tarea1
    {
        public static void Main(string[] args)
        {
            Thread hilo = new Thread(Tareas);
            hilo.Start();
            Thread hilo2 = new Thread(Tareas2);
            hilo2.Start();
            Console.WriteLine("Hola esta es la tarea del hilo 1");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 1");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 1");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 1");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 1");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 1");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 1");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 1");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 1");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 1");
            Thread.Sleep(1000);
            //Tareas();

        }
        private static void Tareas()
        {
            Console.WriteLine("Hola esta es la tarea del hilo 2");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 2");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 2");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 2");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 2");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 2");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 2");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 2");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 2");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 2");
            Thread.Sleep(1000);
        }
        private static void Tareas2()
        {
            Console.WriteLine("Hola esta es la tarea del hilo 3");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 3");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 3");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 3");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 3");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 3");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 3");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 3");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 3");
            Thread.Sleep(1000);
            Console.WriteLine("Hola esta es la tarea del hilo 3");
            Thread.Sleep(1000);
        }
    }
}
