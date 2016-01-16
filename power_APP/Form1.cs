using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;
using Microsoft.Win32;
using System.Runtime.InteropServices; //to DllImport

namespace power_APP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //最小化觸發動作
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            // Console.WriteLine(power_APP.Properties.Settings.Default.is_every);
            label8.ForeColor = Color.Red;
            label9.ForeColor = Color.Red;
            label28.ForeColor = Color.Red;
            label29.ForeColor = Color.Red;
            label43.ForeColor = Color.Red;
            label42.ForeColor = Color.Red;
            label10.ForeColor =  Color.Green;
            label11.ForeColor = Color.Green;
            label12.ForeColor = Color.Green;
            label25.ForeColor = Color.Green;
            label26.ForeColor = Color.Green;
            label27.ForeColor = Color.Green;
            label39.ForeColor = Color.Green;
            label40.ForeColor = Color.Green;
            label41.ForeColor = Color.Green;
            label1.ForeColor = Color.Blue;
            //groupBox2.ForeColor = Color.DarkRed;
            //groupBox3.ForeColor = Color.DarkRed;
            dateTimePicker1.Value = DateTime.Today;

            //倒數 初始化
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 5;
            comboBox6.SelectedIndex = 0;
            //每天 初始化
            comboBox7.SelectedIndex = 12;
            comboBox8.SelectedIndex = 0;
            comboBox9.SelectedIndex = 0;
            //指定時間 初始化
            comboBox1.SelectedIndex = (Convert.ToInt32(DateTime.Now.ToString("HH")));
            comboBox2.SelectedIndex = (Convert.ToInt32(DateTime.Now.ToString("mm")));
            comboBox3.SelectedIndex = (Convert.ToInt32(DateTime.Now.ToString("ss")));

            groupBox3.Visible = true;

            //每隔初始化
            comboBox10.SelectedIndex = 0;
            comboBox11.SelectedIndex = 5;
            comboBox12.SelectedIndex = 0;
			


            //檢查是否有開機自動啟動功能
            string app_name = "myApp";
            //選告一個字串表示本身應用程式的位置後面加的是參數"-s"     
            RegistryKey aimdir = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (aimdir.GetValue(app_name) != null)
            {
                checkBox2.Checked = true;
            }

            //是否自動執行app且執行timer3
            every_check();
            point_check();
        }

        //關閉螢幕 參數 設定
        public int WM_SYSCOMMAND = 0x0112;
        public int SC_MONITORPOWER = 0xF170; //Using the system pre-defined MSDN constants that can be used by the SendMessage() function .
        [DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);
        //To call a DLL function from C#, you must provide this declaration .

        //關機指令 路徑
        String FileLink = @"C:\WINDOWS\system32\shutdown.exe";
        public static int second, minute, hour;
        public static string clock_show = "";

        //播放mp3
        public static WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

        //指定的時間  
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if (!power_APP.Properties.Settings.Default.is_point_time)
            // {
            //hour
            label23.Text = comboBox1.SelectedItem.ToString();
            //  }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //   if (!power_APP.Properties.Settings.Default.is_point_time)
            //  {
            //minute
            label20.Text = comboBox2.SelectedItem.ToString();
            //   }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  if (!power_APP.Properties.Settings.Default.is_point_time)
            //  {
            //second
            label19.Text = comboBox3.SelectedItem.ToString();
            //  }
        }

        //倒數  
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //hour           
            hour = Convert.ToInt32(comboBox4.SelectedItem.ToString());
            label14.Text = "" + hour;
        }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //minute        
            minute = Convert.ToInt32(comboBox5.SelectedItem.ToString());
            if (minute < 10)
                label15.Visible = true;
            else
                label15.Visible = false;

            label17.Text = "" + minute;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            //second         
            second = Convert.ToInt32(comboBox6.SelectedItem.ToString());
            if (second < 10)
                label16.Visible = true;
            else
                label16.Visible = false;

            label18.Text = "" + second;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            label29.Text = dateTimePicker1.Value.Year.ToString()
                         + " /" + dateTimePicker1.Value.Month.ToString()
                         + " /" + dateTimePicker1.Value.Day.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem.ToString() == "0"
                && comboBox5.SelectedItem.ToString() == "00"
                && comboBox6.SelectedItem.ToString() == "00"
                && radioButton6.Checked)
            {
                MessageBox.Show("沒意義指令");
                Application.Restart();
            }

            Console.Write("執行button被按");

            if (!radioButton5.Checked && !radioButton6.Checked && !radioButton11.Checked && !radioButton12.Checked)
            {
                MessageBox.Show("請選擇時間排程");
            }

            if (radioButton5.Checked || radioButton6.Checked || radioButton11.Checked || radioButton12.Checked)
            {
                Console.Write("『執行』被啟動");
                button2.Enabled = true;
                button1.Enabled = false;
                //鎖定ComboBox 元件
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
                comboBox7.Enabled = false;
                comboBox8.Enabled = false;
                comboBox9.Enabled = false;
                comboBox10.Enabled = false;
                comboBox11.Enabled = false;
                comboBox12.Enabled = false;
                //鎖定dateTimePicker 元件
                dateTimePicker1.Enabled = false;
                groupBox1.Enabled = false;



                //指定的時間 啟動
                if (radioButton5.Checked)
                {
                    radioButton6.Enabled = false;
                    radioButton11.Enabled = false;
                    radioButton12.Enabled = false;
                    Form1.ActiveForm.Text = "自動排程-執行中";
                    Console.Write("timer2指定被啟動");

                    power_APP.Properties.Settings.Default.is_point_time = true;
                    power_APP.Properties.Settings.Default.point_hour = comboBox1.Text;
                    power_APP.Properties.Settings.Default.point_second = comboBox3.Text;
                    power_APP.Properties.Settings.Default.point_minute = comboBox2.Text;
                    power_APP.Properties.Settings.Default.date = dateTimePicker1.Value.Year.ToString()
                      + " /" + dateTimePicker1.Value.Month.ToString()
                      + " /" + dateTimePicker1.Value.Day.ToString();

                    power_APP.Properties.Settings.Default.year = dateTimePicker1.Value.Year.ToString();
                    power_APP.Properties.Settings.Default.month = dateTimePicker1.Value.Month.ToString();
                    power_APP.Properties.Settings.Default.day = dateTimePicker1.Value.Day.ToString();

                    power_APP.Properties.Settings.Default.Save();
                    every_bool_set();
                    timer2.Start();
                }


                //倒數 被啟動
                if (radioButton6.Checked)
                {
                    radioButton5.Enabled = false;
                    radioButton11.Enabled = false;
                    radioButton12.Enabled = false;
                    Form1.ActiveForm.Text = "自動排程-執行中";
                    Console.Write("timer1倒數被啟動");
                    timer1.Start();
                }

                //每天 被啟動
                if (radioButton11.Checked)
                {
                    power_APP.Properties.Settings.Default.is_every = true;
                    power_APP.Properties.Settings.Default.everyday_second = comboBox9.Text;
                    power_APP.Properties.Settings.Default.everyday_minute = comboBox8.Text;
                    power_APP.Properties.Settings.Default.everyday_hour = comboBox7.Text;
                    power_APP.Properties.Settings.Default.Save();

                    button2.Enabled = false;
                    radioButton5.Enabled = false;
                    radioButton6.Enabled = false;
                    radioButton12.Enabled = false;

                    Form1.ActiveForm.Text = "每天自動排程-執行中";
                    Console.Write("timer3每天排程被啟動");
                    every_bool_set();
                    timer3.Start();
                }

                //倒數 被啟動
                if (radioButton12.Checked)
                {
                    radioButton5.Enabled = false;
                    radioButton6.Enabled = false;
                    radioButton11.Enabled = false;
                    Form1.ActiveForm.Text = "自動排程-執行中";
                    Console.Write("timer4 每隔 被啟動");
                    timer4.Start();
                }


                if (timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled)
                {
                    in_commission = true;
                    DialogResult result1 = MessageBox.Show("是否將視窗最小化", "提醒", MessageBoxButtons.YesNo);
                    if (result1.ToString() == "Yes")
                    {
                        this.WindowState = FormWindowState.Minimized;
                    }
                }
            }
        }

        bool in_commission = false;
        public static bool first = true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            in_commission = true;
            Console.Write("倒數  被啟動 , timer1  被啟動  ");

            label18.Text = "" + second;
            label17.Text = "" + minute;
            label14.Text = "" + hour;

            if (second < 10)
                label16.Visible = true;
            else
                label16.Visible = false;

            if (minute < 10)
                label15.Visible = true;
            else
                label15.Visible = false;


            //倒數設定
            if (second == 0)
            {
                second = 60;
                minute--;
            }


            if (second == 60 && hour == 0 && minute == 0)
            { minute = 0; }

            if (hour > 0 && minute < 0)
            { minute = 59; hour--; }

            second--;

            //1分鐘前提醒
            if (hour == 0 && minute < 1 && checkBox1.Checked && first)
            {
                first = false;
                Clock_show cs = new Clock_show(second);
                cs.buttonClickedEvent += form2ButtonClicked;
                cs.Show();
            }


            //倒數關機
            if (minute == 0 && hour == 0 && second == 0
                && radioButton1.Checked && radioButton6.Checked)
            {
                Console.Write("倒數關機");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, "-s -f -t 0 ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                label18.Text = "0";
            }

            //倒數重新開機
            if (minute == 0 && hour == 0 && second == 0
                 && radioButton2.Checked && radioButton6.Checked)
            {
                Console.Write("重新開機");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, "-r  ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                label18.Text = "0";
            }

            //倒數休眠
            if (minute == 0 && hour == 0 && second == 0
                 && radioButton3.Checked && radioButton6.Checked)
            {
                Console.Write("倒數休眠");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, " -h  ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                label18.Text = "0";
            }

            //倒數登出
            if (minute == 0 && hour == 0 && second == 0
                && radioButton4.Checked && radioButton6.Checked)
            {
                Console.Write("倒數登出");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, "-l ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                label18.Text = "0";
            }

            //倒數鬧鐘
            if (minute == 0 && hour == 0 && second == 0
                && radioButton8.Checked && radioButton6.Checked)
            {
                Console.Write("倒數鬧鐘");

                //播放音樂
                string play_file = power_APP.Properties.Settings.Default.clock_user;

                if (play_file.Contains(".mp3"))
                {
                    wplayer.URL = play_file;
                    wplayer.controls.play();
                }
                else
                {
                    SoundPlayer myMusic = new SoundPlayer(play_file);
                    myMusic.Play();
                }

                Timeup TP = new Timeup();
                TP.Show();
                label18.Text = "0";
            }

            //倒數訊息
            if (minute == 0 && hour == 0 && second == 0
                && radioButton9.Checked && radioButton6.Checked)
            {
                Console.Write("倒數訊息");
                Message_show MS = new Message_show();
                MS.Show();
                label18.Text = "0";
            }

            //倒數執行程式
            if (minute == 0 && hour == 0 && second == 0
                && radioButton7.Checked && radioButton6.Checked)
            {
                string exe_pro = power_APP.Properties.Settings.Default.exe_user;
                ProcessStartInfo info = new ProcessStartInfo(exe_pro);
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process exe = Process.Start(info);
                label18.Text = "0";
            }

            //倒數關閉螢幕
            if (minute == 0 && hour == 0 && second == 0
                && radioButton10.Checked && radioButton6.Checked)
            {
                SendMessage(this.Handle.ToInt32(), WM_SYSCOMMAND, SC_MONITORPOWER, 2);//DLL function
                label18.Text = "0";
            }

            if (hour == 0 && minute == 0 && second == 0)
            {
                timer1.Stop();
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                groupBox2.Visible = true;
                groupBox3.Visible = false;
                groupBox4.Visible = false;
            }


            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            dateTimePicker1.Enabled = false;
            comboBox4.Enabled = true;
            comboBox5.Enabled = true;
            comboBox6.Enabled = true;
            comboBox7.Enabled = false;
            comboBox8.Enabled = false;
            comboBox9.Enabled = false;
            comboBox10.Enabled = false;
            comboBox11.Enabled = false;
            comboBox12.Enabled = false;

            hour = Convert.ToInt32(comboBox4.SelectedItem.ToString());
            label14.Text = "" + hour;

            minute = Convert.ToInt32(comboBox5.SelectedItem.ToString());
            if (minute < 10)
                label15.Visible = true;
            else
                label15.Visible = false;

            label17.Text = "" + minute;

            //second         
            second = Convert.ToInt32(comboBox6.SelectedItem.ToString());
            if (second < 10)
                label16.Visible = true;
            else
                label16.Visible = false;

            label18.Text = "" + second;

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                groupBox3.Visible = true;
                groupBox2.Visible = false;
                groupBox4.Visible = false;
            }

            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            comboBox6.Enabled = false;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            comboBox7.Enabled = false;
            comboBox8.Enabled = false;
            comboBox9.Enabled = false;

            comboBox10.Enabled = false;
            comboBox11.Enabled = false;
            comboBox12.Enabled = false;

            dateTimePicker1.Enabled = true;

            label23.Text = comboBox1.SelectedItem.ToString();
            label20.Text = comboBox2.SelectedItem.ToString();
            label19.Text = comboBox3.SelectedItem.ToString();

            label29.Text = dateTimePicker1.Value.Year.ToString()
                            + " /" + dateTimePicker1.Value.Month.ToString()
                            + " /" + dateTimePicker1.Value.Day.ToString();
        }


        //暫停 按鈕
        bool is_change = true, choose_timer = false;

        private void button2_Click(object sender, EventArgs e)
        {
            if (is_change)
            {
                Form1.ActiveForm.Text = "自動排程-暫停中";
                button2.Text = "繼續";
                if (timer2.Enabled)
                {
                    timer2.Enabled = false;
                }

                if (timer1.Enabled)
                {
                    timer1.Enabled = false;
                    choose_timer = true;
                }
                is_change = false;
            }
            else
            {
                Form1.ActiveForm.Text = "自動排程-執行中";
                button2.Text = "暫停";
                if (!timer2.Enabled && !choose_timer)
                {
                    timer2.Enabled = true;
                }

                if (!timer1.Enabled && choose_timer)
                {
                    timer1.Enabled = true;
                }
                is_change = true;
            }
        }

        //離開
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //重設
        private void button4_Click(object sender, EventArgs e)
        {
            power_APP.Properties.Settings.Default.is_every = false;
            power_APP.Properties.Settings.Default.is_point_time = false;
            power_APP.Properties.Settings.Default.Save();
            Application.Restart();
        }

        //關機
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label28.Text = "關機";
                label9.Text = "後關機";
                label42.Text = "關機";
                clock_show = "關機";
            }
        }

        //重新開機
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label28.Text = "重新開機";
                label9.Text = "後重新開機";
                label42.Text = "重新開機";
                clock_show = "重新開機";
            }
        }

        //休眠
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                label28.Text = "休眠";
                label9.Text = "後休眠";
                label42.Text = "休眠";
                clock_show = "休眠";
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                label28.Text = "登出";
                label9.Text = "後登出";
                label42.Text = "登出";
                clock_show = "登出";
            }
        }

        //鬧鐘
        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                label28.Text = "啟動鬧鐘";
                label9.Text = "後啟動鬧鐘";
                label42.Text = "啟動鬧鐘";
                clock_show = "啟動鬧鐘";
            }
        }

        //顯示訊息
        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                label28.Text = "顯示訊息";
                label9.Text = "後顯示訊息";
                label42.Text = "顯示訊息";
                clock_show = "顯示訊息";
            }
        }

        //關閉螢幕
        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
            {
                label28.Text = "關閉螢幕";
                label9.Text = "後關閉螢幕";
                label42.Text = "關閉螢幕";
                clock_show = "關閉螢幕";
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true)
            {
                label28.Text = "執行程式";
                label9.Text = "後執行程式";
                label42.Text = "執行程式";
                clock_show = "執行程式";
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            //讓Form再度顯示，並寫狀態設為Normal
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Console.Write("指定時間  timer2  被啟動    ");
            in_commission = true;

            Console.WriteLine("hour:" + power_APP.Properties.Settings.Default.point_hour);
            Console.WriteLine("minute:" + power_APP.Properties.Settings.Default.point_minute);


            //1分鐘前提醒
            if (Convert.ToInt32(power_APP.Properties.Settings.Default.point_hour) == Convert.ToInt32(DateTime.Now.ToString("HH"))
               && Convert.ToInt32(power_APP.Properties.Settings.Default.point_minute) - 1 == Convert.ToInt32(DateTime.Now.ToString("mm"))
               && Convert.ToInt32(power_APP.Properties.Settings.Default.point_second) == Convert.ToInt32(DateTime.Now.ToString("ss"))
               && checkBox1.Checked && first
               && DateTime.Now.Year.ToString() == power_APP.Properties.Settings.Default.year
               && DateTime.Now.Month.ToString() == power_APP.Properties.Settings.Default.month
               && DateTime.Now.Day.ToString() == power_APP.Properties.Settings.Default.day
                )
            {
                first = false;
                Clock_show cs = new Clock_show(60);
                cs.buttonClickedEvent += form2ButtonClicked;
                cs.Show();
            }


            //指定關機
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.point_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.point_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.point_second
               && radioButton1.Checked && radioButton5.Checked
               && DateTime.Now.Year.ToString() == power_APP.Properties.Settings.Default.year
               && DateTime.Now.Month.ToString() == power_APP.Properties.Settings.Default.month
               && DateTime.Now.Day.ToString() == power_APP.Properties.Settings.Default.day
                )
            {
                Console.Write("指定關機");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, "-s -f -t 0 ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                timer2.Stop();
            }

            //指定重新開機
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.point_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.point_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.point_second
                && radioButton2.Checked && radioButton5.Checked
                && DateTime.Now.Year.ToString() == power_APP.Properties.Settings.Default.year
               && DateTime.Now.Month.ToString() == power_APP.Properties.Settings.Default.month
               && DateTime.Now.Day.ToString() == power_APP.Properties.Settings.Default.day)
            {
                Console.Write("指定重新開機");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, "-r  ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                timer2.Stop();
            }

            //指定休眠
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.point_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.point_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.point_second
                && radioButton3.Checked && radioButton5.Checked
                && DateTime.Now.Year.ToString() == power_APP.Properties.Settings.Default.year
               && DateTime.Now.Month.ToString() == power_APP.Properties.Settings.Default.month
               && DateTime.Now.Day.ToString() == power_APP.Properties.Settings.Default.day)
            {
                Console.Write("指定休眠");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, " -h  ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                timer2.Stop();
            }
            // 指定登出
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.point_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.point_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.point_second
                && radioButton4.Checked && radioButton5.Checked
                && DateTime.Now.Year.ToString() == power_APP.Properties.Settings.Default.year
               && DateTime.Now.Month.ToString() == power_APP.Properties.Settings.Default.month
               && DateTime.Now.Day.ToString() == power_APP.Properties.Settings.Default.day)
            {
                Console.Write("指定登出");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, "-l ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                timer2.Stop();
            }

            // 指定時間啟動鬧鐘
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.point_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.point_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.point_second
                && radioButton8.Checked && radioButton5.Checked
                && DateTime.Now.Year.ToString() == power_APP.Properties.Settings.Default.year
               && DateTime.Now.Month.ToString() == power_APP.Properties.Settings.Default.month
               && DateTime.Now.Day.ToString() == power_APP.Properties.Settings.Default.day)
            {
                //播放音樂
                string play_file = power_APP.Properties.Settings.Default.clock_user;

                if (play_file.Contains(".mp3"))
                {
                    wplayer.URL = play_file;
                    wplayer.controls.play();
                }
                else
                {
                    SoundPlayer myMusic = new SoundPlayer(play_file);
                    myMusic.Play();
                }

                Timeup TP = new Timeup();
                TP.Show();

                timer2.Stop();
            }

            // 指定時間啟動文字訊息
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.point_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.point_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.point_second
                && radioButton9.Checked && radioButton5.Checked
                && DateTime.Now.Year.ToString() == power_APP.Properties.Settings.Default.year
               && DateTime.Now.Month.ToString() == power_APP.Properties.Settings.Default.month
               && DateTime.Now.Day.ToString() == power_APP.Properties.Settings.Default.day)
            {
                Console.Write("倒數訊息");
                Message_show MS = new Message_show();
                MS.Show();
                timer2.Stop();
            }

            // 指定時間啟動程式
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.point_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.point_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.point_second
                && radioButton7.Checked && radioButton5.Checked
                && DateTime.Now.Year.ToString() == power_APP.Properties.Settings.Default.year
               && DateTime.Now.Month.ToString() == power_APP.Properties.Settings.Default.month
               && DateTime.Now.Day.ToString() == power_APP.Properties.Settings.Default.day)
            {
                Console.Write("倒數啟動程式");
                string exe_pro = power_APP.Properties.Settings.Default.exe_user;
                ProcessStartInfo info = new ProcessStartInfo(exe_pro);
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process exe = Process.Start(info);
                timer2.Stop();
            }


            // 指定時間關閉螢幕
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.point_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.point_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.point_second
                && radioButton10.Checked && radioButton5.Checked
                && DateTime.Now.Year.ToString() == power_APP.Properties.Settings.Default.year
               && DateTime.Now.Month.ToString() == power_APP.Properties.Settings.Default.month
               && DateTime.Now.Day.ToString() == power_APP.Properties.Settings.Default.day)
            {
                Console.Write("倒數關閉螢幕");
                SendMessage(this.Handle.ToInt32(), WM_SYSCOMMAND, SC_MONITORPOWER, 2);//DLL function
                timer2.Stop();
            }


            try
            {
                if (show_title)
                {
                    show_title = false;
                    Form1.ActiveForm.Text = "只動時間自動排程-執行中";
                }
            }
            catch
            {

            }
        }

        private void 結束ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static bool pb1_bool = true;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pb1_bool)
            {
                Clock clock = new Clock();
                clock.Show();
                pb1_bool = false;
            }

        }

        public static bool pb2_bool = true;

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (pb2_bool)
            {
                Message message = new Message();
                message.Show();
                pb2_bool = false;
            }
        }

        public static bool pb3_bool = true;

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (pb3_bool)
            {
                exe EXE = new exe();
                EXE.Show();
                pb3_bool = false;
            }
        }

        private void form2ButtonClicked(object sender, EventArgs args)
        {
            button2.PerformClick();
            Console.WriteLine("我被按到了");
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string app_name = "myApp";
            //選告一個字串表示本身應用程式的位置後面加的是參數"-s"
            //若沒有附帶啟動參數的話可以不加
            string R_startPath = Application.ExecutablePath + " -S";
            //開啟登錄檔位置，這個位置是存放啟動應用程式的地方
            RegistryKey aimdir = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

            if (checkBox2.Checked)
            {
                try
                {
                    //若登錄檔已經存在則刪除
                    if (aimdir.GetValue(app_name) != null)
                    {
                        //刪除
                        aimdir.DeleteValue(app_name);
                    }
                    //寫入登錄檔值
                    aimdir.SetValue(app_name, R_startPath);
                    //關閉登錄檔
                    aimdir.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("登錄檔寫入失敗:" + ex.Message);
                }
            }

            else
            {
                if (aimdir.GetValue(app_name) != null)
                {
                    //刪除
                    aimdir.DeleteValue(app_name);
                    aimdir.Close();
                }

            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            //指定hour
            label23.Text = comboBox7.SelectedItem.ToString();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            //指定minute
            label20.Text = comboBox8.SelectedItem.ToString();
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            //指定second
            label19.Text = comboBox9.SelectedItem.ToString();
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton11.Checked)
            {
                groupBox3.Visible = true;
                groupBox2.Visible = false;
                groupBox4.Visible = false;
            }

            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            comboBox6.Enabled = false;

            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;

            comboBox7.Enabled = true;
            comboBox8.Enabled = true;
            comboBox9.Enabled = true;

            comboBox10.Enabled = false;
            comboBox11.Enabled = false;
            comboBox12.Enabled = false;

            dateTimePicker1.Enabled = false;

            label23.Text = comboBox7.SelectedItem.ToString();
            label20.Text = comboBox8.SelectedItem.ToString();
            label19.Text = comboBox9.SelectedItem.ToString();

            label29.Text = "     每天";


        }

        bool show_title = false;

        private void timer3_Tick(object sender, EventArgs e)
        {
            Console.Write("每天排程   timer3   被啟動    ");


            //1分鐘前提醒
            if (Convert.ToInt32(power_APP.Properties.Settings.Default.everyday_hour) == Convert.ToInt32(DateTime.Now.ToString("HH"))
               && Convert.ToInt32(power_APP.Properties.Settings.Default.everyday_minute) - 1 == Convert.ToInt32(DateTime.Now.ToString("mm"))
               && Convert.ToInt32(power_APP.Properties.Settings.Default.everyday_second) == Convert.ToInt32(DateTime.Now.ToString("ss"))
               && checkBox1.Checked && first
                )
            {
                first = false;
                Clock_show cs = new Clock_show(60);
                cs.buttonClickedEvent += form2ButtonClicked;
                cs.Show();
            }


            //每天 關機
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.everyday_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.everyday_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.everyday_second
               && power_APP.Properties.Settings.Default.Shutdown
                )
            {
                Console.Write("指定關機");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, "-s -f -t 0 ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                timer3.Stop();
            }

            //每天 重新開機
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.everyday_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.everyday_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.everyday_second
                && power_APP.Properties.Settings.Default.Reboot
         )
            {
                Console.Write("指定重新開機");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, "-r  ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                timer3.Stop();
            }

            //每天  休眠
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.everyday_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.everyday_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.everyday_second
                && power_APP.Properties.Settings.Default.Sleep
         )
            {
                Console.Write("指定休眠");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, " -h  ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                timer3.Stop();
            }

            // 每天  登出
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.everyday_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.everyday_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.everyday_second
                && power_APP.Properties.Settings.Default.Signout
         )
            {
                Console.Write("指定登出");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, "-l ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                timer3.Stop();
            }

            // 每天  啟動鬧鐘
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.everyday_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.everyday_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.everyday_second
                && power_APP.Properties.Settings.Default.AlarmClock
         )
            {
                //播放音樂
                string play_file = power_APP.Properties.Settings.Default.clock_user;

                if (play_file.Contains(".mp3"))
                {
                    wplayer.URL = play_file;
                    wplayer.controls.play();
                }
                else
                {
                    SoundPlayer myMusic = new SoundPlayer(play_file);
                    myMusic.Play();
                }

                Timeup TP = new Timeup();
                TP.Show();

                timer3.Stop();
            }

            // 每天  啟動文字訊息
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.everyday_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.everyday_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.everyday_second
                && power_APP.Properties.Settings.Default.ShowMessage
         )
            {
                Console.Write("倒數訊息");
                Message_show MS = new Message_show();
                MS.Show();
                timer3.Stop();
            }

            // 每天  啟動程式
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.everyday_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.everyday_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.everyday_second
                && power_APP.Properties.Settings.Default.Perform_tasks
         )
            {
                Console.Write("倒數啟動程式");
                string exe_pro = power_APP.Properties.Settings.Default.exe_user;
                ProcessStartInfo info = new ProcessStartInfo(exe_pro);
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process exe = Process.Start(info);

                timer3.Stop();
            }


            // 每天  關閉螢幕
            if (
                   DateTime.Now.ToString("mm") == power_APP.Properties.Settings.Default.everyday_minute
                && DateTime.Now.ToString("HH") == power_APP.Properties.Settings.Default.everyday_hour
                && DateTime.Now.ToString("ss") == power_APP.Properties.Settings.Default.everyday_second
                && power_APP.Properties.Settings.Default.OffScreen
           )
            {
                Console.Write("倒數關閉螢幕");
                SendMessage(this.Handle.ToInt32(), WM_SYSCOMMAND, SC_MONITORPOWER, 2);//DLL function

                timer3.Stop();
            }

            try
            {
                if (show_title)
                {
                    show_title = false;
                    Form1.ActiveForm.Text = "每天自動排程-執行中";
                }
            }
            catch
            {

            }

        }

        public void every_bool_set()
        {
            if (radioButton1.Checked)
            {
                power_APP.Properties.Settings.Default.Shutdown = true;
            }
            else
            {
                power_APP.Properties.Settings.Default.Shutdown = false;
            }

            if (radioButton2.Checked)
            {
                power_APP.Properties.Settings.Default.Reboot = true;
            }
            else
            {
                power_APP.Properties.Settings.Default.Reboot = false;
            }
            if (radioButton3.Checked)
            {
                power_APP.Properties.Settings.Default.Sleep = true;
            }
            else
            {
                power_APP.Properties.Settings.Default.Sleep = false;
            }
            if (radioButton4.Checked)
            {
                power_APP.Properties.Settings.Default.Signout = true;
            }
            else
            {
                power_APP.Properties.Settings.Default.Signout = false;
            }
            if (radioButton10.Checked)
            {
                power_APP.Properties.Settings.Default.OffScreen = true;
            }
            else
            {
                power_APP.Properties.Settings.Default.OffScreen = false;
            }

            if (radioButton8.Checked)
            {
                power_APP.Properties.Settings.Default.AlarmClock = true;
            }
            else
            {
                power_APP.Properties.Settings.Default.AlarmClock = false;
            }
            if (radioButton9.Checked)
            {
                power_APP.Properties.Settings.Default.ShowMessage = true;
            }
            else
            {
                power_APP.Properties.Settings.Default.ShowMessage = false;
            }
            if (radioButton7.Checked)
            {
                power_APP.Properties.Settings.Default.Perform_tasks = true;
            }
            else
            {
                power_APP.Properties.Settings.Default.Perform_tasks = false;
            }
            power_APP.Properties.Settings.Default.Save();
        }

        public void every_check()
        {
            if (power_APP.Properties.Settings.Default.is_every)
            {
                radioButton11.Checked = true;
                button1.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
                radioButton5.Enabled = false;
                radioButton6.Enabled = false;
                radioButton7.Enabled = false;
                radioButton8.Enabled = false;
                radioButton9.Enabled = false;
                radioButton10.Enabled = false;
                dateTimePicker1.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
                comboBox7.Enabled = false;
                comboBox8.Enabled = false;
                comboBox9.Enabled = false;



                checkBox1.Checked = power_APP.Properties.Settings.Default.Remind;

                if (power_APP.Properties.Settings.Default.Shutdown)
                {
                    radioButton1.Checked = true;
                }

                if (power_APP.Properties.Settings.Default.Reboot)
                {
                    radioButton2.Checked = true;
                }

                if (power_APP.Properties.Settings.Default.Sleep)
                {
                    radioButton3.Checked = true;
                }

                if (power_APP.Properties.Settings.Default.Signout)
                {
                    radioButton4.Checked = true;
                }

                if (power_APP.Properties.Settings.Default.OffScreen)
                {
                    radioButton10.Checked = true;
                }


                if (power_APP.Properties.Settings.Default.AlarmClock)
                {
                    radioButton8.Checked = true;
                }

                if (power_APP.Properties.Settings.Default.ShowMessage)
                {
                    radioButton9.Checked = true;
                }

                if (power_APP.Properties.Settings.Default.Perform_tasks)
                {
                    radioButton7.Checked = true;
                }

                comboBox9.Text = power_APP.Properties.Settings.Default.everyday_second;
                comboBox8.Text = power_APP.Properties.Settings.Default.everyday_minute;
                comboBox7.Text = power_APP.Properties.Settings.Default.everyday_hour;

                in_commission = true;
                show_title = true;

                timer3.Start();
            }

        }

        public void point_check()
        {
            if (power_APP.Properties.Settings.Default.is_point_time)
            {

                radioButton5.Checked = true;
                button1.Enabled = false;
                button2.Enabled = true;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
                radioButton11.Enabled = false;
                radioButton6.Enabled = false;
                radioButton7.Enabled = false;
                radioButton8.Enabled = false;
                radioButton9.Enabled = false;
                radioButton10.Enabled = false;
                dateTimePicker1.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
                comboBox7.Enabled = false;
                comboBox8.Enabled = false;
                comboBox9.Enabled = false;




                checkBox1.Checked = power_APP.Properties.Settings.Default.Remind;

                if (power_APP.Properties.Settings.Default.Shutdown)
                {
                    radioButton1.Checked = true;
                }

                if (power_APP.Properties.Settings.Default.Reboot)
                {
                    radioButton2.Checked = true;
                }

                if (power_APP.Properties.Settings.Default.Sleep)
                {
                    radioButton3.Checked = true;
                }

                if (power_APP.Properties.Settings.Default.Signout)
                {
                    radioButton4.Checked = true;
                }

                if (power_APP.Properties.Settings.Default.OffScreen)
                {
                    radioButton10.Checked = true;
                }


                if (power_APP.Properties.Settings.Default.AlarmClock)
                {
                    radioButton8.Checked = true;
                }

                if (power_APP.Properties.Settings.Default.ShowMessage)
                {
                    radioButton9.Checked = true;
                }

                if (power_APP.Properties.Settings.Default.Perform_tasks)
                {
                    radioButton7.Checked = true;
                }

                label23.Text = power_APP.Properties.Settings.Default.point_hour;
                label20.Text = power_APP.Properties.Settings.Default.point_minute;
                label19.Text = power_APP.Properties.Settings.Default.point_second;
                label29.Text = power_APP.Properties.Settings.Default.date;
                in_commission = true;
                show_title = true;

                timer2.Start();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                power_APP.Properties.Settings.Default.Remind = true;
            }
            else
            {
                power_APP.Properties.Settings.Default.Remind = false;
            }

            power_APP.Properties.Settings.Default.Save();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*if (radioButton5.Checked || radioButton6.Checked)
            {
                power_APP.Properties.Settings.Default.is_every = false;
                power_APP.Properties.Settings.Default.Save();
            }*/
            notifyIcon1.Icon = null;
            notifyIcon1.Visible = false;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && in_commission)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(3);
            }
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked)
            {
                groupBox3.Visible = false;
                groupBox2.Visible = false;
                groupBox4.Visible = true;
            }

            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            comboBox6.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox7.Enabled = false;
            comboBox8.Enabled = false;
            comboBox9.Enabled = false;

            comboBox10.Enabled = true;
            comboBox11.Enabled = true;
            comboBox12.Enabled = true;

            dateTimePicker1.Enabled = false;

            hour = Convert.ToInt32(comboBox10.SelectedItem.ToString());
            label35.Text = "" + hour;

            minute = Convert.ToInt32(comboBox11.SelectedItem.ToString());
            if (minute < 10)
                label38.Visible = true;
            else
                label38.Visible = false;
            label34.Text = "" + minute;


            //second         
            second = Convert.ToInt32(comboBox12.SelectedItem.ToString());
            if (second < 10)
                label45.Visible = true;
            else
                label45.Visible = false;

            label33.Text = "" + second;		
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        int interval_hour, interval_minute, interval_second;

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            hour = Convert.ToInt32(comboBox10.SelectedItem.ToString());
            interval_hour = hour;
            label35.Text = "" + hour;
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
           // label38.Text = comboBox11.SelectedItem.ToString();
            minute = Convert.ToInt32(comboBox11.SelectedItem.ToString());
            interval_minute = minute;
              if (minute < 10)
                label38.Visible = true;   
            else
                label38.Visible = false;
            label34.Text = "" + minute;
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  label45.Text = comboBox12.SelectedItem.ToString();
            second = Convert.ToInt32(comboBox12.SelectedItem.ToString());
            interval_second = second;
              if (second < 10)
                label45.Visible = true;   
            else
                label45.Visible = false;

            label33.Text = "" + second;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            in_commission = true;

            Console.Write("每隔  被啟動 , timer4  被啟動  ");

            label33.Text = "" + second;
            label34.Text = "" + minute;
            label35.Text = "" + hour;

            if (second < 10)
                label45.Visible = true;
            else
                label45.Visible = false;

            if (minute < 10)
                label38.Visible = true;
            else
                label38.Visible = false;


            //倒數設定
            if (second == 0)
            {
                second = 60;
                minute--;
            }


            if (second == 60 && hour == 0 && minute == 0)
            { minute = 0; }

            if (hour > 0 && minute < 0)
            { minute = 59; hour--; }

            second--;

            //1分鐘前提醒
            if (hour == 0 && minute < 1 && checkBox1.Checked && first)
            {
                first = false;
                Clock_show cs = new Clock_show(second);
                cs.buttonClickedEvent += form2ButtonClicked;
                cs.Show();
            }


            //倒數關機
            if (minute == 0 && hour == 0 && second == 0
                && radioButton1.Checked && radioButton12.Checked)
            {
                Console.Write("倒數關機");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, "-s -f -t 0 ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                label33.Text = "0";
            }

            //倒數重新開機
            if (minute == 0 && hour == 0 && second == 0
                 && radioButton2.Checked && radioButton12.Checked)
            {
                Console.Write("重新開機");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, "-r  ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                label33.Text = "0";
            }

            //倒數休眠
            if (minute == 0 && hour == 0 && second == 0
                 && radioButton3.Checked && radioButton12.Checked)
            {
                Console.Write("倒數休眠");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, " -h  ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                label33.Text = "0";
            }

            //倒數登出
            if (minute == 0 && hour == 0 && second == 0
                && radioButton4.Checked && radioButton12.Checked)
            {
                Console.Write("倒數登出");
                ProcessStartInfo info = new ProcessStartInfo(FileLink, "-l ");
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process shutDown = Process.Start(info);
                label33.Text = "0";
            }

            //倒數鬧鐘
            if (minute == 0 && hour == 0 && second == 0
                && radioButton8.Checked && radioButton12.Checked)
            {
                Console.Write("倒數鬧鐘");

                //播放音樂
                string play_file = power_APP.Properties.Settings.Default.clock_user;

                if (play_file.Contains(".mp3"))
                {
                    wplayer.URL = play_file;
                    wplayer.controls.play();
                }
                else
                {
                    SoundPlayer myMusic = new SoundPlayer(play_file);
                    myMusic.Play();
                }

                Timeup TP = new Timeup();
                TP.Show();
                label33.Text = "0";
            }

            //倒數訊息
            if (minute == 0 && hour == 0 && second == 0
                && radioButton9.Checked && radioButton12.Checked)
            {
                Console.Write("倒數訊息");
                Message_show MS = new Message_show();
                MS.Show();
                label33.Text = "0";
            }

            //倒數執行程式
            if (minute == 0 && hour == 0 && second == 0
                && radioButton7.Checked && radioButton12.Checked)
            {
                string exe_pro = power_APP.Properties.Settings.Default.exe_user;
                ProcessStartInfo info = new ProcessStartInfo(exe_pro);
                info.WindowStyle = ProcessWindowStyle.Normal;
                Process exe = Process.Start(info);
                label33.Text = "0";
            }

            //倒數關閉螢幕
            if (minute == 0 && hour == 0 && second == 0
                && radioButton10.Checked && radioButton12.Checked)
            {
                SendMessage(this.Handle.ToInt32(), WM_SYSCOMMAND, SC_MONITORPOWER, 2);//DLL function
                label33.Text = "0";
            }

            if (hour == 0 && minute == 0 && second == 0)
            {
                hour = interval_hour;
                minute = interval_minute;
                second = interval_second;
                first = true;
                //timer4.Stop();
            }
        }

    }
}
