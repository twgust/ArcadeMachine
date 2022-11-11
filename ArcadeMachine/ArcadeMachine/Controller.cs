using ArcadeMachineLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcadeMachine
{
    internal class Controller: OnGameReceivedCallback
    {
        public event EventHandler<string> GameStartEvent;
        //private Form1 gui; 
        private OnGameReceivedCallback callback;
        //public Controller( Form1 form)
        //{
        //    this.gui = form;
          //  callback = this;
        //    CreateModules();
        //}

        public Controller()
        {
            callback = this;
            CreateModules();
        }

        private void CreateModules()
        {
            NetworkService network = new NetworkService(11111, "127.0.0.1", callback);
            network.startServer();
        }

        /// <summary>
        /// Method <c>StartGame</c> Callback function. invoked by NetworkService, 
        /// notifies GUI thread to start game in WinForm.
        /// </summary>
        public void StartGame(string gameTitle, string path)
        {
            OnGameStart(gameTitle, path);
            // gui.BeginInvoke(gui.gameDelegate, path);
        }

        protected virtual void OnGameStart(string gameTitle, string path)
        {
            GameStartEvent(this, path);
        }
    }
}

