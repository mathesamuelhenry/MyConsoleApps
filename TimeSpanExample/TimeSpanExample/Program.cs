using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSpanExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(TryParseOrDefault<int>("10"));
            Console.WriteLine(TryParseOrDefault<int>(""));
            Console.WriteLine(TryParseOrDefault<string>("hello"));
            Console.WriteLine(TryParseOrDefault<string>(""));
            Console.WriteLine(TryParseOrDefault<string[]>(""));



            Console.ReadLine();
        }

        public static T TryParseOrDefault<T>(string value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                return (T)converter.ConvertFromString(value);
            }
            catch
            {
                return (T)Activator.CreateInstance(typeof(T));
            }
        }

    }
}
