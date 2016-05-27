using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace _2015Assignments
{
    class Program
    {
        public static void Main(string[] args)
        {
            DataTable PRCAccountsTable = new DataTable();
            DataRow dRow;
            FileStream sourceFile = null;
            StreamReader sReader = null;
            string[] Line;
            int recordCount = 0;

            Console.WriteLine("Loading {0} file... 2015Assignments");

            try
            {
                sourceFile = new FileStream("C:\\Projects\\DotNetConsoleApps\\RAA\\2015RiskAssigns.csv", FileMode.Open, FileAccess.Read);
                sReader = new StreamReader(sourceFile);

                Line = sReader.ReadLine().ToString().Split('\t');
                foreach (string columnName in Line)
                {
                    PRCAccountsTable.Columns.Add(columnName);
                }

                while (sReader.Peek() != -1)
                {
                    Line = sReader.ReadLine().ToString().Split(',');
                    dRow = PRCAccountsTable.NewRow();

                    dRow.ItemArray = Line;
                    PRCAccountsTable.Rows.Add(dRow);
                    recordCount++;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("exit");
            }

            if (PRCAccountsTable != null)
            {
                foreach (DataRow drow in PRCAccountsTable.Rows)
                {
                    string vcid = drow["acct_no"].ToString();
                    string product_id = drow["product_id"].ToString();
                    string sub_product_id = drow["sub_product_id"].ToString();
                    string primary_territory = drow["pri_terr"].ToString();
                    string secondary_territory = drow["sec_terr"].ToString();


                }
            }
        }
    }
}
