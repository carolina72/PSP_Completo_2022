using System;
using System.Threading; // Necesario para utilizar thread.sleep
namespace tareas
{
    public class Tareas1
    {
        public static void Main(string[] args)
        {
            Almacen AlmacenMelocotones = new Almacen(2000); // Hemos creado el almacén de melocotones.

            //Creamos tantos threads como fendwich tengamos en el almacén para cargar diversos camiones.

            Thread[] Fenwick = new Thread[10]; //Creamos 10 hilos que serán los consumidores de ese almacén de melocotones.

            for (int i = 0; i < 10; i++)
            {
                //Thread t = new Thread(AlmacenMelocotones.RetirarProducto(200)); no se puede hacer porque no deja pasar un parámetro OJO! 
                //Para solucionarlo vamos a crear una llamada a constructor pero sin pasarle parámetros.
                //Thread t = new Thread(AlmacenMelocotones.RetirarProducto);

                Thread t = new Thread(AlmacenMelocotones.RetirarProductoMasViajes);
                t.Name = i.ToString();  //Le vamos a dar a cada hilo o fenwick un número.
                Fenwick[i] = t; //En cada iteración, me crea un elemento de tipo fenwick que retira una carga de 2000 melocotones
            }
            for (int i = 0; i < 10; i++)
            {
               // iniciamos los thread
                Fenwick[i].Start();
            }

            Console.ReadLine();

        }

        class Almacen
        {
            double Stock { get; set; } //campo de clase para poder acceder a sus valores

            private Object bloqueaAlmacen = new Object(); // Objeto creado y necesario para el bloqueo
            public Almacen(double Stock) //Creamos el constructor de la clase en la que se establece un stock del almacén.
            {
                this.Stock = Stock;
            }
            public double RetirarProducto(double cantidad) // Creamos un método para cuando la fendwich retire producto.
            {
                if ((Stock - cantidad) < 0)
                {
                    Console.WriteLine("No puedes retirar esa cantidad, quedan sólo {0} del producto en el almacén. Soy el Fenwick {1}.", Stock, Thread.CurrentThread.Name);
                    return Stock;
                }
                lock (bloqueaAlmacen) { }
                    if ((Stock - cantidad) >= 0)
                    {
                        Console.WriteLine("Has sacado {0} producto del almacén y quedan {1} en Stock. Soy el Fenwick {2}. ", cantidad, (Stock - cantidad), Thread.CurrentThread.Name);
                        Stock -= cantidad;
                    }
                    return Stock;
                }
            }
            public void RetirarProducto() // Creamos un método para cuando la fendwich retire dinero
            {
                RetirarProducto(500);

            }
            public void RetirarProductoMasViajes() // Creamos un método para cuando la fendwich retire dinero
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine("Sacando producto de almacén. Soy el Soy el Fenwick {0}. ", Thread.CurrentThread.Name);
                    RetirarProducto(500);
                }

            }
        }
    }
}