using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RIAG.MBS.DBAccess;
using RIAG.MBS.Console;
using System.Xml;
using System.Data;
using System.IO;

namespace _2015AssignmentsVS2010
{
    public class MBSConsoleApp : RIAGMBSWSConsoleBase
    {
        /*
         * Add your class variables here
         */

        public MBSConsoleApp(string configFile)
            : base(configFile)
        {
        }

        protected override void PreProcessor()
        {
          
        }

        protected override void Processor()
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

                Line = sReader.ReadLine().ToString().Split(',');
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
            catch (Exception e)
            {
                Console.WriteLine("exit");
            }

            var csv = new StringBuilder();

            var newLine = string.Format("BillingId,ProductId,SubProductId,PrimaryTerritory,SecondaryTerritory,{0}", Environment.NewLine);
            csv.Append(newLine);
            
            if (PRCAccountsTable != null)
            {
                foreach (DataRow drow in PRCAccountsTable.Rows)
                {
                    string vcid = drow["acct_no"].ToString();
                    string product_id = drow["product_id"].ToString();
                    string sub_product_id = drow["sub_product_id"].ToString();
                    string primary_territory = drow["pri_terr"].ToString();
                    string secondary_territory = drow["sec_terr"].ToString();

                    string sql = string.Format("select billing_id from sales_assignment.household_acct where vc_id = '{0}'", vcid);
                    string billing_id = RIAG.MBS.DBAccess.Utils.GetScalar(this.DbConnData, sql);

                    csv.Append(string.Format("{0},{1},{2},{3},{4}{5}", billing_id, product_id, sub_product_id, primary_territory, secondary_territory, Environment.NewLine));
                }
            }

            string filePath = "C:\\Projects\\DotNetConsoleApps\\RAA\\2015RiskAssignsModified.csv";
            //after your loop
            File.WriteAllText(filePath, csv.ToString());
        }

        protected override void PostProcessor()
        {

        }
    }
}
