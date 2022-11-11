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
        private OnGameReceivedCallback callback;

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
        }

        protected virtual void OnGameStart(string gameTitle, string path)
        {
            GameStartEvent(this, path);
        }
    }
}

