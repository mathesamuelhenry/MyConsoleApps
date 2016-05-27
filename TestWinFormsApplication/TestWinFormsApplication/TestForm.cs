using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;

namespace TestWinFormsApplication
{
    public partial class TestForm : Form
    {
        public string connString = string.Empty;

        public TestForm()
        {
            InitializeComponent();

            connString = "DataSource=localhost;Port=3306;UID=root;PWD=;Database=sample;";

            //this.LoadTable();

        }

        private void LoadTable()
        {
            //string connString = "DataSource=dbdprsql-bct.risk.regn.net;Port=3306;UID=mbs_ws;PWD=mbs_ws123;Database=sales_assignment;";
            
            MySqlConnection mySqlConn = new MySqlConnection(connString);
            MySqlCommand cmdDataBase = new MySqlCommand("select map_id, application, name, data_type, category, subcategory from aac_tagmap", mySqlConn);
            
            try{
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dt = new DataTable();
                sda.Fill(dt);

                /*BindingSource bs = new BindingSource();

                bs.DataSource = dt;
                dataGridView2.DataSource = bs;
                dataGridView2.Visible = true;
                */
                foreach (DataRow dRow in dt.Rows)
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
                }
                dataGridView2.Visible = true;
                
            }
            finally{
                if (mySqlConn != null)
                    mySqlConn.Close();
            }

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int sum = 0;
            int percentage = 0;
            int numberCount = 100;

            for (int i = 1; i <= numberCount; i++)
            {
                Thread.Sleep(30);
                sum = 0;
                sum = sum + i;

                percentage = (int)Math.Round((Double)(sum * 100) / numberCount);

                backgroundWorker1.ReportProgress(percentage);

                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    backgroundWorker1.ReportProgress(0);
                    return;
                }
            }

            e.Result = sum;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                label1.Text = "Processing cancelled";
            }
            else if (e.Error != null)
            {
                label1.Text = e.Error.Message;
            }
            else
            {
                label1.Text = "Sum = " + e.Result.ToString();
            }
        }

        private void btnProcessControl_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                label1.Text = "Busy processsing, Please wait";
            }
        }

        private void btnCancelControl_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                label1.Text = "No operation in progress to cancel";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.LoadTable();
            //MessageBox.Show("Hello World");
            
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (dataGridView2.Rows.Count > 0)
            {
                MySqlConnection mySqlConn = null;
                mySqlConn = new MySqlConnection(connString);
                mySqlConn.Open();

                try
                {
                    foreach (DataGridViewRow dRow in dataGridView2.Rows)
                    {
                        if (dRow.Cells[0].Value != null)
                        {
                            if (bool.Parse(dRow.Cells[0].Value.ToString()))
                            {
                                string sql = string.Format("delete from aac_tagmap where map_id = {0}; commit;", dRow.Cells["map_id"].Value.ToString());

                                MySqlCommand cmd = new MySqlCommand(sql, mySqlConn);
                                count += cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                finally
                {
                    if (mySqlConn != null)
                        mySqlConn.Close();
                }
            }

            if (count > 0)
            {
                MessageBox.Show(string.Format("{0} records deleted", count.ToString()), "Delete Status");

                dataGridView2.Rows.Clear();
                dataGridView2.Refresh();

                this.LoadTable();
            }
            else
            {
                MessageBox.Show("No records were deleted");
            }
        }
    }
}
