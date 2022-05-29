using System;
using System.Collections.Generic;
using System.Linq;

namespace primerosPasos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World2!");
            MiClase[] ejemplo = new MiClase[3];
            ejemplo[0] = new MiClase();
            ejemplo[1] = new MiClase();
            ejemplo[2] = new MiClase();

            int[] enteros = new int[5];
            for (int i = 0; i < enteros.Length; i++)
                enteros[i] = 4;

            Console.WriteLine("Numero de elementos: {0}", ejemplo.Length);

            /*WHERE*/
                /*ENTEROS*/

            //Equivaldría a guardar mediante un for en una variable aquellos elementos que sean igual que 5.
            var resultado2 = enteros.Where(elemento => elemento == 5);
            //Muestra por pantalla el número de elementos que cumplen la condición anterior.
            Console.WriteLine("Enteros iguales a 5: {0}", resultado2.Count());
            

            modifica1(enteros);
            resultado2 = enteros.Where(elemento => elemento == 5);
            Console.WriteLine("Enteros iguales a 5: {0}", resultado2.Count());

            /* WHERE*/
                /*con OBJETOS*/

            //asignamos el valor 3 a la segunda variable del elemento 2 de ejemplo. 
            ejemplo[2].Variable2 = 3;

            
            var resultado = ejemplo.Where(elemento => elemento.Variable2 == 3);
            // Crea un nuevo array que se llama resultado del mismo tipo que ejemplo donde sólo guarda aquellos elementos qrruhue coincida Variable2 == 3.
            Console.WriteLine("Numero de elementos con variable2 igual a 3: {0}", resultado.Count());

           
            modifica1(ejemplo); 


            resultado = ejemplo.Where(elemento => elemento.Variable2 == 3);
            Console.WriteLine("Numero de elementos con variable2 igual a 3: {0}", resultado.Count());

            

            /*PASE DE PARÁMETRO POR VALOR O REFERENCIA*/

            int tmp = 5;
            modifica1(tmp);
            Console.WriteLine("5+2={0}", tmp);

            modifica1(ref tmp);
            Console.WriteLine("5+2={0}", tmp);

            int parametroSalida1, parametrosSalida2;
            //la funcion devuelve dos resultados, misma idea que los parametros out de plsql
            modifica1(out parametroSalida1,out parametrosSalida2, 4, 3);
            Console.WriteLine("parametroSalida1:{0} y parametrosSalida2:{1} ", parametroSalida1, parametrosSalida2);


            /* FOREACH */


            //Uso de foreach con caracteres
            string nombre = "EjemploBirtLH";

            foreach (char letra in nombre) // por cada caracter introduce un espacio por delante y por detrás
            {
                Console.Write(" " + letra + " ");
            }

            //Ejemplo de foreach con array 
            List<MiClase> ejemplo2 = new List<MiClase>();

            foreach (MiClase miClase in ejemplo) //por cada objeto miClase que encuentre dentro de ejemplo lo guardará en la variable miClase
            {
                ejemplo2.Add(miClase); //Añade el contenido la variable miClase al array ejemplo2 en cada uno de sus elementos. 
            }
        }
        public static void modifica1(out int uno,out int dos, in int v1, in int v2)
        {
            //v1 = 4; error la variable es de entrada y no se puede modificar
            uno = v1 + 1;
            dos = v2 + 3;
        }
        public static void modifica1(MiClase[] mis)
        {
            for(int i=0;i<mis.Length;i++)
            {
                mis[i].Variable2 = 3;
            }
        }
        public static void modifica1(int[] mis)
        {
            for (int i = 0; i < mis.Length; i++)
            {
                mis[i]=5;
            }
        }
        public static void modifica1(int mio)
        {
           mio = mio+2;
        }
        public static void modifica1(ref int mio)
        {
            mio += 2;
        }
    }
    partial class MiClase
    {
        int variable1;
        public int Variable1 { get => variable1; set => variable1 = value; }
    }
    partial class MiClase
    {
        int variable2;
        public int Variable2 { get => variable2; set => variable2 = value; }
    }
    partial class MiClase: ISuma
    {
        public MiClase()
        {
            Variable1 = 1;
            variable2 = 2;
        }

        public int suma()
        {
            return variable1+variable2;
        }
    }
    interface ISuma
    {
        int Variable1
        {
            get; set;
        }
         int suma();
    }
}
