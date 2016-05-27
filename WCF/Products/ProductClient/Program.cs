using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ProductInterfaces;

namespace ProductClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IWCFProductService> channelFactory = new
                ChannelFactory<IWCFProductService>("ProductServiceEndpoint");

            IWCFProductService proxy = channelFactory.CreateChannel();

            Console.WriteLine("Enter 'YES' to get data from server");
            string input = Console.ReadLine();

            if (input == "YES")
            {
                List<string> products = proxy.ListProducts();

                foreach (var p in products)
                {
                    Console.WriteLine(p);
                }
            }

            Console.Write("Enter the Product Number to get the Product Details : ");
            string pNumber = Console.ReadLine();

            ProductData pr = proxy.GetProduct(pNumber);
            
            Console.WriteLine("**************************************************");
            

            Console.WriteLine(pr.Name);
            Console.WriteLine(pr.ProductNumber);
            Console.WriteLine(pr.color);

            Console.ReadLine();
        }
    }
}
