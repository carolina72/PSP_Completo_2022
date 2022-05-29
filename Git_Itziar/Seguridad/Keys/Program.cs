using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Keys
{
    class Program
    {
        static void Main(string[] args)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(3072);
            byte[] sshrsa_bytes = Encoding.Default.GetBytes("ssh-rsa");
            byte[] n = RSA.ExportParameters(false).Modulus;
            var sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < n.Length; i++)
            {
                sBuilder.Append(n[i].ToString("x2"));
            }
            Console.WriteLine(sBuilder.ToString());
            byte[] e = RSA.ExportParameters(false).Exponent;
            sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < e.Length; i++)
            {
                sBuilder.Append(e[i].ToString("x2"));
            }
            Console.WriteLine(sBuilder.ToString());
            RSAParameters parameters = new RSAParameters();
            parameters.Exponent = e;
            parameters.Modulus = n;


            XmlSerializer serializador = new XmlSerializer(typeof(RSAParameters));
            TextWriter writer = new StreamWriter("publica.key");
            serializador.Serialize(writer, parameters);
            writer.Close();

            TextReader reader = new StreamReader("publica.key");
            RSAParameters leidos = (RSAParameters)serializador.Deserialize(reader);
            bool ok = true;
            for(int i = 0;i<leidos.Modulus.Length;i++)
            {
                if (leidos.Modulus[i] != n[i])
                    ok = false;
            }
            for (int i = 0; i < leidos.Exponent.Length; i++)
            {
                if (leidos.Exponent[i] != e[i])
                    ok = false;
            }
            if (ok)
                Console.WriteLine("Comprobado byte a byte que son iguales");
            RSACryptoServiceProvider tester = new RSACryptoServiceProvider();
            tester.ImportParameters(leidos);
            //var datosNuevos = tester.Encrypt(datosEncriptados, false);
            var datosFirmadosConPublica = tester.Encrypt(sshrsa_bytes, false);
            var datosFirmadosPrivada = RSA.Encrypt(sshrsa_bytes, false);

            var datosLimpios = RSA.Decrypt(datosFirmadosConPublica, false);
            //error no se puede desencriptar con la clave publica, aunque en teoria si se puede para ello estan las funciones de sing y verify
            //var datosLimpios2 = tester.Decrypt(datosFirmadosPrivada, false);

            if (StructuralComparisons.StructuralEqualityComparer.Equals(datosLimpios, sshrsa_bytes))
                Console.WriteLine("Son iguales");
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(datosLimpios));
            var signedData = RSA.SignData(sshrsa_bytes, new SHA1CryptoServiceProvider());
            bool xxxx = tester.VerifyData(sshrsa_bytes, new SHA1CryptoServiceProvider(),signedData);

            Console.WriteLine(xxxx);






        }
    }
}
