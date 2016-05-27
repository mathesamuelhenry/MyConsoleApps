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

namespace SampleForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void multiply_Click(object sender, EventArgs e)
        {
            int no1 = 0;
            int no2 = 0;
            
            for (int i = 1; i < 10000; i++)
            { 
                if (string.IsNullOrEmpty(number1.Text) ||
                    string.IsNullOrEmpty(number2.Text))
                {
                    displayCalculate.Text = "Numbers are empty";
                }
                else if (!Int32.TryParse(number1.Text, out no1) ||
                    !Int32.TryParse(number2.Text, out no2))
                {
                    displayCalculate.Text = "Invalid values passed";
                }
                else if (!string.IsNullOrEmpty(number1.Text) && !string.IsNullOrEmpty(number2.Text))
                {
                    int res = Int32.Parse(number1.Text) * Int32.Parse(number2.Text);
                    result.Text = res.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            number1.Text = string.Empty;
            number2.Text = string.Empty;
            result.Text = string.Empty;
            displayCalculate.Text = "Reset fields...";

            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
