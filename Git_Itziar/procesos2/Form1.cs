using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace procesos2
{
    public partial class Form1 : Form
    {
        //inicializamos la variable p1 de tipo Process a null.
        private Process p1 = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Botón de explorador
        private void Button1_Click(object sender, EventArgs e)
        {
            
                // se pasa como información el ejecutable que queremos arrancar como proceso.
                // asegurarse de que el lanzamiento del proceso se puede realizar desde CMD(consola), sino habrá que modificar las variables de entorno.
                ProcessStartInfo p = new ProcessStartInfo("chrome.exe");

                //arrancamos el proceso
                Process.Start(p);
            
        }

        //Botón calculadora
        private void Button2_Click(object sender, EventArgs e)
        {
                // se pasa como información el ejecutable que queremos arrancar como proceso.
                // En este caso la calculadora es un recurso de windows y no hará falta modificar las variables de entorno.
                ProcessStartInfo p = new ProcessStartInfo("calc.exe");

                //Inicia el recurso de proceso y guarda la información en la variable tmp.
                var tmp = Process.Start(p);              
        }

        // Lanza el proceso parámetros
        private void Button3_Click(object sender, EventArgs e)
        {
            // se pasa como información el ejecutable que queremos arrancar como proceso.
            // pasar la información con rutas relativas no absolutas (para que el programa pueda funcionar en otros entornos).
            // recoge la información del textBox y se lo pasa como parámetro para que lo muestre en consola.
            ProcessStartInfo p = new ProcessStartInfo(@"..\..\..\parametros\bin\Release\netcoreapp2.1\win-x64\parametros.exe", textBox1.Text);

            //Inicia el recurso de proceso y guarda la información en la variable tmp.
            var tmp = Process.Start(p);
        }

        //Lanza el proceso Pipes
        private void Button4_Click(object sender, EventArgs e)
        {
            // si p1 está inicializado o hemos matado los procesos anteriores y está a null.
            if(p1==null)
            {
                // iniciar pipes la parte cliente como proceso
                // se recoge como valor el path relativo del ejecutable
                ProcessStartInfo info = new ProcessStartInfo(@"..\..\..\pipe\bin\Release\netcoreapp2.1\win-x64\pipe.exe");
                
                //se recoge como valor que queremos abrir el proceso en una ventana
                info.CreateNoWindow = false;
                //se recoge como valor el tipo de ventana que queremos abrir
                info.WindowStyle = ProcessWindowStyle.Normal;
                //se recoge como parámetro que se el ejecutable nada más se genere el proceso.
                info.UseShellExecute = true;

                // se crea el proceso, pasando el valor de info como parámetro y devuelve la información de p1 para poder tratar más adelante el proceso.
                // el valor de p1 pasa por referencia.
                p1 = Process.Start(info);
            }
            
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            //parar pipes
            //si el valor p1 es diferente a null o el proceso ha expierado
            if(p1!=null && !p1.HasExited)
            {
                //se mata el proceso p1 con el método kill.
                //si matamos el proceso hijo, como el del servidor es dependiente del hijo lo mata también.
                p1.Kill();

                //se inicializa nuevamente con el valor null para que se pueda lanzar.
                p1 = null;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
