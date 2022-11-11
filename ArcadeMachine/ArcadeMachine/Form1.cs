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
        public StartGame gameDelegate;

       // private Controller controller;

        public Form1()
        {          
            InitializeComponent();
           // gameDelegate = new StartGame(startGame);
            new Controller().GameStartEvent += startGame;
           // controller.GameStartEvent += startGame;

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
         
        }

        /// <summary>
        /// Method <c>startGame</c> Delegate, start game on UI thread...
        /// once delegate is invoked.
        /// Flow of method invocation:
        /// 1) NetworkService.init() -(callback interface)-> 
        /// 2) Controller.startGame(String path)  -(delegate)-> 
        /// 3) gui.startGame (UI Thread)
        /// </summary>
        /// <param name="path">path of the .exe file to be executed</param>
        //public void startGame(String path)
        //{
        //    var hWnd = this.Handle;
        //    String str = hWnd.ToString();
        //    ProcessStartInfo info = new ProcessStartInfo();

        //    info.FileName = (path);
        //    info.Arguments = "--parentHWND " + hWnd;
        //    Process.Start(info);
        //}

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