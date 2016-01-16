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
    public partial class Message_show : Form
    {
        public Message_show()
        {
            InitializeComponent();
            textBox1.Text = power_APP.Properties.Settings.Default.message_user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Message_show_Load(object sender, EventArgs e)
        {
           
          
        }
    }
}
