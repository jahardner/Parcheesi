using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace parcheesi_space
{
    public partial class space: UserControl
    {
        public int numSpace;
        public int spot1 = 0;
        public int spot2 = 0;
        public int spot3 = 0;
        public int doorStep = 0;
        public bool safe;
        public bool avail;
        private Color origColor;

        public space(int num, int dStep, bool s, Color backColor)
        {
            InitializeComponent();
            numSpace = num;
            doorStep = dStep;
            safe = s;
            origColor = backColor;
        }

        public bool check_Blockade()
        {
            bool blocked = false;
            if (spot1 != 0 && spot1 == spot2) // if the same player's pieces are in both spot1 and spot2
            {
                blocked = true;
            }
            return blocked;
        }

        public void resetColor()
        {
            this.BackColor = origColor; // resets the background color to the original
        }

        public void rotate()
        {
            // change in size is dealt wiht in Parcheesi, this changes the location of the picturebox object in the control
            piece1.Location = new Point(2, 0);
            piece2.Location = new Point(2, 19);
            piece3.Location = new Point(2, 39); 
        }

        public void makeAvail()
        {
            if (this.check_Blockade() == false) // checks that there isn't a blockade
            {
                this.BackColor = Color.Aqua; // changes the color
                avail = true;
            }
        }
    }
}
