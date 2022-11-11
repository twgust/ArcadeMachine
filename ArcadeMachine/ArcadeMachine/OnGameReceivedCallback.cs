using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeMachine
{
    internal interface OnGameReceivedCallback
    {
        void StartGame(String gameTitle, String path);
    }
}
