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
    public partial class Form1 : Form
    {


        [DllImport("winmm.dll")]
        private static extern int mciSendString(string strCommand,
                StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        [DllImport("winmm.dll")]
        public static extern int mciGetErrorString(int errCode,
                      StringBuilder errMsg, int buflen);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {




        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }




    }
}






//http://www.codeproject.com/Articles/63094/Simple-MCI-Player
//http://www.pinvoke.net/default.aspx/winmm.mcisendstring

















/*CMP3_MCI myMp3;
    std::string address= "C:\\Users\\music embed testing\\test.mp3";
    myMp3.Load(address);
    myMp3.Play();

//Load function

    void Load(string szFileName)
    {
        m_szFileName = szFileName;
        Load2();
    }

//load2 funtion

    void Load2()
    {
          std::string szCommand = "open \"" + GetFileName() + "\" type mpegvideo" + GetFileName();        
           mciSendString(szCommand.c_str(), NULL, 0, 0); //gives me error code of 259(unidentified keyword)
    }

//play function

    void Play()
    {
        std::string szCommand = "play " + GetFileName();
        mciSendString(szCommand.c_str(), NULL, 0, 0); //gives me error code of 263
    }

//getFileName basically returns m_szFileName stored as private attribute*/



/*BackgroundWorker bw = new BackgroundWorker();


        bw.WorkerReportsProgress = true;


        bw.DoWork += new DoWorkEventHandler(
        delegate(object o, DoWorkEventArgs args)
        {
            BackgroundWorker b = o as BackgroundWorker;

            string path = System.IO.Path.GetFullPath(".../.../sounds/") + "C4" + ".mp3";
            string command;
            command = "open \"" + path + "\" type mpegvideo alias fileC4";
            mciSendString(command, null, 0, IntPtr.Zero);
            command = "play all";
            mciSendString(command, null, 0, IntPtr.Zero);
        });


        bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
        delegate(object o, RunWorkerCompletedEventArgs args)
        {
            label1.Text = "HOORAY!";
        });

        bw.RunWorkerAsync();*/