using System.Diagnostics;

namespace ArcadeMachine
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                Debug.WriteLine(p.ProcessName);
            }

            NetworkService network = new NetworkService(7117, "127.0.0.1");
            network.startServer();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            
        }
    }
}