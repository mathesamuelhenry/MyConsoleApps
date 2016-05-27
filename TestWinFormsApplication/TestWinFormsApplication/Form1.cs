using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;

namespace TestWinFormsApplication
{
    public partial class Form1 : Form
    {
        public string connString = string.Empty;
        OpenFileDialog ofd = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent();

            connString = "DataSource=localhost;Port=3306;UID=root;PWD=;Database=sample;";

            this.LoadTable();
        }

        private void Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadTable()
        {
            //string connString = "DataSource=dbdprsql-bct.risk.regn.net;Port=3306;UID=mbs_ws;PWD=mbs_ws123;Database=sales_assignment;";

            MySqlConnection mySqlConn = new MySqlConnection(connString);
            MySqlCommand cmdDataBase = new MySqlCommand("select description as Description, year as Year, ticket as 'Ticket No.', user_added as 'User Added', date_added as 'Date Added' from project where status = 1;", mySqlConn);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                System.Data.DataTable dt = new System.Data.DataTable();
                sda.Fill(dt);

                BindingSource bs = new BindingSource();

                bs.DataSource = dt;
                dataGridView1.DataSource = bs;
                dataGridView1.Visible = true;
                
                /*foreach (DataRow dRow in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dRow["map_id"].ToString()))
                    {
                        int n = dataGridView2.Rows.Add();
                        dataGridView2.Rows[n].Cells[0].Value = false;
                        dataGridView2.Rows[n].Cells[1].Value = dRow["map_id"].ToString();
                        dataGridView2.Rows[n].Cells[2].Value = dRow["application"].ToString();
                        dataGridView2.Rows[n].Cells[3].Value = dRow["name"].ToString();
                        dataGridView2.Rows[n].Cells[4].Value = dRow["data_type"].ToString();
                        dataGridView2.Rows[n].Cells[5].Value = dRow["category"].ToString();
                        dataGridView2.Rows[n].Cells[6].Value = dRow["subcategory"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("hello");
                    }
                }*/

            }
            finally
            {
                if (mySqlConn != null)
                    mySqlConn.Close();
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            MySqlConnection mySqlConn = null;

            try
            {
                int count = 0;
                mySqlConn = new MySqlConnection(connString);
                mySqlConn.Open();

                string description = Description.Text;
                string year = YearComboBox.Text;
                string ticket = Ticket.Text;

                count = this.Insert(description, year, ticket);

                if (count > 0)
                    MessageBox.Show("Record added");
                else
                    MessageBox.Show("Record not added");

                this.processing.Text = "Processing...";
                this.LoadTable();
                this.processing.Text = string.Empty;

            }
            finally
            {
                if (mySqlConn != null)
                    mySqlConn.Close();
            }
        }

        private int Insert(string description, string year, string ticket)
        {
            int count = 0;
            MySqlConnection mySqlConn = null;

            try
            {
                mySqlConn = new MySqlConnection(connString);
                mySqlConn.Open();
                string sql = string.Format(@"INSERT INTO project (description, year, ticket, date_added, user_added) 
                                         VALUES ('{0}', '{1}', '{2}', now(), 'SamCorpX')", description, year, ticket);

                MySqlCommand cmd = new MySqlCommand(sql, mySqlConn);
                count = cmd.ExecuteNonQuery();
            }
            finally
            {
                if (mySqlConn != null)
                    mySqlConn.Close();
            }

            return count;
        }

        private void RefeshButton_Click(object sender, EventArgs e)
        {
            this.processing.Text = "Processing...";
            this.LoadTable();
            this.processing.Text = string.Empty;
        }

        private void ProjectName_AcceptsTabChanged(object sender, EventArgs e)
        {
            MessageBox.Show("dfsdf");
        }

        

        private void OnDescriptionDefocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Description.Text))
                MessageBox.Show("Description must not be empty.", "Valid Description");

        }

        private void OnTicketDeFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Ticket.Text))
                MessageBox.Show("Ticket must not be empty.", "Valid ticket");

        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
                OpenTextBox.Text = ofd.FileName;


        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            this.processing.Text = "Processing...";

            if (string.IsNullOrEmpty(OpenTextBox.Text))
                MessageBox.Show("nothing to submit", "dsfdfs", MessageBoxButtons.OK);
            else
            {
                if (!string.IsNullOrEmpty(OpenTextBox.Text))
                {
                    if (File.Exists(OpenTextBox.Text))
                    {
                        FileAttributes fa = File.GetAttributes(OpenTextBox.Text);
                        //MessageBox.Show(Path.GetExtension(OpenTextBox.Text).Replace(".", ""));

                        if (Path.GetExtension(OpenTextBox.Text).Replace(".", "").ToLower() == "csv")
                        {
                            System.Data.DataTable DocumentData = new System.Data.DataTable();
                            DataRow row;

                            using (TextFieldParser reader = new TextFieldParser(string.Concat(OpenTextBox.Text)))
                            {
                                reader.TextFieldType = FieldType.Delimited;
                                reader.SetDelimiters(",");
                                foreach (string item in reader.ReadFields())
                                {
                                    DocumentData.Columns.Add(item.ToString().ToLower());
                                }

                                while (!reader.EndOfData)
                                {
                                    try
                                    {
                                        row = DocumentData.NewRow();
                                        row.ItemArray = reader.ReadFields();
                                        DocumentData.Rows.Add(row);
                                    }
                                    catch (Exception ae)
                                    {
                                        throw new Exception(ae.Message);
                                    }
                                }
                            }

                            int recCount = 0;

                            if (DocumentData.Rows.Count > 0)
                            {
                                foreach(DataRow dRow in DocumentData.Rows)
                                {
                                    recCount += this.Insert(dRow["summary"].ToString(),
                                        "2016",
                                        dRow["key"].ToString());
                                }
                            }

                            if (recCount > 0)
                                MessageBox.Show(string.Format("{0} records added.", recCount));
                            else
                                MessageBox.Show("No records added");
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Not CSV. its {0}", Path.GetExtension(OpenTextBox.Text).Replace(".", "")));
                        }
                    }
                }
            }

            this.LoadTable();
            this.processing.Text = string.Empty;
        }

        private void SwitchButton_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            //TestForm test = new TestForm();
            //test.ShowDialog();
            Microsoft.Office.Interop.Excel.Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            /*xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "http://csharp.net-informations.com";
            xlWorkSheet.Cells[1, 2] = "samuel";
            xlWorkSheet.Cells[1, 2].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

            xlWorkBook.SaveAs("csharp-Excel.xls", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);*/

            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            MySqlConnection mySqlConn = new MySqlConnection(connString);
            MySqlCommand cmdDataBase = new MySqlCommand("select description as Description, year as Year, ticket as 'Ticket No.', user_added as 'User Added', date_added as 'Date Added' from project where status = 1;", mySqlConn);
            xlWorkSheet.Cells.WrapText = true;

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                System.Data.DataTable dt = new System.Data.DataTable();
                sda.Fill(dt);

                int rowCount = 0;
                foreach (DataRow dRow in dt.Rows)
                {
                    rowCount++;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (rowCount == 1)
                        {
                            xlWorkSheet.Cells[rowCount, i+1] = dt.Columns[i].ColumnName;
                            xlWorkSheet.Cells[rowCount, i+1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkOrange);
                        }
                        else
                        {
                            xlWorkSheet.Cells[rowCount, i+1] = dRow[i].ToString();
                        }
                    }
                }


                xlWorkBook.SaveAs("csharp-Excel.xls", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            }
            finally
            {

            }
            MessageBox.Show("Excel file created , you can find the file c:\\csharp-Excel.xls");
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
