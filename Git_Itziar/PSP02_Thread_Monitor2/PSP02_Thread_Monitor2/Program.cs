
using System;
using System.Collections.Generic;
using System.Threading;

namespace monitores
{
    public sealed class ProducerConsumer //sealed, no permite la herencia.
    {
        const int MagicNumber = 5;      //Número de veces que van a pasar la pelota de un método a otro. 
        private Object m_lock = new Object();                       // Lock método object necesario para el lock y para trabajar con monitor.
        private Queue<int> m_queue = new Queue<int>(); //Cola donde vamos a guardar los números enteros mientras se pasen el dato de un hilo a otro.


        // Constructor
       public ProducerConsumer()
        {
        }

        // Método Productor
        public void Producer()
        {
            int counter = 0;

            //Sólo permite el acceso de un thread a esta parte del código
            Monitor.Enter(m_lock);
            try
            {
                while (counter < MagicNumber)
                {

                    //Dejamos el thread en Wait, no se pondrá activo hasta que reciba un pulse de algún otro método. Señal de activación
                    Monitor.Wait(m_lock);
                    //Dormimos el thread medio segundo
                    Thread.Sleep(1000);
                    //Cuando se activa el thread del método productor, escribimos por consola el contador
                    Console.WriteLine("producer {0} y Soy: {1}.", counter, Thread.CurrentThread.Name);
                    //Metemos en la cola el contador
                    m_queue.Enqueue(counter);
                    //Despertamos otro thread que se haya quedado a la espera
                    Monitor.Pulse(m_lock);

                    counter++;
                }
            }
            finally
            {
                // Ensure that the lock is released.
                Monitor.Exit(m_lock);
            }


        }

        //Método Consumidor
        public void Consumer()
        {
            //Sólo permite el acceso de un thread a esta parte del código
                Monitor.Enter(m_lock);
            
                //Libera el thread del wait que esté a la espera, es decir, dejamos que el productor produzca un número y lo encole.

                //El thread de este método se queda en wait , hasta que lo libere el productor. Es decir, cuando el productor haga pulse entrará dentro del while.
                try
                {
                    Monitor.Pulse(m_lock);
                    while (Monitor.Wait(m_lock))
                    {

                        //Thread.Sleep(2000);
                        int data = m_queue.Dequeue();
                        //Muestra el dato por consola
                        Console.WriteLine("consumer {0} y Soy: {1}.", data, Thread.CurrentThread.Name);
                        //Hace un pulse para dejar al productor que vuelva a producir otro número
                        Monitor.Pulse(m_lock);
                        if (data == MagicNumber - 1)          // Fin del trabajo no queda nada por consumir y se sale del programa
                            break;


                    }
                }
                finally
                {
                    // Ensure that the lock is released.
                    Monitor.Exit(m_lock);
                }

            
        }

        class Program
        {
            static void Main(string[] args)
            {
                ProducerConsumer app = new ProducerConsumer();

                // Create 2 threads 
                Thread t_producer = new Thread(new ThreadStart(app.Producer));
                Thread t_consumer = new Thread(new ThreadStart(app.Consumer));
                // Thread t_consumer2 = new Thread(new ThreadStart(app.Consumer));

                t_producer.Name = "Hilo Productor 1";
                t_consumer.Name = "Hilo Consumidor 1";
                //  t_consumer2.Name = "Hilo Consumidor 2";

                // Start threads 
                t_producer.Start();
                Thread.Sleep(500);
                t_consumer.Start();
                // t_consumer2.Start();

                // Waith for the threads to complete 
                t_producer.Join();
                t_consumer.Join();
                //  t_consumer2.Join();

                Console.WriteLine("\nPress any key to complete the program.\n");
                Console.ReadKey(false);
            }
        }
    }
}