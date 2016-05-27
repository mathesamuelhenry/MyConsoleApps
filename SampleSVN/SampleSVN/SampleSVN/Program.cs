using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data;

namespace SampleSVN
{
    class Program
    {
        static void Main(string[] args)
        {

            //Regex regex = new Regex(@"^((?!-)[A-Za-z0-9-]{1,63}(?<!-)\\.)+[A-Za-z]{2,6}$");
            //Regex regex = new Regex(@"^([a-zA-Z0-9]+(\.[a-zA-Z0-9]+)+.*)$");
            //Regex regex = new Regex(@"^([-A-Za-z0-9]+\.)+[A-Za-z]{2,6}$");
            Regex regex = new Regex(@"^([-A-Za-z0-9]+\.)+[A-Za-z]{2,6}$");

            Match match = regex.Match("www.Google.com");
            if (match.Success)
            {
                Console.WriteLine(match.Value);
            }
            else
            {
                Console.WriteLine("failure");
            }

            
            List<string> sam = null;

            if (sam != null && sam.Count > 0)
            {
                Console.WriteLine("gello");
            }
            else
            {
                Console.WriteLine("No");
            }

            DataRow dr = getRow();
            
            /*Console.WriteLine("Checkout...");
            string errorMessage = string.Empty;
            string outputMessage = string.Empty;

            Stopwatch sp1 = new Stopwatch();
            sp1.Start();
            SVN svn = new SVN("mbs_enclarity_svn_user@mbs", "abc123$$", "https://svn.br.seisint.com/batchsetup/trunk/healthcare/client_configuration/dev", "C:\\Enclarity\\ConfigFiles");
            svn.Checkout(out errorMessage, out outputMessage);
            sp1.Stop();
            double checkoutTimeElapsed = sp1.Elapsed.TotalSeconds;
            Console.WriteLine(checkoutTimeElapsed.ToString());*/
        }

        public static DataRow getRow()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Weight", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Breed", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            // Here we add five DataRows.
            table.Rows.Add(57, "Koko", "Shar Pei", DateTime.Now);
            table.Rows.Add(130, "Fido", "Bullmastiff", DateTime.Now);
            table.Rows.Add(92, "Alex", "Anatolian Shepherd Dog", DateTime.Now);
            table.Rows.Add(25, "Charles", "Cavalier King Charles Spaniel", DateTime.Now);
            table.Rows.Add(7, "Candy", "Yorkshire Terrier", DateTime.Now);

            return null;

        }
    }
}
