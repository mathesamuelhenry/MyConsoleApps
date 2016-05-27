using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Seisint.LogNamespace;

namespace CommandLineSVN
{
    class Program
    {
        public static void Main(string[] args)
        {
            /*string data_format = "sdfsdf|sdasda";
            string[] formats = data_format.Split(new char[] { '|' });
            Console.WriteLine(formats);*/
            string product_section_name = string.Empty;
            string[] section = product_section_name.Split(new string[] { "_" }, 2, StringSplitOptions.None);

            if (section[0] == "enclarity" && section.Length > 1)
            {
                Console.WriteLine("enclarity");
            }
            else
            {
                Console.WriteLine("not enclarity");
            }

            //LogClass log = new LogClass();
            //MBSConfig con = new MBSConfig("", log);

            //string errorMessage = string.Empty;
            //string outputMessage = string.Empty;

            //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);

            //SVN svn = new SVN("mathsa01@risk", "blackhawk6#", "https://svn.br.seisint.com/batchsetup/trunk/healthcare/client_configuration", @"C:\Solutions\Enclarity\trialrepositories");
            //svn.Checkout(out errorMessage, out outputMessage);

            //Console.WriteLine(errorMessage);
            //Console.WriteLine(outputMessage);

            ////Process p = new Process();
            ////p.StartInfo.FileName = "C:\\extras\\Accurint billable functions final.csv";
            ////p.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            ////p.Start();

            //Process proc = new Process();
            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.RedirectStandardOutput = true;
            ////startInfo.RedirectStandardInput = true;
            //startInfo.RedirectStandardError = true;
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //startInfo.FileName = "svn.exe";
            //startInfo.CreateNoWindow = true;
            ////startInfo.Arguments = "update C:\\Solutions\\Enclarity\\add_repository\\client_configuration";
            //startInfo.Arguments = "checkout https://svn.br.seisint.com/batchsetup/trunk/healthcare/client_configuration C:\\Solutions\\Enclarity\\trialrepositories --username mathsa01@risk --password blackhawk6#";
            //startInfo.UseShellExecute = false;


            //proc.StartInfo = startInfo;
            //proc.Start();

            //System.IO.StreamReader rr = proc.StandardOutput;
            //System.IO.StreamReader er = proc.StandardError;
            
            //Console.WriteLine(rr.ReadToEnd());
            //Console.WriteLine("ERROR : " + er.ReadToEnd());
            //Console.WriteLine("1" + proc.HasExited.ToString());
            //proc.WaitForExit();
            //Console.WriteLine("2" + proc.HasExited.ToString());
            //proc.Close();
            
            //C:\Solutions\Enclarity\add_repository\client_configuration
        }
    }
}
