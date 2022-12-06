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
        private Form1 menu;

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        const int MYACTION_HOTKEY_ID = 1;


        public GAMELAYER(Form1 menu)
        {
            this.menu = menu;
            RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID, 6, (int)Keys.Insert);

        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == MYACTION_HOTKEY_ID)
            {
                Debug.WriteLine("> GLOBALKEYBIND [CTRL + SHIFT + INS] INVOKED");

                // step 1: kill game process
                killGame("My project.exe");

                // step 2: hide game frame (form)
                this.Hide();
                this.Visible = false;
                this.WindowState = FormWindowState.Minimized;

                // step 3: display menu frame (form)
                menu.OnGameQuit();
            }
            base.WndProc(ref m);
        }

        protected void killGame(String processName)
        {
            Process[] process = Process.GetProcessesByName(processName);
            foreach (Process processItem in process)
            {
               processItem.Kill();
            }
        }

        public void DisplayMenu()

        {
            
            menu.Visible = true;

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
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(250, 250);
            this.Name = "GAMELAYER";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.GAMELAYER_Load);
            this.Click += new System.EventHandler(this.GAMELAYER_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GAMELAYER_KeyPress);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.GAMELAYER_PreviewKeyDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}