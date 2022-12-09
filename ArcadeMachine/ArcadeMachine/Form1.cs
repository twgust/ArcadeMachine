using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ArcadeMachine
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Form1 : Form
    {
       public delegate void StartGame(String myString);
       public StartGame gameDelegate;
 
        [DllImport("UnityPlayer")]
        private static extern int UnityMain(IntPtr hInstance, IntPtr hPrevInstance,
      [MarshalAs(UnmanagedType.LPWStr)] string lpCmdline, int nShowCmd);

        [DllImport("User32.dll")]
        private static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            gameDelegate = new StartGame(LoadGame);
    
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            Controller controller = new Controller(this);
            StartMenu();
        }

        /// <summary>
        /// Embeds UnityPlayer.dll into application and runs it on its own thread
        /// </summary>
        public void StartMenu()
        {
            var handle = Process.GetCurrentProcess().Handle;
            String commandArgs = "--parentHWND " + this.Handle + " delayed";
            Thread thread = new Thread(() =>
            {
                UnityMain(handle, IntPtr.Zero, commandArgs, 1);
            });
            thread.Start();
            SetParent(IntPtr.Zero, this.Handle);
        }

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
      
        public void LoadGame(String path)
        {
            try
            {    
                // instantiate Game View-Holder [Frame/Form]
            GAMELAYER GameProperFrame = new GAMELAYER(this, path);
            
            ShowGameProper_View(GameProperFrame);
            HideGameMenu_View(); // <-- refers to Form1

            // pass Integer pointer of Game Frame to StartGame function 
            StartGameProper(path, GameProperFrame.Handle);

            }catch  (Exception ex) { Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
       
        }

        /// <summary>
        /// </summary>
        /// <param name="frame"></param>
        private void ShowGameProper_View(GAMELAYER frame)
        {
            // game layer frame
            frame.Location = this.Location;
            frame.StartPosition = FormStartPosition.Manual;
          //  frame.FormClosing += delegate { this.Show(); };
            frame.Show();
            frame.WindowState = FormWindowState.Maximized;
        }


        /// <summary>
        /// Hide Form1.cs (Game menu) from being displayed.
        /// </summary>
        private void HideGameMenu_View()
        {
            this.Hide();
            //this.Visible = false;
            //this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Shows Form1.cs (Game Menu)
        /// </summary>
        private void ShowGameMenu_View()
        {
            this.Location = this.Location;
            this.StartPosition = FormStartPosition.Manual;
            //  frame.FormClosing += delegate { this.Show(); };
            this.Show();
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Function starts the game specified by path,
        /// invoked by LoadGame(String path);
        /// </summary>
        /// <param name="path">Path of game.exe received from Unity Menu</param>
        /// <param name="wdwPtr">Handle to the Winforms Window in which the Game will be displayed</param>
        public void StartGameProper(String path, IntPtr wdwPtr)
        {
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}


