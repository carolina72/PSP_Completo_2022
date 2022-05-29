/* Ejercicio 1. 
 * Gestion de stock con un productor, un consumidor y productor-consumidor
 * 
 * El programa deberá gestionar el stock de la siguiente forma:
 * 
 * Tarea PRODUCTOR: 
 * Proveerá de 200 bicicletas etiquetadas de 1 a 200, que se irán cargando al almacen principal
 * Una vez provistas las 200, el almacén principal se marcará como cerrado.
 * 
 * Tarea CONSUMIDOR 1:
 * Simulará la recogida de bicis en la zona de Gros.
 * La tarea terminará cuando el almacén principal esté vacío y marcado como completado.
 * 
 * Tarea CONSUMIDOR 2:
 * Simulará la recogida de bicis en la zona de Amara.
 * En aquellos casos en que el número de la bici sea múltiplo de 5, 
 * se realizará la devolución de la bicicleta al almacén principal mientras esté abierto. 
 * En caso de que el almacén principal esté cerrado, la devolución se hará al almacén seundario
 * La tarea terminará cuando el almacén principal esté vacío y marcado como completado.
 * 
 * En el programa existirán dos almacenes:
 * Almacén Principal: 
 *          - regoge las bicis del proveedor,
 *          - tamaño máximo de 100 bicicletas,
 *          - cuando el proveedor complete la entrega de 200 bicis, se marcará como cerrado:
 *                    - el proveedor no podrá entregar mas bicis,
 *                    - las tareas consumidores no podrán devolver más bicis,
 *                    - las tareas consumidores podrán seguir retirando bicis hasta que quede vacío.
 * Almacén Secundario:
 *          - recoge las bicis que devuelva el consumidor cuando el almacén principal este cerrado,
 *          - tamaño máximo de 100 bicicletas,
 *          - no se consumirán bicis de este almacén,
 *          el proveedor no añade bicis a este almacén.
 *          
 * Todas las acciones realizadas por las tareas tanto de productor como de consumidor deberán 
 * aparecer por consola en modo LOG.
 *    - nº de bicis que se van creando y añadiendo al almacén principal,
 *    Devolución de bici con nº de bici y al almacén al que se devuelve,
 *    Cierre del almacén con el nombre del almacén,
 *    Alquiler de bici, nombre de la zona en la que se consume y número de bici consumida,
 *    Stock de almacén que queda en el almacén secundario al finalizar todas las tareas,
 *    Datos relevantes para la ejecución del programa.
 *    
 *  Alumna: Natalia carolina Mendez Chapoval
 *  Ciclo: DAM
 *  Módulo: PSP_Ud02
 */

using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Concurrent;

namespace PSP02_TE01
{
    class ejercicio01
    {
        public static void Main(string[] args)
        {
            // Crea la coleccion de almacenes con una capacidad máxima de 100 bicis cada uno
            BlockingCollection<int> almacenPrincipal = new BlockingCollection<int>(100);
            BlockingCollection<int> almacenSecundario = new BlockingCollection<int>(100);

            // Tarea Productor: agrega 1 bici a almacenPrincipal hasta que bici = 200
            Task Productor = Task.Run(() =>
            {
                int bici = 1;
                bool AñadirBici = true;

                while (AñadirBici)
                {
                    if (!almacenPrincipal.IsCompleted)
                    {
                        almacenPrincipal.Add(bici);
                        {
                            Console.WriteLine("El proveedor ha entregado la bici número {0} al almacén principal", bici);
                            bici++;
                        }
                    }
                    if (bici == 201) AñadirBici = false;
                }
                // AlmacenPrincipal se cierra al haber recibido las 200 bicis
                almacenPrincipal.CompleteAdding();
                Console.WriteLine("Se cierra el Almacén Principal");
            });

            // Tarea Consumidor de Gros 
            Task Consumidor_Gros = Task.Run(() =>
            {
                while (!almacenPrincipal.IsCompleted)
                {
                    int bici = -1;

                    try
                    {
                        bici = almacenPrincipal.Take();
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Error: no se ha podido acceder al almacén.");
                    }
                    if (bici != -1)
                    {
                        Console.WriteLine("Un usuario en la zona de Gros alquila la bici {0} del  almacén principal", bici);
                    }
                }
            });

            // Tarea Consumidor de Amara 
            Task Consumidor_Amara = Task.Run(() =>
            {
                while (!almacenPrincipal.IsCompleted)
                {
                    int bici = -1;

                    try
                    {
                        bici = almacenPrincipal.Take();
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Error: no se ha podido acceder al almacén.");
                    }
                    if (bici != -1)
                    {
                        Console.WriteLine("Un usuario en la zona de Amara alquila la bici {0} del  almacén principal", bici);
                    }

                    // Gestion de la devolución cuando el numero de la bici es múltiplo de 5
                    if (bici % 5 == 0)
                    {
                        // se devuelve al almacen principal
                        if (!almacenPrincipal.IsAddingCompleted)
                        {
                            if (!almacenPrincipal.IsCompleted)
                            {
                                if (almacenPrincipal.TryAdd(bici, TimeSpan.FromSeconds(1)))
                                {
                                    Console.WriteLine("Un usuario en la zona de Amara devuelve la bici {0} al almacén principal", bici);
                                }
                            }
                        }
                        // se devuelve al almacen secundario por estar cerrado el almacén principal
                        else
                        {
                            almacenSecundario.Add(bici);
                            Console.WriteLine("Un usuario en la zona de Amara devuelve la bici {0} al almacén secundario, el principal está cerrado", bici);
                        }
                    }
                }

            });
            Productor.Wait();
            Consumidor_Gros.Wait();
            Consumidor_Amara.Wait();
            Console.WriteLine("Quedan {0} bicicletas en el almacén secundario", almacenSecundario.Count);
        }

    }

}
