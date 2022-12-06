using Microsoft.VisualBasic.ApplicationServices;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ArcadeMachine
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Form1 : Form
    {
        private string gamesDirPath = "C:\\Users\\sweet\\Desktop\\gamesdir\\";
        private string GameDirPath = "C:\\Users\\sweet\\Desktop\\gamesdir\\game\\";
        private string GameExecPath = "C:\\Users\\sweet\\Desktop\\gamesdir\\game\\My project.exe";

        public delegate void StartGame(String myString);
        public StartGame gameDelegate;
        private Process process;


        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            gameDelegate = new StartGame(LoadGame);
        }

        /// <summary>
        /// </summary>
        public void StartMenu()
        { 
            var hWnd = this.Handle;
            String path = "C:\\Users\\sweet\\Desktop\\proj\\Windowed\\Arcade_menu.exe";
            ProcessStartInfo info = new ProcessStartInfo();
            info.CreateNoWindow = true;
            info.FileName = (path);
            info.Arguments = "--parentHWND " + hWnd;
            Process.Start(info);
            Debug.WriteLine("!!!!!"+ Thread.CurrentThread.ToString());
        }


        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        public void LoadGame(String path)
        {
            // instantiate Game View-Holder [Frame/Form]
            var GameProperFrame = new GAMELAYER(this);
            ShowGameProper_View(GameProperFrame);
            HideGameMenu_View(); // <-- refers to Form1

            // pass Integer pointer of Game Frame to StartGame function 
            StartGameProper(path, GameProperFrame.Handle);
        }

        /// <summary>
        /// </summary>
        /// <param name="frame"></param>
        private void ShowGameProper_View(GAMELAYER frame)
        {
            // game layer frame
            frame.Location = this.Location;
            frame.StartPosition = FormStartPosition.Manual;
            frame.FormClosing += delegate { this.Show(); };
            frame.Show();
            frame.WindowState = FormWindowState.Maximized;
        }


        /// <summary>
        /// </summary>
        private void HideGameMenu_View()
        {
            this.Hide();
            this.Visible = false;
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// </summary>
        private void ShowGameMenu_View()
        {
            this.Show();
            this.Visible = true;
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// </summary>
        /// <param name="path">Path of game.exe received from Unity Menu</param>
        /// <param name="wdwPtr">Handle to the WinForms Frame</param>
        public void StartGameProper(String path, IntPtr wdwPtr)
        {
            killArcade();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = (path);
            info.Arguments = "--parentHWND " + wdwPtr;
            Process.Start(info);
            Debug.WriteLine("Game started");
        }

        /// <summary>
        /// </summary>
        private void HideGameProper_View(GAMELAYER frame)
        {
            frame.Hide();
            frame.Visible = false;
            frame.WindowState = FormWindowState.Minimized;
        }

        public void OnGameQuit()
        {
            ShowGameMenu_View();
        }

        public void OneFrameTest(String path)
        {
            StartGameProper(path, this.Handle);
        }

        protected void killArcade()
        {
            Debug.WriteLine("Stänger arkad...");

            Process[] processes = System.Diagnostics.Process.GetProcessesByName("Arcade_menu");
            int i = 0;
            foreach (Process process in processes)
            {
                i++;
                Debug.WriteLine(process.ProcessName + i);
                process.Kill();
            }
        }


        /// <summary>
        /// Error handling, should the app crash it's important to deal with the unity player 
        /// as it is a process independent from this application. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            killArcade();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

            /*
        /// <summary>
        /// Method <c>StartGane</c> Delegate, start game on UI thread...
        /// once delegate is invoked.
        /// Flow of method invocation:
        /// 1) NetworkService.init() -(callback interface)-> 
        /// 2) Controller.StartGane(String path)  -(delegate)-> 
        /// 3) gui.StartGane (UI Thread)
        /// </summary>
        /// <param name="path">path of the .exe file to be executed</param>
        public void startGame(String path)
        {          
            var hWnd = this.Handle;
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = (path);
            //info.Arguments = "--parentHWND " + hWnd;
            Process.Start(info);
            Debug.WriteLine("ok");
        }
        */
