using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;

namespace ArcadeMachine
{
    public partial class Form1 : Form
    {
        private string gamesDirPath = "C:\\Users\\sweet\\Desktop\\gamesdir\\";
        private string GameDirPath = "C:\\Users\\sweet\\Desktop\\gamesdir\\game\\";
        private string GameExecPath = "C:\\Users\\sweet\\Desktop\\gamesdir\\game\\My project.exe";

        public delegate void StartGame(String myString);

        /*
         * Initialises Form, creates a Controller and registers a listener.
         */
        public Form1()
        {          
            InitializeComponent();
            new Controller().GameStartEvent += startGame;

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
         
        }

        /*
         * Listener method that receives filepath for which game to launch.
         */
        private void startGame(object sender, String path)
        {
            Debug.WriteLine($"Startar {path}");
            var hWnd = this.Handle;
            String str = hWnd.ToString();
            ProcessStartInfo info = new ProcessStartInfo();

            info.FileName = (path);
            info.Arguments = "--parentHWND " + hWnd;
            Process.Start(info);
        }

        /// <summary>
        /// Error handling, should the app crash it's important to deal with the unity player 
        /// as it is a process independent from this application. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Debug.WriteLine("Stänger arkad...\n  " + " " + e.CloseReason);

            Process[] ArcadeMachine = Process.GetProcessesByName("ArcadeMachine.exe");
            Process[] unityCrashHdlProcess = Process.GetProcessesByName("UnityCrashHandler64");
        
            foreach (Process item in unityCrashHdlProcess)
            {
                Debug.WriteLine("Killing Crash handler <" + item.Id + ">" + " " + item.ProcessName) ;
                item.Kill();
            }
        }
    }
}