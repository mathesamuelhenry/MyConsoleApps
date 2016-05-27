using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections;

namespace LearningTHISFunctionality
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<string> StringList = new List<string>();
            StringList.Add("Hellodude");
            StringList.Add("SamHello");
            FixedString fs = new FixedString();
            fs.Add("Hellodude");
            fs.Add("SamHello");

            fs.CleanString(1, 3);
        }
    }

    public class FixedString : Collection<string>
    {
        public void CleanString(int startIndex, int count)
        {
            List<string> testStr = new List<string>();
            foreach (string text in this)
            {
                testStr.Add(text.Remove(startIndex, count));
            }

            foreach (string text in testStr)
            {
                Console.WriteLine(text);
            }
        }
    }
}
