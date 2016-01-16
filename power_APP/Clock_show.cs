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
    public partial class Clock_show : Form
    {
        int conditon_second ;
        public Clock_show(int show_second)
        {
            InitializeComponent();
            conditon_second = show_second;
            
            this.comboBox1.SelectedIndex = 0;
        }

        private void Clock_show_Load(object sender, EventArgs e)
        {
            label1.Text = "電腦將於" + conditon_second + "秒鐘後" + Form1.clock_show;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1.first = true;

            if (Convert.ToInt32(comboBox1.SelectedIndex) == 0)
            {
                Form1.minute += 1;
                Console.WriteLine(Convert.ToInt32(comboBox1.SelectedIndex));
            }

            if (Convert.ToInt32(comboBox1.SelectedIndex) == 1)
            {
                Form1.minute += 5;
            }

            if (Convert.ToInt32(comboBox1.SelectedIndex) == 2)
            {
                Form1.minute += 10;
            }

            this.Close();
        }

        public event RoutedEvent buttonClickedEvent;
        public delegate void RoutedEvent(object sender, EventArgs e);

        private void button2_Click(object sender, EventArgs e)
        {
            if (buttonClickedEvent != null)
            {
                // 拋出事件，給所有相應者
                buttonClickedEvent(sender, e);
            }
            this.Close();
        }
    }
}
