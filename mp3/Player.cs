using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;



namespace WindowsFormsApplication1
{
    public partial class Player : Form
    {
        Boolean isPlay = false;
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, int hwndCallback);
        private string Pcommand;    
        private int volume;
        private StringBuilder returnData = new StringBuilder(128);
        bool agrego = false;

        public void open(String file)
        {
            int num, hor, min, seg;

            num = GetSongLenght();

            hor = (num / 3600);
            min = ((num - hor * 3600) / 60);
            seg = num - (hor * 3600 + min * 60);

            Pcommand = "open \"" + file + "\" type MPEGVideo alias MyMp3";
            mciSendString(Pcommand, null, 0, 0);

            isPlay = true;

            textime.Text = ((GetSongLenght()/60000).ToString() + ":" +(GetSongLenght()/1000 - (GetSongLenght() / 60000 * 60)).ToString("00"))  ;

            trackBar2.Maximum = (GetSongLenght() /1000);
            
            SetVolume(100);
            volume = 100;
            textBox1.Text = "1";
            trackBar1.Value = 100;
           
            isPlay = false;
            agrego = true;
        }

        public void play()
        {
            if (agrego == true)
            {
                if (isPlay == false)
                {
                    string command = "play MyMP3";
                    mciSendString(command, null, 0, 0);

                    isPlay = true;
                    timer1.Enabled = true;
                    timer1.Start();
                }
            }
            else
            {
                MessageBox.Show("Primero agregue una cancion");
            
            
            }
        }
        public void pause()
        {
            if (isPlay == true)
            {
                string command = "pause MyMP3";
                mciSendString(command, null, 0, 0);
                isPlay = false;
                timer1.Stop();
            }
        }
        public void stop()
        {
            string command = "stop MyMp3";
            mciSendString(command, null, 0, 0);
            isPlay = false;

            command = "close MyMp3";
            mciSendString(command, null, 0, 0);

            textime.Text = ("0:00");
            trackBar1.Value = 100;
            textBox1.Text = 1.ToString();
            textBox3.Text = ("0:00").ToString();
            trackBar2.Value = 0;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            timer1.Stop();
            label1.Text = "Titulo";
            trackBar2.Maximum = 10;
            duration = 0;

            agrego = false;
        }

        public void stop2()
        {
            string command = "pause MyMP3";
            mciSendString(command, null, 0, 0);
        }

        public Player()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open File";
            openFileDialog1.Filter = "mp3 (*mp3)|*mp3|wma (*wma)|*.wma|wav (*wav)|*.wav|puto (*.*)|*.*";
            openFileDialog1.FileName = "";
            openFileDialog1.FilterIndex = 0;

            openFileDialog1.InitialDirectory = "MyDocuments";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                open(openFileDialog1.FileName);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            play();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pause();
        }   

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            label1.Text = openFileDialog1.SafeFileName;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            stop();
        }

        public bool SetVolume(int volume)
        {
            if (volume >= 0 && volume <= 1000)
            {
                Pcommand = "setaudio MyMp3 volume to " + volume.ToString();
                mciSendString(Pcommand, null, 0, 0);
                return true;
            }
            else
                return false;
        }

       /* public bool SetBalance(int balance)
        {
            if (balance >= 0 && balance <= 1000)
            {
                Pcommand = "setaudio MyMp3 left volume to " + (1000 - balance).ToString();
               // error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
                Pcommand = "setaudio MyMp3 right volume to " + balance.ToString();
               // error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
                return true;
            }
            else
                return false;
        }*/
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                volume = trackBar1.Value;
                textBox1.Text = (volume / 100).ToString();

                SetVolume(volume);
            }
        }

       public int GetSongLenght()
        {
            if (isPlay == true)
            {
                mciSendString("status MyMp3 length", returnData, returnData.Capacity, 0);
                return int.Parse(returnData.ToString());
            }
            else
                return 0;
        }

       public int GetCurentMilisecond()
       {
           Pcommand = "status MyMp3 position";
            mciSendString(Pcommand, returnData,returnData.Capacity, 0);
           return int.Parse(returnData.ToString());
       }

       public void SetPosition(int miliseconds)
       {
           if (isPlay == true)
           {
               Pcommand = "play MyMp3 from " + miliseconds.ToString();
                mciSendString(Pcommand, null, 0, 0);
           }
           else
           {
               Pcommand = "seek MyMp3 to " + miliseconds.ToString();
                mciSendString(Pcommand, null, 0, 0);
           }
       }

       int duration = 0;
        
       private void trackBar2_Scroll_1(object sender, EventArgs e)
       {           
           SetPosition(trackBar2.Value*1000);

           textBox3.Text =((GetCurentMilisecond() / 60000).ToString() + ":"  + (GetCurentMilisecond() / 1000 - (GetCurentMilisecond() / 60000 * 60)).ToString());

           duration = trackBar2.Value;
       }
       
       private void timer1_Tick(object sender, EventArgs e)
       {
           duration++;

           trackBar2.Value = duration;
           if(duration ==  (GetSongLenght() /1000))
           {
               duration = 0;
               stop2();
               trackBar2.Value = 0;
               timer1.Stop();
               isPlay = false;
               SetPosition(0);
                };

           if (checkBox2.Checked == true)
           {
               if (trackBar2.Value == 0)
               { play(); }
           }

           if (duration >= 60)
           {
               int asd3 = (duration /60);
               textBox3.Text = (duration / 60).ToString() + ":" + (duration -(asd3*60 )).ToString("00"); ;
           }
           else
           {
               textBox3.Text = "0" + ":" + duration.ToString("00");
           };
       }

       private void checkBox1_CheckedChanged(object sender, EventArgs e)
       {
           if(checkBox1.Checked == true)
           {
               SetVolume(0);}
           else{
               SetVolume(volume);}
       }

       private void pictureBox4_Click(object sender, EventArgs e)
       {
           if (checkBox1.Checked == false)
           {checkBox1.Checked = true;}
           else
           { checkBox1.Checked = false; };
       }

       private void pictureBox5_Click(object sender, EventArgs e)
       {
           if (checkBox2.Checked == false)
           { checkBox2.Checked = true; }
           else
           { checkBox2.Checked = false; };
       }

       private void button1_Click(object sender, EventArgs e)
       {  
           openFileDialog1.Title = "Open File";
           openFileDialog1.Filter = "mp3 (*mp3)|*mp3|wma (*wma)|*.wma|wav (*wav)|*.wav|puto (*.*)|*.*";
           openFileDialog1.FileName = "";
           openFileDialog1.FilterIndex = 0;

           openFileDialog1.InitialDirectory = "MyDocuments";

           openFileDialog1.CheckFileExists = true;
           openFileDialog1.CheckPathExists = true;

           if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
               open(openFileDialog1.FileName);
       } 
    }  
    }





//https://www.caveofprogramming.com/c-sharp-tutorial/c-for-beginners-make-your-own-mp3-player-free.html
//http://stackoverflow.com/questions/22834609/developing-a-mp3-player-using-c-sharp
//http://www.codeproject.com/Articles/63094/Simple-MCI-Player
//http://www.pinvoke.net/default.aspx/winmm.mcisendstring