using System;
using System.Diagnostics;
using System.Threading;
namespace tareas
{
    public class MetodosWait
    {
        public static void Main(string[] args)
        {

            //Ejercicio1
            //MetodoLlamadaTodasTareas();


            //Ejercicio2: Ejecutar tareas para que se ejecuten de forma simultánea, con optimización de proceso.
            //Las tareas se entrelazan
            //Segunda ejecución las tareas no se ejecutan en el mismo orden

            
            Task.Run(() =>
            {
                EjecutarTareaWait1();
            });

            Task.Run(() =>
            {
                EjecutarTareaWait2();
            });

            Task.Run(() =>
            {
                EjecutarTareaWait3();
            });

            Console.ReadLine();

            

            //Ejercicio3: Simultanear tareas pero que una de ellas espere a que otra tarea finalice. 
            //Casos en los que queremos aprovechar la ejecución simultánea pero que requiera de alguna condición.
            //Tarea 3 no se ejecuta hasta que la Tarea1 y la Tarea2 no hayan finalizado.
            //Para ello debemos guardar las variables de la tarea1 y tarea2

            /*

            var task1= Task.Run(() =>
            {
                EjecutarTareaWait1();
            });

            var task2 = Task.Run(() =>
            {
                EjecutarTareaWait2();
            });

            //De esta forma la tarea1  y la tarea2 pueden ejecutarse en paralelo.
            //Pero la 3 no se ejecutará hasta que finalicen las anteriores.
            //El método WaitAll necesita la información de las tareas a las que debe esperar.

            Task.WaitAll(task1, task2);

            var task3 = Task.Run(() =>
            {
                EjecutarTareaWait3();
            });


            */


            //Ejercicio4: Simultanear tareas pero que una de ellas espere a que otra tarea finalice. 
            //Casos en los que queremos aprovechar la ejecución simultánea pero que requiera de alguna condición.
            //Tarea 3 no se ejecuta hasta que la Tarea1 O la Tarea2 no hayan finalizado.
            //Para ello debemos guardar las variables de la tarea1 y tarea2

            /*
            var task1 = Task.Run(() =>
            {
                EjecutarTareaWait1();
            });

            var task2 = Task.Run(() =>
            {
                EjecutarTareaWait2();
            });

            //De esta forma la tarea1  y la tarea2 pueden ejecutarse en paralelo.
            //Pero la 3 no se ejecutará hasta que finalice cualquiera de las anteriores. Pero no es condición indispensable que finalicen ambas tareas.
            //El método WaitAny necesita la información de las tareas a las que debe esperar.

            Task.WaitAny(task1, task2);

            var task3 = Task.Run(() =>
            {
                EjecutarTareaWait3();
            });
            */

            //Ejercicio5: Simultanear tareas pero que una de ellas espere a que otra tarea finalice. 
            //Casos en los que queremos aprovechar la ejecución simultánea pero que requiera de alguna condición.
            //No se ejecuta ninguna tarea hasta que una de ellas haya finalizado
            //Para ello debemos guardar las variables de la tarea1 (aquella a la que esperaremos)

            /*
            var task1 = Task.Run(() =>
            {
                EjecutarTareaWait1();
            });

            //De esta forma la tarea1 se ejecutará, y  tarea2 y tarea 3pueden ejecutarse en paralelo, siempre y cuando la primera haya finalizado
            //Pero la 3 no se ejecutará hasta que finalice cualquiera de las anteriores. Pero no es condición indispensable que finalicen ambas tareas.
            //El método Wait necesita información de la tarea, pero sin pasarla como parámetro.

            task1.Wait();


            var task2 = Task.Run(() =>
            {
                EjecutarTareaWait2();
            });

            

            var task3 = Task.Run(() =>
            {
                EjecutarTareaWait3();
            });

            */


            //Ejercicio6: Igual que el ejercicio anterior pero esperamos a la tarea1 y tarea2 para que se ejecute la 3.

            
            var task1 = Task.Run(() =>
            {
                EjecutarTareaWait1();
            });

            //Esperamos la tarea1

            task1.Wait();


            var task2 = Task.Run(() =>
            {
                EjecutarTareaWait2();
            });

            //Esperamos la tarea1

            task2.Wait();

            var task3 = Task.Run(() =>
            {
                EjecutarTareaWait3();
            });

            

            Console.ReadLine();

        }

        private static void MetodoLlamadaTodasTareas()
        {
            //Creamos una tarea lo más sencilla posible WriteLine
            EjecutarTareaWait1();
            EjecutarTareaWait2();
            EjecutarTareaWait3();
        }

        //Creamos 3 métodos a las cuales se les va a llamar desde 3 tareas diferentes.
        //
        private static void EjecutarTareaWait1()
        {
            //Creamos una tarea lo más sencilla posible WriteLine
            for (int i = 0; i < 5; i++)
            {
                // guardamos en variable miThread el ID del Thread que se está ejecutando
                var miThread = Thread.CurrentThread.ManagedThreadId;

                //Le damos un segundo
                Thread.Sleep(1000);

                //Mostramos en consola el id del thread que se está ejecutando en ese mismo instante.
                Console.WriteLine("Esta vuelta de for corresponde a la tarea1:{0}");
            }
        }
        private static void EjecutarTareaWait2()
        {
            //Creamos una tarea lo más sencilla posible WriteLine
            for (int i = 0; i < 5; i++)
            {
                // guardamos en variable miThread el ID del Thread que se está ejecutando
                var miThread = Thread.CurrentThread.ManagedThreadId;

                //Le damos un segundo
                Thread.Sleep(500);

                //Mostramos en consola el id del thread que se está ejecutando en ese mismo instante.
                Console.WriteLine("Esta vuelta de for corresponde a la tarea2:{0}");
            }
        }
        private static void EjecutarTareaWait3()
        {
            //Creamos una tarea lo más sencilla posible WriteLine
            for (int i = 0; i < 5; i++)
            {
                // guardamos en variable miThread el ID del Thread que se está ejecutando
                var miThread = Thread.CurrentThread.ManagedThreadId;

                //Le damos un segundo
                Thread.Sleep(500);

                //Mostramos en consola el id del thread que se está ejecutando en ese mismo instante.
                Console.WriteLine("Esta vuelta de for corresponde a la tarea3:{0}");
            }
        }

    }
}
