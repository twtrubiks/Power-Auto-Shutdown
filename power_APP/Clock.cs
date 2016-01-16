using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using WMPLib;


namespace power_APP
{
    public partial class Clock : Form
    {
        public Clock()
        {
            InitializeComponent();
            textBox1.Text = power_APP.Properties.Settings.Default.clock_user;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            //file.Filter = "mp3 files (*.mp3)|*.mp3|wav files (*.wav)|*.wav|All files (*.*)|*.*";
            //file.Filter =  "All Supported Audio | *.mp3; *.wma | MP3s | *.mp3 | WMAs | *.wma";

            file.Filter = "All Supported Audio |*.mp3;*.wav";
            // file.Filter = "Image files|*.bmp;*.jpeg;*.jpg;*.png;*.gif";
           
            /*  if ((file.InitialDirectory == null) || (file.InitialDirectory == string.Empty))
              {
                  //file.InitialDirectory = URL_APP.Properties.Settings.Default.USER;
              }*/

            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = file.FileName; // 選擇的完整路徑    
                // string filename = file.FileName;
                //  play_name = filename.Substring(filename.LastIndexOf("\\") + 1);
                //Console.WriteLine("play_name:" + play_name);

                //System.Diagnostics.Process.Start(Filename);
            }
        }

        bool is_play_wav = false, is_play_mp3 = false;

        SoundPlayer myMusic;
        WMPLib.WindowsMediaPlayer wplayer  = new WMPLib.WindowsMediaPlayer();

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("請先選擇音樂");
            }
            else
            {
           
                if (textBox1.Text.Contains(".mp3"))
                {
                    is_background_play();
                    is_play_mp3 = true;
                    wplayer.URL = textBox1.Text;
                    wplayer.controls.play();
                }
                else
                {
                    is_background_play();
                    is_play_wav = true;
                    myMusic = new SoundPlayer(textBox1.Text);
                    myMusic.Play();
                }
            }
        }


        public void is_background_play()
        {
            if (is_play_mp3)
            {
                wplayer.controls.stop();
            }

            if (is_play_wav)
            {
                myMusic.Stop();
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("請先選擇音樂");
            }
            else
            {
                power_APP.Properties.Settings.Default.clock_user = textBox1.Text;
                power_APP.Properties.Settings.Default.Save();

                if (is_play_mp3)
                {
                    wplayer.controls.stop();
                }

                if (is_play_wav)
                {
                    myMusic.Stop();
                }

                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (is_play_mp3)
            {
                wplayer.controls.stop();
            }

            if (is_play_wav)
            {
                myMusic.Stop();
            }

            this.Close();
        }

        private void Clock_FormClosing(object sender, FormClosingEventArgs e)
        {
           Form1.pb1_bool = true;
        }

        private void Clock_FormClosed(object sender, FormClosedEventArgs e)
        {

        
        }


    }
}
