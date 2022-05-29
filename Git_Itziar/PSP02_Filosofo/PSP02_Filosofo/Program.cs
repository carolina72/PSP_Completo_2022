using System;
using System.Threading;

namespace Ejercicio2UD2PSP
{
    
    class Program
    {
        const int NUM_FILO = 5;
        const int NUM_PALILLO = 5;
        public static Filosofo[] filo = new Filosofo[NUM_FILO]; //Se crea array de 5 filósofos
        public static object[] palillos = new object[NUM_PALILLO]; //Declara array de 5 objetos correspondiente a 5 palillos.
        static void Main(string[] args)
        {
            const int numFilosofos = 5;
            //Se crean los cinco palillos.
            for (int i = 0; i < numFilosofos; i++)
            {
                palillos[i] = new object();
            }

            //Se asigna un índice y palillo a cada filósofo
            filo[0] = new Filosofo(0, numFilosofos - 1, 0);

            for (int i = 1; i < numFilosofos; i++)
            {
                filo[i] = new Filosofo(i, i - 1, i);
            }

            //Se crean las 5 tareas que llaman al método comer.
            var t1 = new Thread(new ParameterizedThreadStart(filo[0].comer));
            var t2 = new Thread(new ParameterizedThreadStart(filo[1].comer));
            var t3 = new Thread(new ParameterizedThreadStart(filo[2].comer));
            var t4 = new Thread(new ParameterizedThreadStart(filo[3].comer));
            var t5 = new Thread(new ParameterizedThreadStart(filo[4].comer));

            //Arrancan cada una de las tareas.
            t1.Start(filo[0]);
            Thread.Sleep(1000);
            t2.Start(filo[2]);
            t3.Start(filo[3]);
            t4.Start(filo[1]);
            t5.Start(filo[4]);

        }
    }

    class Filosofo
    {
        //Atributos plato, palillos y  número de comidas que lleva.
        int indice;
        int palilloIzq;
        int palilloDrc;
        int comidas;

        // Constructor
        public Filosofo(int indice, int palilloIzq, int palilloDrc)
        {
            this.indice = indice;
            this.palilloIzq = palilloIzq;
            this.palilloDrc = palilloDrc;
            this.comidas = 0;
        }

        //Método comer
        //Gestionará las comidas. 
        //@Program.palillos{indice correspondiente] son 5 objetos, correspondientes a 5 palillos a bloquear.
        //Gestión de bloqueo mediante  Monitor.TryEnter

        public void comer(object param)
        {

            Filosofo filoComer = (Filosofo)param;
            Console.WriteLine("El filosofo {0} debe coger el palillo izquierdo {1} y el derecho {2}", filoComer.indice, filoComer.palilloIzq, filoComer.palilloDrc);
            //Si el filósofo ha comido menos de 5 veces.
            while (filoComer.comidas < 5)
            {
                
                bool lockTaken = false;
                //Intento de coger palillo Izquierdo
                Monitor.TryEnter(Program.palillos[filoComer.palilloIzq], 1000, ref lockTaken);
                //Si el intento es satisfactorio, nos echamos dos segundos intentándolo
                if (lockTaken)
                {
                    try
                    {
                        lockTaken = false;
                        //Muestra que ha cogido el palillo izdo
                        Console.WriteLine("El filosofo {0} coge el palillo izquierdo {1}", filoComer.indice, filoComer.palilloIzq);
                        //Intento de coger palillo Derecho, estamos dos segundos intentándolo
                        Monitor.TryEnter(Program.palillos[filoComer.palilloDrc], 1000, ref lockTaken);
                        //Si consigue coger el palillo derecho
                        if (lockTaken)
                        {
                            try
                            {
                                Console.WriteLine("El filosofo {0} coge el palillo derecho {1}", filoComer.indice, filoComer.palilloDrc);
                                Console.WriteLine("El filosofo {0} está comiendo.", filoComer.indice);
                                //Aumenta el valor de comida en uno
                                filoComer.comidas++;
                                //Esperamos un segundo antes de terminar de comer.
                                Thread.Sleep(1000);
                            }
                            finally
                            {
                                //Una vez finalicemos la comida liberamos el palillo derecho
                                Monitor.Exit(Program.palillos[filoComer.palilloDrc]);
                                Console.WriteLine("El filosofo {0} deja el palillo derecho {1}", filoComer.indice, filoComer.palilloDrc);
                                
                            }
                        }
                        //Si no consigue coger el palillo derecho, se muestra hambriendo
                        else
                        {
                            Console.WriteLine("El filósofo {0} está hambriento.", filoComer.indice);
                        }
                    }
                    //Libera el palillo izquierdo
                    finally
                    {
                        Monitor.Exit(Program.palillos[filoComer.palilloIzq]);
                        Console.WriteLine("El filosofo {0} deja el palillo izquierdo {1}", filoComer.indice, filoComer.palilloIzq);
                        Console.WriteLine("El filósofo {0} deja de comer.", filoComer.indice);
                        //Si el filósofo ha llegado al máximo de comidas se retira a pensar
                        if (filoComer.comidas == 5)
                        {
                            Console.WriteLine("El filósofo {0} comió suficiente se retira a pensar", filoComer.indice);
                        }
                    }
                }
                //El filósofo deja de intentar comer
                else
                {
                    Console.WriteLine("El filósofo {0} deja de intentar comer", filoComer.indice);
                }
            }
        }
    }
}
