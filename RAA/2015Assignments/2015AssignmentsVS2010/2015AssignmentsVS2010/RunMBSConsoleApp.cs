using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2015AssignmentsVS2010
{
    public class RunMBSConsoleApp
    {
        static void Main(string[] args)
        {
            MBSConsoleApp consoleApp = null;
            /*
             * Config file
             * Make sure your command line argument is; "../../Config/ConsoleAppConfig.xml"
             */
            if (args == null || args.Length == 0)
                throw new Exception("No config file set");

            try
            {
                /*
                 * Get DoImport Class
                 */
                consoleApp = new MBSConsoleApp(args[0]);

                consoleApp.Execute();

            }
            catch (Exception ex)
            {
                string errmsg = string.Format("Error running console app: {0}", ex.Message);
                if (consoleApp != null && consoleApp.Log != null)
                    consoleApp.Log.AddEntry(errmsg);
                else
                    System.Console.Out.WriteLine(errmsg);
            }
        }
    }
}
