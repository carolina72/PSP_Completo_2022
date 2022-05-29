using System;
using System.Threading; // Necesario para utilizar thread.sleep
namespace tareas
{
    public class Tareas1
    {
        public static void Main(string[] args)
        {
            Almacen AlmacenMelocotones = new Almacen(2000);

            Thread[] Fenwick = new Thread[100];
            for (int i = 0; i < 100; i++)
            {
                Thread t = new Thread(AlmacenMelocotones.RetirarProducto);
                t.Name = i.ToString(); ;
                Fenwick[i] = t;
            }
            for (int i = 0; i < 100; i++)
            {
                Fenwick[i].Start();
                Fenwick[i].Join();
            }


        }

        class Almacen
        {
            double Stock { get; set; } // campo de clase para poder acceder a sus valores.
            private Object bloqueaAlmacen = new Object(); //Objeto creado y necesario para el bloqueo
            public Almacen(double Stock) //Creamos el constructor de la clase en la que se establece un stock del almacén.
            {
                this.Stock = Stock;
            }

            public double RetirarProducto(double cantidad) // método con el que el fendwich retirará el producto
            {

                if ((Stock - cantidad) < 0)
                {
                    Console.WriteLine("No puedes retirar esa cantidad, quedan sólo {0} del producto en el almacén. Soy el hilo {1}.", Stock, Thread.CurrentThread.Name);
                    return Stock;
                }
                /*lock (bloqueaAlmacen) {*/
                    if ((Stock - cantidad) >= 0)
                    {
                        Console.WriteLine("Has sacado {0} producto del almacén y quedan {1} en Stock. Soy el hilo {2}.", cantidad, Stock - cantidad, Thread.CurrentThread.Name);
                        Stock = Stock - cantidad;
                    }
                //}
                return Stock;
                
            }
            public void RetirarProducto()
            {
                Console.WriteLine("Sacando producto del almacén. Soy el hilo {0}.",  Thread.CurrentThread.Name);
                RetirarProducto(500);
            }

        }
    }
}