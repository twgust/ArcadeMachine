using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;

namespace ArcadeMachine
{
    public partial class Form1 : Form
    {
        private string gamesDirPath = "C:\\Users\\sweet\\Desktop\\gamesdir\\";
        private string GameDirPath = "C:\\Users\\sweet\\Desktop\\gamesdir\\game\\";
        private string GameExecPath = "C:\\Users\\sweet\\Desktop\\gamesdir\\game\\My project.exe";


        public Form1()
        {
          
            InitializeComponent();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            // define path for a unity game built for WINDOWS. 
            startGame(GameExecPath);
        }

        private void startGame(String path)
        {
            var hWnd = this.Handle;
            String str = hWnd.ToString();
            ProcessStartInfo info = new ProcessStartInfo();

            info.FileName = (path);
            info.Arguments = "--parentHWND " + hWnd;
            Process.Start(info);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Debug.WriteLine("Stänger arkad...\n  " + " " + e.CloseReason);

            Process[] unityCrashHdlProcess = Process.GetProcessesByName("UnityCrashHandler64");
            
            foreach (Process item in unityCrashHdlProcess)
            {
                Debug.WriteLine("Killing Crash handler <" + item.Id + ">" + " " + item.ProcessName) ;
                item.Kill();

            }
        }
    }
}