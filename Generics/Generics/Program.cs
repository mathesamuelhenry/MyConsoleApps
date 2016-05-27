using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    public class Print<pType>
    {
        pType _value;

        public Print(pType p_value)
        {
            this._value = p_value;
        }

        public void write()
        {
            Console.WriteLine(this._value);
        }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            Print<int> p1 = new Print<int>(20);
            p1.write();

            Print<string> p2 = new Print<string>("hello");
            p2.write();

            string Detail2 = string.Empty;
            Detail2 = "0000000";

            Detail2 = Detail2.Remove(5, 1);
            Detail2 = Detail2.Insert(5, "1");

            Console.WriteLine(Detail2);

        }
    }
}
