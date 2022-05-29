using System;
using System.Threading; // Necesario para utilizar thread.sleep
namespace tareas
{
    public class Tareas1
    {
        public static void Main(string[] args)
        {
            /*
            //Ejercicio1: JOIN
            Thread t0 = new Thread(Tarea1_0); //Hemos creado un nuevo hilo
            Thread t1 = new Thread(Tarea1_1); //Hemos creado un nuevo hilo
            Thread t2 = new Thread(Tarea1_2); //Hemos creado un nuevo hilo

           

            t0.Start();

            t0.Join();

            t1.Start();
            t1.Join();

            t2.Start();
            
            t2.Join();

            */
            //Ejercicio2: MARCANDO PRIORIDAD

            Thread t = new Thread(Tarea2_2); //Hemos creado un nuevo hilo
            t.Priority = ThreadPriority.Highest;
            t.Start();
            

            Thread t2 = new Thread(Tarea2_1); //Hemos creado un nuevo hilo
            t2.Priority = ThreadPriority.Lowest;
            t2.Start();
            

            Console.ReadLine();
            
        }


        private static void Tarea1_0()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Hola esto es el thread1");
                Thread.Sleep(500);
            }

        }
        private static void Tarea1_1()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Hola esto es el thread2");
                Thread.Sleep(500);
            }
            
        }
        private static void Tarea1_2()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Hola esto es el thread3");
                Thread.Sleep(500);
            }
        }
        private static void Tarea2_1()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("Hola esto es el thread Prioridad BAJA");
                //Thread.Sleep(500);
            }
            Console.WriteLine("Fin Prioridad BAJA");

        }
        private static void Tarea2_2()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("Hola esto es el Prioridad ALTA");
                //Thread.Sleep(500);
            }
            Console.WriteLine("Fin Prioridad ALTA");
        }



    }
}