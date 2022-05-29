using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace AlquilerBicicletas
{
    class Program
    {
        static void Main(string[] args)
        {
            const int MAX_VALOR_ALMACEN = 100;
            const int MAX_NUM_BICIS = 200;
            BlockingCollection<int> stockBicis = new BlockingCollection<int>(MAX_VALOR_ALMACEN);
            BlockingCollection<int> stockBiciSecundario = new BlockingCollection<int>(MAX_VALOR_ALMACEN);
            int count = 0;
            
            Console.WriteLine("{0} %d\n",count);

            /*Tiene que haber 3 task:
             * 1. Productor: Empresa de Bicicletas
             * 2. Consumidor y productor (alquila y devuelve bicicletas): Zona Gros
             * 3. Consumidor y productor (alquila y devuelve bicicletas): Zona Amara
            */
            
            //Productor, empresa que compra bicicletas.

            Task compraBici = Task.Run(() =>
            {
                int bici = 0;
                bool maxAlmacen = false;
                while (!maxAlmacen)
                {
                    stockBicis.Add(bici);                    
                    Console.WriteLine("La empresa de bicis ha comprado la bicicleta{0} y la tiene en el almacén principal.{1} stock almacen principal", bici, stockBicis.Count);
                    bici++;

                    if (bici == MAX_NUM_BICIS)
                    {
                        maxAlmacen = true;
                    }
                }
                stockBicis.CompleteAdding();
                Console.WriteLine("Cierre de almacén. Nadie podrá depositar bicicletas en dicho almacén. {1} stock almacen principal", bici, stockBicis.Count);
            });

            Task ZonaAmara = Task.Run(() =>
            {
                int bici = -1;
                while (!stockBicis.IsCompleted)
                {
                    try
                    {
                        bici = stockBicis.Take();
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Error en Amara: ha habido problemas para alquilar bici.");
                    }

                    if (bici != -1)
                    {
                        Console.WriteLine("Un usuario en la zona Amara ha alquilado la bicicleta{0}.{1} stock almacen principal", bici, stockBicis.Count);
                    }

                }
                Console.WriteLine("En zona Amara no hay más bicis en el almacén.");
            });

            Task ZonaGros = Task.Run(() =>
            {
                while (!stockBicis.IsCompleted)
                {
                    int bici = -1;

                    try
                    {
                        bici = stockBicis.Take();
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Error en Gros: ha habido problemas para alquilar bici.");
                    }

                    if ((bici != -1)&& (!stockBicis.IsCompleted))
                    {
                        Console.WriteLine("Un usuario en la zona Gros ha alquilado la bicicleta{0}.{1} stock almacen principal", bici, stockBicis.Count);
                    }

                    if ((!stockBicis.IsAddingCompleted))
                    {
                        if (bici % 3 == 0)
                        {
                            stockBicis.Add(bici);
                            Console.WriteLine("Un usuario en la zona Gros ha devuelto la bicicleta{0}.{1} stock almacen principal", bici, stockBicis.Count);
                        }
                    }
                    else
                    {
                        if (bici % 3 == 0)
                        {
                            Console.WriteLine("El almacén principal está completo, se despositarán las bicis en el secundario.Almacen principal {0}, Almacén secundario {1}",stockBicis.Count, stockBicis.Count);
                            stockBiciSecundario.Add(bici);
                            Console.WriteLine("Un usuario en la zona Gros ha devuelto la bicicleta{0} al segundo almacén.Almacén secundario {1}", bici, stockBicis.Count);
                        }
                    }
                }
                Console.WriteLine("En zona Gros no hay más bicis en el almacén.");
            });
            
            compraBici.Wait();
            ZonaGros.Wait();
            ZonaAmara.Wait();
            
            Console.WriteLine("El stock sobrante es {0} bicis.", stockBiciSecundario.Count);
            Console.Read();
        }
    }
}
