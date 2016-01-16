using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace power_APP
{
    public partial class Timeup : Form
    {
        public Timeup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string play_file = power_APP.Properties.Settings.Default.clock_user;

            if (play_file.Contains(".mp3"))
            {
                Form1.wplayer.controls.stop();               
            }
            else
            {
                SoundPlayer myMusic = new SoundPlayer(play_file);
                myMusic.Stop();
            }
            this.Close();
        }
    }
}
