/* Programa que muestra el funcionamiento de BlockingCollection
 * BlockingCollection: Es una lista de elementos.
 * Es seguro, ya  que mediante sus métodos se asegura que recoger objetos y agregarlos en esa colección se va a realizar de forma segura. 
 * Realiza una gestión de threads interna y abstrae al programador de su realización.
 * Funcionamiento:
 *  El programa tendrá una cola de 100 números que se podrán consumir y producir simultáneamente.
 *  Los valores que se generarán y consumirán tendrán unos valores comprendidos entre el 0 y el 1000.
 *  La ejecución puede ser multithreading o multihilo.
*/

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;


namespace consumerProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creación de nueva colección en este caso de números enteros.
            // Se inicializa la Colección a 100 elementos. Es decir, no se podrán generar más de 100 ni consumir más de 100 en el mismo instante.
            BlockingCollection<int> dataItems = new BlockingCollection<int>(100);


            // Tarea que realiza función de CONSUMIDOR. Consume los elementos de la colección BlockingCollection
            Task t1 = Task.Run(() =>
            {
                while (!dataItems.IsCompleted) //Analiza si la colección se ha marcado como completada
                {

                    int data = -1;
                    // Se bloquea sí dataItems.Count == 0. Es decir si no hay elemento para consumir se queda a la espera

                    try
                    {
                        //Thread.Sleep(2000); //Consumidor duerme hilo 2 segundos.
                        //Cogemos un elemento de la colección de dataItems.
                        //Si no hay nada para coger, se queda la tarea esperando hasta que haya algún elemento para coger.
                        data = dataItems.Take();

                        
                    }
                    //Recogemos una excepción que no vamos a tratar en este ejercicio. Entraría dentro del catch si hay una operación no válida.
                    catch (InvalidOperationException) { }

                    if (data != -1)
                    {
                        Console.WriteLine("Consumir el elemento {0}", data);
                    }

                }

                //Si no hay nada por consumir, muestra este mensaje por pantalla.
                Console.WriteLine("\r\nNo hay más números por consumir");
            });

            // Tarea que realiza función de PRODUCTOR. Produce los elementos de la colección BlockingCollection.
            // Añade elementos a la colección.
            Task t2 = Task.Run(() =>
            {
                int data = 0;
                bool AnadirElemento = true;

                //Mientras haya un elemento por añadir entra dentro del bucle
                while (AnadirElemento)
                {

                    // Añadimos el dato a la colección.
                    // Si la capacidad ha llegado a 100 que es el tope de la colección se queda esperando a que se vacíe la colección para añadirle un número.
                    // No hay que hacer ninguna comprobación lo hae automático.
                    // No hay que esperar a nada para crearlo.

                    dataItems.Add(data);
                    Console.WriteLine("Anadir el elemento {0}", data);
                    //Si he leido algo, muestro por consola.

                    //Aumentamos el número de datos
                    data++;

                    //Si el número data ha llegado al máximo finalizamos el programa.
                    //ESte número no tiene nada que ver con el 100 con el que hemos creado la colección.
                    //Los 1000 números serán los números a generar para que se guarden dentro de la colección, aquellos que se producen y se consumen.
                    if (data == 1000)
                    {
                        //Cuando se llegue a la cantidad de 1000 números ya no se guardará nada más dentro de la colección.
                        AnadirElemento = false;
                    }

                }
                // La colección no va a aceptar más elementos
                dataItems.CompleteAdding();

            });

            //Se espera a que finalicen las tareas
            //Si ejecutamos de una forma síncrona los objetos necesitamos  un wait. Para que primero se ejecute el t1 y luego el t2.
            t1.Wait();
            t2.Wait();
        }
    }
}