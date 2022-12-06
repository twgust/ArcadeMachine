using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcadeMachine
{
    internal class Controller: OnGameReceivedCallback
    {
        private Form1 gui; 
        private OnGameReceivedCallback callback;
        public Controller( Form1 form )
        {
            this.gui = form;
            callback = this;
            StartServer();
            //gui.StartMenu(); Bug - no input from unity

        }

        private void StartServer()
        {
            NetworkService network = new NetworkService(11111, "127.0.0.1", callback);
            Thread t = new Thread(network.init);
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// Method <c>StartGame</c> Callback function. invoked by NetworkService, 
        /// notifies GUI thread to start game in WinForm.
        /// </summary>
        public void StartGame(string gameTitle, string path)
        {
            gui.BeginInvoke(gui.gameDelegate, path);
        }
    }
}

