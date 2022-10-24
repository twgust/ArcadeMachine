using System.Diagnostics;

namespace ArcadeMachine
{
    public partial class Form1 : Form
    {
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
            startGame("C:\\Users\\sweet\\Desktop\\game\\My project.exe");
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
    }
}