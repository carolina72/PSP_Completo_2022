using System;
using System.Threading; //Necesario para utilizar los métodos de la clase thread

namespace tareas
{
    public class TareaSecuencial
    {
        public static void Main(string[] args)
        {

            //Ejercicio1: Se ejecutan de forma consecutiva porque no hay tareas creadas.
            //Tareas();


            //Ejercicio2: Entrelazamos las tareas.
            //Tareas2();

            //Ejercicio3: La tarea3 se ejecuta sólo cuando la tarea1 y la tarea2 ya han finalizado.
            //Hay que guardar las tareas en alguna variable. WaitAll(Hay que pasarle las tareas a las que va a esperar) . Revisar el método.
            //TareasWaitAll();

            //Ejercicio4: La tarea3 se ejecuta sólo cuando la tarea1 O la tarea2 ya han finalizado.
            //Analizar cómo se construye el método TareasWaitAny().
            //TareasWaitAny();

            //Ejercicio5: La tarea2 y tarea3 no se ejecutan hasta que la tarea1 no finalice. Tarea2 y tarea3 pueden ejecutarse de forma paralela.
            //Analizar cómo se construye el método TareasWait().
            //TareasWait();


            //Ejercicio6: Se ejecutan tarea1, tarea2 y tarea3 de forma secuencial
            //Analizar cómo se construye el método TareasWait2().
            //TareasWait2();
            Console.ReadLine();

        }

        
        private static void Tareas()
        {
            TareaUno();
            TareaDos();
            TareaTres();
        }

        private static void Tareas2()
        {
            Task.Run(() =>
            {
                TareaUno();
            });
            Task.Run(() =>
            {
                TareaDos();
            });
            Task.Run(() =>
            {
                TareaTres();
            });        
            
        }

        //Creo un método que al ejecutar tareas devuelvan una variable
        private static void TareasWaitAll()
        {
            var tarea1 = Task.Run(() =>
            {
                TareaUno();
            });
            var tarea2 = Task.Run(() =>
            {
                TareaDos();
            });

            //La tarea3 comenzará cuando las otras dos han finalizado.
            Task.WaitAll(tarea1, tarea2);
            var tarea3 = Task.Run(() =>
            {
                TareaTres();
            });

        }

        //La tarea3 se ejecutará cuando cualquiera de las tarea1  o tarea2 se ejecuten.
        private static void TareasWaitAny()
        {
            var tarea1 = Task.Run(() =>
            {
                TareaUno();
            });
            var tarea2 = Task.Run(() =>
            {
                TareaDos();
            });

            //La tarea3 comenzará cuando cualquier de las dos anteriores haya finalizado.
            Task.WaitAny(tarea1, tarea2);
            var tarea3 = Task.Run(() =>
            {
                TareaTres();
            });

        }

        //Imprescindible que la tarea1 esté finalizada antes de que cualquier otra tarea se ejecute.
        private static void TareasWait()
        {
            var tarea1 = Task.Run(() =>
            {
                TareaUno();
            });

            //Se espera hasta que finalice la tarea1 y luego comenzarán a ejectutarse la tarea2 y 3 de forma paralela.
            tarea1.Wait();
            var tarea2 = Task.Run(() =>
            {
                TareaDos();
            });

            
            var tarea3 = Task.Run(() =>
            {
                TareaTres();
            });

        }


        //se ejecutarán todas las tareas de forma secuencial.
        private static void TareasWait2()
        {
            var tarea1 = Task.Run(() =>
            {
                TareaUno();
            });

            //Se espera hasta que finalice la tarea1 y luego comenzarán a ejectutarse la tarea2 y 3 de forma paralela.
            tarea1.Wait();
            var tarea2 = Task.Run(() =>
            {
                TareaDos();
            });
            //No comenzará a ejecutarse la tarea3 hasta que esté finalizada la tarea2.
            tarea2.Wait();
            var tarea3 = Task.Run(() =>
            {
                TareaTres();
            });

        }
        //Creamos tres métodos cada uno de ellos con una tarea diferente
        private static void TareaUno()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Método TAREAUNO y se está ejecutando con el thread {0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            }

        }


        private static void TareaDos()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Método TAREADOS y se está ejecutando con el thread {0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(500);
            }

        }
        private static void TareaTres()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Método TAREATRES y se está ejecutando con el thread {0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(500);
            }

        }


    }
}