using System;
using System.Diagnostics;

namespace PSP01_Ejercicio1
{
    class Program
    {
        static void Main(string[] args)
        {
            Process process1 = new Process();
            //Process process2 = new Process();
            //Process process3 = new Process();

            //consolaDir(process1);
            //consolaDir2();
            //inteExplorer(process2);
            aplicacionConArg();
            //aplicacionConArg2();
        
        }

        static void consolaDir(Process p1)
        {
            p1.StartInfo.FileName = "CMD";
            p1.StartInfo.Arguments = "/C dir";
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            p1.Start();
            p1.WaitForExit();
        }
        /*static void consolaDir2()
        {
            Process.StartInfo.FileName = "CMD";
            Process.StartInfo.Arguments = "/C dir";
            Process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            Process.Start();
            Process.WaitForExit();
        }*/

        static void inteExplorer(Process p2)
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\" ;

            p2.StartInfo.FileName = "C:/Program Files (x86)/Internet Explorer/iexplore.exe";
            p2.StartInfo.Arguments = "www.google.com";
            p2.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            p2.Start();
            p2.WaitForExit();
        }

        static void aplicacionConArg()
        {
            // url's are not considered documents. They can only be opened
            // by passing them as arguments.
            Process.Start("C:/Program Files (x86)/Internet Explorer/iexplore.exe", "www.birt.eus");
            Process.Start("CMD", "/C dir");
            //Process.WaitForExit();
            // Start a Web page using a browser associated with .html and .asp files.
            //Process.Start("IExplore.exe", "C:\\myPath\\myFile.htm");
            //Process.Start("IExplore.exe", "C:\\myPath\\myFile.asp");
        }

        static void aplicacionConArg2()
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";

            ProcessStartInfo startInfo = new ProcessStartInfo("C:/Program Files (x86)/Internet Explorer/iexplore.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;

            Process.Start(startInfo);

            startInfo.Arguments = "www.birt.eus";

            Process.Start(startInfo);
            
        }
        

    }
}
