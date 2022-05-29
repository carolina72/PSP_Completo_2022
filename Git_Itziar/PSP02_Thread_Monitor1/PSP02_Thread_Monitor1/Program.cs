// Con el siguiente programa se analizará lo que se puede hacer con Monitor*/
using System;
using System.Collections.Generic;
using System.Threading;

namespace monitores
{
    public class ClaseMonitor
    {
        static int[] ArrayDeNumeros = { 15, 225, 26, 25, 8547, 625, 12 };
        static Object m_lock = new Object();  //Objeto Object para realizar un lock y pulse y wait     
        static Queue<int> m_queue = new Queue<int>(); // Cola de enteros para encolar números.
        public static void Main(string[] args)
        {

            Thread hilo1 = new Thread(EscriboNum); // Encargado de escribir números en la cola
            Thread hilo2 = new Thread(LeoNum); //Encargado de leer números de la cola y mostrarlos por pantalla.

            hilo1.Name = "hilo1";
            hilo2.Name = "hilo2";

            hilo1.Start();
            hilo2.Start();

           
        }
        //Escribo un número en la cola
        public static void EscriboNum()
        {

            for (int i = 0; i < ArrayDeNumeros.Length; i++)
            {
                lock (m_lock)
                {
                    m_queue.Enqueue(ArrayDeNumeros[i]);  //Añade el elemento al final de la cola
                    Console.WriteLine("Soy el hilo {0} y estoy añadiendo el elemento {1} al final de la cola", Thread.CurrentThread.Name, ArrayDeNumeros[i]);
                    Monitor.Pulse(m_lock);
                }
                Thread.Sleep(2000);
            }



        }

        //Leo  un número en la cola
        public static void LeoNum()
        {
            while (true)
            {
                lock (m_lock) //Parte del código donde sólo queremos que acceda un único thread
                {
                    while (m_queue.Count == 0) //Mientras la cola no tenga números encolados
                    {
                        Monitor.Wait(m_lock);
                    }
                    //Console.WriteLine(m_queue.Dequeue()); //Escribe el número que está en la cola.
                    Console.WriteLine("Soy el hilo {0} y estoy consumiendo el elemento {1} de la cola", Thread.CurrentThread.Name, m_queue.Dequeue());
                }
                Thread.Sleep(1000);
            }

        }

        
    }
}


