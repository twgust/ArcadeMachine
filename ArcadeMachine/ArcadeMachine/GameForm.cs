using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcadeMachine
{
    public partial class GAMELAYER : Form
    {
        public GAMELAYER()
        {
            InitializeComponent();
        }
        //TODO register a keybutton listener which will initiate the "go back to menu" sequence, 
        // --> minimize game window, kill game process, start game menu, maximize game menu process.
        private void GAMELAYER_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine(e.KeyChar.ToString());
            if (e.KeyChar == 'o')
            {
                Debug.WriteLine(e.KeyChar);
                e.Handled = true;
            }
        }

        private void GAMELAYER_Load(object sender, EventArgs e)
        {
            Debug.WriteLine(e.GetType().ToString());
        }

        private void GAMELAYER_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }
    }
}
