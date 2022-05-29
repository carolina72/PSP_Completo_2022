using System;
//Using necesario para hacer uso de los algoritmos de Hash
using System.Security.Cryptography;
using System.Text;

namespace Resumen
{


    public class Program
    {
        public static void Main()
        {
            //String con el texto que queremos convertir a Hash
            string source = "Hola Mundo!";

            //Se crea una instancia por defecto para el algoritmo SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {

                //Devuelve en un string el hash creado (hexadecimal) para compararlo posteriormente.
                string hash = ObtenerHash(sha256Hash, source);

                Console.WriteLine($"El hash obtenido con algoritmo SHA256 del texto {source} es: {hash}.");

                Console.WriteLine("Verificando el hash...");

                if (VerificarHash(sha256Hash, source, hash))
                {
                    Console.WriteLine("El hash corresponde.");
                }
                else
                {
                    Console.WriteLine("El hash no corresponde.");
                }
            }
        }

        //El siguiente método nos devuelve un string legible del hash, convertido en hexadecimal.
        private static string ObtenerHash(HashAlgorithm hashAlgorithm, Object input)
        {

            // Parseamos el imput a string. Y lo codificamos a un array de bytes.
            // Aplicamos el algoritmo con la instancia creada y pasada como parámetro + método ComputeHash
            // Se obtiene el hash en un array de bytes.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes((String)input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Recoge cada byte y se queda con los dos últimos elementos del mismo
            // formateándolo en un string
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Devuelve el string en hexadecimal
            return sBuilder.ToString();
        }

        // El siguiente método verifica el hash frente a un texto que nos han pasado
        private static bool VerificarHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            // Obtengo el hash con los datos pasados por parámetro
            var hashOfInput = ObtenerHash(hashAlgorithm, input);

            // Creamos un StringComparer para comparar los datos del hash
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            //Devuelvo el resultado de la comparativa
            return comparer.Compare(hashOfInput, hash) == 0;
        }

    }
}
// El ejemplo muestra el siguiente resultado:
//*********************************************
//El hash obtenido con algoritmo SHA256 del texto Hola Mundo! es: d4962daf2b2f39666bcd8d35df1357c5608b7019791c20812cd9108b830388bc.
//Verificando el hash...
//El hash corresponde.