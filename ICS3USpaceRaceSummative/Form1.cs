using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICS3USpaceRaceSummative
{
    public partial class SpaceRace : Form
    {
        public SpaceRace()
        {
            InitializeComponent();
        }

        bool wDown = false;
        bool sDown = false;
        bool upKey = false;
        bool downKey = false;

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upKey = false;
                    break;
                case Keys.Down:
                    downKey = false;
                    break;
            }



        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upKey = true;
                    break;
                case Keys.Down:
                    downKey = true;
                    break;
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
