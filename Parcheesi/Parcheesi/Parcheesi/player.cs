using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcheesi
{
    public class player
    {
        public int num;
        //public int inHouse = 4;
        public int atHome = 0;
        public int doorStep;
        public int rampSpot;
        public int firstRamp;
        public bool pastGreen = false;
        public int[] pieces = { 0, 0, 0, 0, 0 };
        
        public player(int n, int dStep, int rSpot, int fRamp)
        {
            num = n;
            doorStep = dStep;
            rampSpot = rSpot;
            firstRamp = fRamp;
        }
    }
}
