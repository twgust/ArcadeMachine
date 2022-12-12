using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ArcadeMachine
{
    partial class GAMELAYER
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Form1 Menu;
        private String Path;
        private String Title;

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        const int MYACTION_HOTKEY_ID = 1;


        public GAMELAYER(Form1 menu, String title, String path)
        {
            try
            {
                this.Menu = menu;
                this.Path = path;
                this.Title = title;
                RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID, 6, (int)Keys.PageDown);
            }
            catch (Exception ex) { Debug.WriteLine(ex.StackTrace + ex.Message); 
           
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == MYACTION_HOTKEY_ID)
            {
                Debug.WriteLine("> GLOBALKEYBIND [CTRL + SHIFT + INS] INVOKED");

                // step 1: kill game process
                killGame(this.Title);

                // step 2: hide game frame (form)
                // this.Hide();
                this.Visible = false;
                this.WindowState = FormWindowState.Minimized;

                // step 3: display menu frame (form)
                Menu.Show();
                Menu.WindowState = FormWindowState.Maximized;
                Menu.OnGameQuit(this);

            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// kills game process given path. 
        /// </summary>
        /// <param name="path"></param>
        protected void killGame(String path)
        {
            Debug.WriteLine(path + " KILLLLLL IT");
            if(path == "chooseme")
            {
                String str = "My project";
                Process[] custom = Process.GetProcessesByName(str);
                foreach (Process processItem in custom)
                {
                    Debug.WriteLine(processItem.ToString() );
                    processItem.Kill();
                }
            }
        }

        public void DisplayMenu()

        {
            Menu.Location = this.Location;
            Menu.StartPosition = FormStartPosition.Manual;
           // menu.FormClosing += delegate { this.Show(); };
            Menu.Show();
            Menu.WindowState = FormWindowState.Maximized;

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GAMELAYER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(250, 250);
            this.Name = "GAMELAYER";
            this.Text = "Form2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.GAMELAYER_Load);
            this.Click += new System.EventHandler(this.GAMELAYER_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GAMELAYER_KeyPress);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.GAMELAYER_PreviewKeyDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}