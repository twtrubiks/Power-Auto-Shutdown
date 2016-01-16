using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace power_APP
{
    public partial class exe : Form
    {
        public exe()
        {
            InitializeComponent();
           textBox1.Text= power_APP.Properties.Settings.Default.exe_user  ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "All files (*.*)|*.*";

            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = file.FileName; // 選擇的完整路徑                  
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("請先選擇需執行程式路徑");
            }
            else
            {
                power_APP.Properties.Settings.Default.exe_user = textBox1.Text;
                power_APP.Properties.Settings.Default.Save();
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exe_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.pb3_bool = true;
        }
    }
}
