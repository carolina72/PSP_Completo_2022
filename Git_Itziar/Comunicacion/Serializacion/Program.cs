using System;
using System.Runtime.Serialization;

namespace Serializacion
{
    [Serializable]
    public class Mensaje : ISerializable
    {
        public string Sms { get; set; }
        public string Resumen { get; set;}
        public DateTime Stamp { get; set; }
        public Mensaje() { }
        public Mensaje(string sms,string resumen)
        {
            this.Sms = sms;
            this.Resumen = resumen;
            Stamp = DateTime.Now;
        }
        public Mensaje(SerializationInfo info,StreamingContext context)
        {
            Sms = info.GetString("mensaje");
            Resumen = (string)info.GetValue("resumen", typeof(string));
            Stamp = (DateTime)info.GetValue("fecha", typeof(DateTime));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mensaje", Sms);
            info.AddValue("resumen", Resumen);
            info.AddValue("fecha", Stamp);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
