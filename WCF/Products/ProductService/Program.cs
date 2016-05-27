using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ProductService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(WCFProductService)))
            {
                host.Open();
                Console.WriteLine("Server is open.");
                Console.WriteLine("<Press ENTER to close server.");
                Console.ReadLine();
            }
        }
    }
}
