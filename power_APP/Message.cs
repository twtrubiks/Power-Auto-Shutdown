using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace power_APP
{
    public partial class Message : Form
    {
        public Message()
        {
            InitializeComponent();
            textBox1.Text = power_APP.Properties.Settings.Default.message_user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            power_APP.Properties.Settings.Default.message_user = textBox1.Text;
            power_APP.Properties.Settings.Default.Save();
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            } 
        }

        private void Message_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.pb2_bool = true;
        }
    }
}
