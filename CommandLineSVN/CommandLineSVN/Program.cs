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

            string dsm = "000100000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000001001000000000000000000";
            Console.WriteLine(dsm.Length);
            
            string Detail2 = dsm.Remove(319 - 1, 1);

        }
    }
}
