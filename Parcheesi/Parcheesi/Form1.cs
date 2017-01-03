using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcheesi
{
    public partial class parcheesi : Form
    {
        public parcheesi()
        {
            InitializeComponent();
        }

        // variables
        public Random r = new Random();
        public List<parcheesi_space.space> spaces = new List<parcheesi_space.space>();
        public List<PictureBox> homePieces = new List<PictureBox>();
        private int roll1, roll2;
        private int turnNum = 0;
        private int currentPiece = 0;
        private int doubles = 0;
        private bool homeAvail;
        public player pink = new player(1, 22, 17, 76);
        public player blue = new player(2, 39, 34, 83);
        public player yellow = new player(3, 56, 51, 90);
        public player green = new player(4, 5, 68, 69);

        private void parcheesi_Load(object sender, EventArgs e)
        {
            create_spaces();
            green.pastGreen = true;
            instructionsLbl.Text = "Welcome to the game of Parcheesi! \n" + 
                "This is a four player game, with each\n" +
                "player choosing one of the following\n" + 
                "colors: pink, blue, yellow, and green.\n" +
                "The players take turns in that order.\n" +
                "You must press the colored button\n" +
                "at least once per turn to see the\n" +
                "available moves. Each click cycles\n" +
                "through the current player's pieces.\n" +
                "Open spots will turn turquoise and\n" +
                "can then be clicked on. Blue spots\n" +
                "are safe; the only spots where\n" +
                "players can't send back other\n" +
                "player's pieces by landing on their\n" +
                "spot. Just before traveling the whole\n" +
                "board, the player's piece will reach\n" +
                "that player's ramp, the gateway to\n" +
                "Home, and only traversable by them.\n" +
                "The player must roll the exact number\n" +
                "to get them off of the ramp and into\n" +
                "the house. Whoever gets all four\n" +
                "pieces back home first wins.";

            for (int i = 0; i < spaces.Count; i++)
            {
                spaces[i].Click += new EventHandler(space_clicked);
            }

            turn();
        }

        private void turnBtn_Click(object sender, EventArgs ed)
        {
            if (!won())
            {
                turn();
            }
            else
            {
                rollLbl.Text = getPlayerName(turnNum) + " just won!";
                turnBtn.Enabled = false;
            }
        }

        public void turn()
        {
            if (doubles == 3) // checks if the player rolled 3 doubles in a row
            {
                doubles = 0;
                sacrifice(getPlayer(turnNum));
            }
            else
            {
                if (doubles == 0) // checks if it's the player's first turn
                {
                    turnNum++;
                    setButtonPic(pieceRotBtn, getPlayerName(turnNum));
                }

                // reset radio buttons
                die1Radio.Enabled = true;
                die2Radio.Enabled = true;
                bothDiceRadio.Enabled = true;
                homeBox.Image = null;
                
                // rolls 2 "die" for the player
                roll1 = roll();
                roll2 = roll();

                rollLbl.Text = "Your rolls were: " + roll1.ToString() + " and " + roll2.ToString();

                if (roll1 == roll2) // checks to doubles
                {
                    doubles++;
                }
                else
                {
                    doubles = 0;
                }
            }
        }

        private bool won()
        {
            bool w = false;
            if (pink.atHome >= 4 || blue.atHome >= 4 || yellow.atHome >= 4 || green.atHome >= 4)
            {
                w = true;
            }
            return w;
        }

        public void setButtonPic(Button pic, string colorPiece)
        {
            if (colorPiece == "Pink")
            {
                pic.Image = System.Drawing.Image.FromFile("pink_piece.png");
            }
            else if (colorPiece == "Blue")
            {
                pic.Image = System.Drawing.Image.FromFile("blue_piece.png");
            }
            else if (colorPiece == "Yellow")
            {
                pic.Image = System.Drawing.Image.FromFile("yellow_piece.png");
            }
            else if (colorPiece == "Green")
            {
                pic.Image = System.Drawing.Image.FromFile("green_piece.png");
            }
        }

        public string getPlayerName(int num)
        {
            string player = String.Empty;
            if (num % 4 == 1) // pink's turn
            {
                player = "Pink";
            }
            else if (num % 4 == 2) // blue's turn
            {
                player = "Blue";
            }
            else if (num % 4 == 3) // yellow's turn
            {
                player = "Yellow";
            }
            else if (num % 4 == 0) // green's turn
            {
                player = "Green";
            }
            return player;
        }

        public int getPlayer(int num)
        {
            int player = 0;
            if (num == 1 || num % 4 == 1) // pink's turn
            {
                player = 1;
            }
            else if (num == 2 || num % 4 == 2) // blue's turn
            {
                player = 2;
            }
            else if (num == 3 || num % 4 == 3) // yellow's turn
            {
                player = 3;
            }
            else if (num % 4 == 0) // green's turn
            {
                player = 4;
            }
            return player;
        }


        // functions for movement

        private int roll()
        {
            int roll = r.Next(1, 7);
            return roll;
        }

        public void sacrifice(int p)
        {
            int farthestPiece = 0;
            int farthestSpot = 0;
            if (p == 1) // pink
            {
                for (int i = 1; i <= 4; i++) // loops through player's pieces
                {
                    if (pink.pieces[i] > farthestSpot && pink.pieces[i] != 100 && pink.pieces[i] != 0) // checks if this piece is the farthest, but not at home or in the house
                    {
                        farthestPiece = i; // sets i to the farthest piece
                        farthestSpot = pink.pieces[i]; // sets i's location to the farthest spot
                    }
                }
                pink.pieces[farthestPiece] = 0; // set the farthest piece's location to 0 (the house)
            }
            else if (p == 2) // blue
            {
                for (int i = 1; i <= 4; i++)
                {
                    if (blue.pieces[i] > farthestSpot && blue.pieces[i] != 100 && blue.pieces[i] != 0) // checks if this piece is the farthest, but not at home or in the house
                    {
                        farthestPiece = i; // sets i to the farthest piece
                        farthestSpot = blue.pieces[i]; // sets i's location to the farthest spot
                    }
                }
                blue.pieces[farthestPiece] = 0; // set the farthest piece's location to 0 (the house)
            }
            else if (p == 3) // yellow
            {
                for (int i = 1; i <= 4; i++)
                {
                    if (yellow.pieces[i] > farthestSpot && yellow.pieces[i] != 100 && yellow.pieces[i] != 0) // checks if this piece is the farthest, but not at home or in the house
                    {
                        farthestPiece = i; // sets i to the farthest piece
                        farthestSpot = yellow.pieces[i]; // sets i's location to the farthest spot
                    }
                }
                yellow.pieces[farthestPiece] = 0; // set the farthest piece's location to 0 (the house)
            }
            else // green
            {
                for (int i = 1; i <= 4; i++)
                {
                    if (green.pieces[i] > farthestSpot && green.pieces[i] != 100 && green.pieces[i] != 0) // checks if this piece is the farthest, but not at home or in the house
                    {
                        farthestPiece = i; // sets i to the farthest piece
                        farthestSpot = green.pieces[i]; // sets i's location to the farthest spot
                    }
                }
                pink.pieces[farthestPiece] = 0; // set the farthest piece's location to 0 (the house)
            }
            remove(spaces.ElementAt(farthestSpot), p); // remove the farthest piece from it's spot
            addToHouse(p); // adds player's piece to the house
        }

        private void pieceRotBtn_Click(object sender, EventArgs e)
        {
            // resets the background of all spaces
            foreach (parcheesi_space.space sp in spaces)
            {
                sp.resetColor();
            }
            // increments current piece within the range of 1-4, so if it's 4, it gets reset to 1 (corresponds to player numbers)
            if (currentPiece < 4)
            {
                currentPiece++;
            }
            else
            {
                currentPiece = 1;
            }

            // switch to call the movePieceOptions function for the player
            switch (getPlayer(turnNum))
            {
                case 1:
                    movePieceOptions(pink, roll1, roll2);
                    break;
                case 2:
                    movePieceOptions(blue, roll1, roll2);
                    break;
                case 3:
                    movePieceOptions(yellow, roll1, roll2);
                    break;
                case 4:
                    movePieceOptions(green, roll1, roll2);
                    break;
            }
        }

        public void movePieceOptions(player p, int r1, int r2)
        {
            if (p.pieces[currentPiece] == 0) // piece is in house
            {
                if ((r1 == 6 && die1Radio.Enabled) || (r2 == 6 && die2Radio.Enabled)) // makes sure the roll hasn't been used
                {
                    spaces.ElementAt(p.doorStep).makeAvail(); // makes the doorstep available
                }
            }
            else if (die1Radio.Checked) // if the player wants to use the first die
            {
                checkSpots(p, roll1);
            }
            else if (die2Radio.Checked) // if the player wants to use the second die
            {
                checkSpots(p, roll2);
            }
            else if (bothDiceRadio.Checked) // if the player wants to use both die
            {
                checkSpots(p, roll1 + roll2);
            }
        }

        public void checkSpots(player p, int r) // function to check spots to move to once movePieceOptions knows what roll(s) to use
        {
            int spotNum = p.pieces[currentPiece] + r; // finds the spot that roll would move the piece to
            if (p.pieces[currentPiece] >= p.firstRamp) // on ramp
            {
                if (spotNum <= p.firstRamp + 6) // can move to another ramp spot
                {
                    spaces.ElementAt(spotNum).makeAvail(); // makes ramp spot available
                }
                else if (spotNum == p.firstRamp + 7) // right roll to get them to home
                {
                    homeAvail = true;
                    homeBox.Image = System.Drawing.Image.FromFile("home.png");
                }
            }
            else
            {
                if (p.num != 4 && spotNum > 68) // incremented past the main board spots, but not onto a ramp
                {
                    spotNum -= 68;
                    p.pastGreen = true;
                }
                if (p.pieces[currentPiece] != 0 && !checkBlockades(p.pieces[currentPiece], spotNum)) // checks that there are no blockades between the current location and spotNum
                {
                    if (p.pastGreen)
                    {
                        if (spotNum <= p.rampSpot)
                        {
                            spaces.ElementAt(spotNum).makeAvail();
                        }
                        else // goes past ramp
                        {
                            //if (spotNum < 13) // in the case of green...
                            {
                            //    spotNum += 68;
                            }
                            spaces.ElementAt(p.firstRamp - (p.rampSpot - spotNum + 1)).makeAvail(); // makes the spot on the ramp available
                        }
                    }
                    else if (spotNum >= p.rampSpot) // checks that spotNum is before the player's ramp
                    {
                        spaces.ElementAt(spotNum).makeAvail();
                    }
                }
            }
        }

        public bool checkBlockades(int current, int toSpot)
        {
            bool blocked = false;
            if (toSpot <= 68)
            {
                for (int i = current+1; i <= toSpot; i++) // oops through every spot between current and the toSpot
                {
                    if (spaces.ElementAt(i).check_Blockade() && spaces.ElementAt(i).spot1 != getPlayer(currentPiece)) // checks the i is one of the regular board spots and then whether it's blocked
                    {
                        blocked = true;
                    }
                }
            }
            else
            {
                for (int i = current+1; i <= 68; i++) // oops through every spot between current and the toSpot
                {
                    if (spaces.ElementAt(i).check_Blockade() && spaces.ElementAt(i).spot1 != getPlayer(currentPiece)) // checks the i is one of the regular board spots and then whether it's blocked
                    {
                        blocked = true;
                    }
                }
                for (int i = 0; i <= toSpot; i++) // oops through every spot between current and the toSpot
                {
                    if (spaces.ElementAt(i).check_Blockade() && spaces.ElementAt(i).spot1 != getPlayer(currentPiece)) // checks the i is one of the regular board spots and then whether it's blocked
                    {
                        blocked = true;
                    }
                }
            }
            
            return blocked;
        }

        private void space_clicked(object sender, EventArgs e)
        {
            parcheesi_space.space s = sender as parcheesi_space.space;
            if (s.avail) // checks that the spce clicked was available
            {
                s.avail = false;
                int p = getPlayer(turnNum);
                foreach (parcheesi_space.space sp in spaces)
                {
                    sp.resetColor();
                }

                // checks which player, then sets their piece to the space's spot number
                if (p == 1)
                {
                    remove(spaces.ElementAt(pink.pieces[currentPiece]), p);
                    pink.pieces[currentPiece] = s.numSpace;
                }
                else if (p == 2)
                {
                    remove(spaces.ElementAt(blue.pieces[currentPiece]), p);
                    blue.pieces[currentPiece] = s.numSpace;
                }
                else if (p == 3)
                {
                    remove(spaces.ElementAt(yellow.pieces[currentPiece]), p);
                    yellow.pieces[currentPiece] = s.numSpace;
                }
                else
                {
                    remove(spaces.ElementAt(green.pieces[currentPiece]), p);
                    green.pieces[currentPiece] = s.numSpace;
                }

                // checks if the space was the player's doorstep
                if (p == s.doorStep)
                {
                    removeFromHouse(p);

                    // disables the correct radio button
                    if (roll1 == 6)
                    {
                        die1Radio.Enabled = false;
                        die1Radio.Checked = false;
                        die2Radio.Checked = true;
                        bothDiceRadio.Enabled = false;
                        bothDiceRadio.Checked = false;
                    }
                    else
                    {
                        die1Radio.Checked = true;
                        die2Radio.Enabled = false;
                        die2Radio.Checked = false;
                        bothDiceRadio.Enabled = false;
                        bothDiceRadio.Checked = false;
                    }
                }
                else
                {
                    // disables the correct radio button
                    if (die1Radio.Checked)
                    {
                        die1Radio.Enabled = false;
                        die1Radio.Checked = false;
                        die2Radio.Checked = true;
                        bothDiceRadio.Enabled = false;
                        bothDiceRadio.Checked = false;
                    }
                    else if (die2Radio.Checked)
                    {
                        die1Radio.Checked = true;
                        die2Radio.Enabled = false;
                        die2Radio.Checked = false;
                        bothDiceRadio.Enabled = false;
                        bothDiceRadio.Checked = false;
                    }
                    else
                    {
                        die1Radio.Enabled = false;
                        die1Radio.Checked = false;
                        die2Radio.Enabled = false;
                        die2Radio.Checked = false;
                        bothDiceRadio.Enabled = false;
                        bothDiceRadio.Checked = false;
                    }
                }

                // pushes the player's piece to the space
                pushPiece(s, p);
            }
        }

        public void pushPiece(parcheesi_space.space s, int pieceType)
        {
            if (s.spot1 == 0) // if spot1 is empty
            {
                s.spot1 = pieceType;
                pieceChange(s.piece1, pieceType);
            }
            else if (s.spot1 == pieceType) // if spot1 already contains one of the player's pieces
            {
                if (s.spot2 == 0) // checks if spot2 is empty
                {
                    s.spot2 = pieceType;
                    pieceChange(s.piece2, pieceType);
                }
                else if (s.spot3 == 0) // checks if spot3 is empty
                {
                    s.spot3 = pieceType;
                    pieceChange(s.piece2, pieceType);
                }
            }
            else // spot 1 has another player's piece in it
            {
                if (s.safe) // checks if it's a safe spot
                {
                    if (s.spot2 == 0) // checks if spot2 is empty
                    {
                        s.spot2 = pieceType;
                        pieceChange(s.piece2, pieceType);
                    }
                    else if (s.spot3 == 0) // checks if spot3 is empty
                    {
                        s.spot3 = pieceType;
                        pieceChange(s.piece2, pieceType);
                    }
                }
                else // normal spot - send back the other player's piece
                {
                    addToHouse(s.spot1); // add the other player's piece to their house
                    remove(s, s.spot1); // rmoves the other player's visual piece
                    s.spot1 = pieceType; // sets the space's spot to the new player's piece
                    pieceChange(s.piece1, pieceType);
                }
            }
        }

        public void remove(parcheesi_space.space s, int pieceType)
        {
            // finds the spot with the player's piece in it and removes the picture of the piece
            if (s.spot1 == pieceType)
            {
                s.spot1 = 0;
                s.piece1.BackgroundImage = null;
            }
            else if (s.spot2 == pieceType)
            {
                s.spot2 = 0;
                s.piece2.BackgroundImage = null;
            }
            else if (s.spot3 == pieceType)
            {
                s.spot3 = 0;
                s.piece3.BackgroundImage = null;
            }
        }

        public void pieceChange(PictureBox pic, int pieceType)
        {
            pic.BackgroundImageLayout = ImageLayout.Stretch;
            pic.BringToFront();

            // sets the correct picture based on the pieceType
            if (pieceType == 1)
            {
                pic.BackgroundImage = System.Drawing.Image.FromFile("pink_piece.png");
            }
            else if (pieceType == 2) // blue
            {
                pic.BackgroundImage = System.Drawing.Image.FromFile("blue_piece.png");
            }
            else if (pieceType == 3) // yellow
            {
                pic.BackgroundImage = System.Drawing.Image.FromFile("yellow_piece.png");
            }
            else if (pieceType == 4) // green
            {
                pic.BackgroundImage = System.Drawing.Image.FromFile("green_piece.png");
            }
        }

        private void homeBox_Click(object sender, EventArgs e)
        {
            if (homeAvail)
            {
                int p = getPlayer(turnNum);
                pushHome(p);

                // sets the current player's piece based on the value of p
                if (p == 1)
                {
                    remove(spaces.ElementAt(pink.pieces[currentPiece]), p);
                    pink.pieces[currentPiece] = 100;
                    pink.atHome++;
                }
                else if (p == 2)
                {
                    remove(spaces.ElementAt(blue.pieces[currentPiece]), p);
                    blue.pieces[currentPiece] = 100;
                    blue.atHome++;
                }
                else if (p == 3)
                {
                    remove(spaces.ElementAt(yellow.pieces[currentPiece]), p);
                    yellow.pieces[currentPiece] = 100;
                    yellow.atHome++;
                }
                else
                {
                    remove(spaces.ElementAt(green.pieces[currentPiece]), p);
                    green.pieces[currentPiece] = 100;
                    green.atHome++;
                }

                // disables the correct radio buttons
                if (die1Radio.Checked)
                {
                    die1Radio.Enabled = false;
                    die1Radio.Checked = false;
                    die2Radio.Checked = true;
                    bothDiceRadio.Enabled = false;
                    bothDiceRadio.Checked = false;
                }
                else if (die2Radio.Checked)
                {
                    die1Radio.Checked = true;
                    die2Radio.Enabled = false;
                    die2Radio.Checked = false;
                    bothDiceRadio.Enabled = false;
                    bothDiceRadio.Checked = false;
                }
                else
                {
                    die1Radio.Enabled = false;
                    die1Radio.Checked = false;
                    die2Radio.Enabled = false;
                    die2Radio.Checked = false;
                    bothDiceRadio.Enabled = false;
                    bothDiceRadio.Checked = false;
                }
            }
        }

        private void pushHome(int p)
        {
            // creates a new picture box in a random spot in the home
            homeAvail = false;
            homeBox.Image = null;
            Random r = new Random();
            int x = r.Next(225, 368);
            int y = r.Next(225, 368);
            homePieces.Add(new PictureBox());
            homePieces[homePieces.Count-1].Enabled = true;
            homePieces[homePieces.Count-1].Visible = true;
            homePieces[homePieces.Count-1].BackgroundImageLayout = ImageLayout.Stretch;
            homePieces[homePieces.Count-1].Location = new Point(x, y);
            homePieces[homePieces.Count - 1].Size = new System.Drawing.Size(20, 20);

            // sets the image based on the value of p
            if (p == 1)
            {
                homePieces[homePieces.Count-1].BackgroundImage = System.Drawing.Image.FromFile("pink_piece.png");
            }
            else if (p == 2)
            {
                homePieces[homePieces.Count-1].BackgroundImage = System.Drawing.Image.FromFile("blue_piece.png");
            }
            else if (p == 3)
            {
                homePieces[homePieces.Count-1].BackgroundImage = System.Drawing.Image.FromFile("yellow_piece.png");
            }
            else
            {
                homePieces[homePieces.Count-1].BackgroundImage = System.Drawing.Image.FromFile("green_piece.png");
            }
            this.Controls.Add(homePieces[homePieces.Count-1]);
            homePieces[homePieces.Count - 1].BringToFront();
        }

        public void addToHouse(int p)
        {
            // added a piece back to the house since it was sent back
            if (p == 1) // pink
            {
                if (!pink1Box.Visible)
                {
                    pink1Box.Visible = true;
                }
                else if (!pink2Box.Visible)
                {
                    pink2Box.Visible = true;
                }
                else if (!pink3Box.Visible)
                {
                    pink3Box.Visible = true;
                }
                else
                {
                    pink4Box.Visible = true;
                }
            }
            else if (p == 2) // blue
            {
                if (!blue1Box.Visible)
                {
                    blue1Box.Visible = true;
                }
                else if (!blue2Box.Visible)
                {
                    blue2Box.Visible = true;
                }
                else if (!blue3Box.Visible)
                {
                    blue3Box.Visible = true;
                }
                else
                {
                    blue4Box.Visible = true;
                }
            }
            else if (p == 3) // yellow
            {
                if (!yellow1Box.Visible)
                {
                    yellow1Box.Visible = true;
                }
                else if (!yellow2Box.Visible)
                {
                    yellow2Box.Visible = true;
                }
                else if (!yellow3Box.Visible)
                {
                    yellow3Box.Visible = true;
                }
                else
                {
                    yellow4Box.Visible = true;
                }
            }
            else // green
            {
                if (!green1Box.Visible)
                {
                    green1Box.Visible = true;
                }
                else if (!green2Box.Visible)
                {
                    green2Box.Visible = true;
                }
                else if (!green3Box.Visible)
                {
                    green3Box.Visible = true;
                }
                else
                {
                    green4Box.Visible = true;
                }
            }
        }

        public void removeFromHouse(int p)
        {
            // removing a piece since it got out to the board
            if (p == 1) // pink
            {
                if (pink4Box.Visible)
                {
                    pink4Box.Visible = false;
                }
                else if (pink3Box.Visible)
                {
                    pink3Box.Visible = false;
                }
                else if (pink2Box.Visible)
                {
                    pink2Box.Visible = false;
                }
                else
                {
                    pink1Box.Visible = false;
                }
            }
            else if (p == 2) // blue
            {
                if (blue4Box.Visible)
                {
                    blue4Box.Visible = false;
                }
                else if (blue3Box.Visible)
                {
                    blue3Box.Visible = false;
                }
                else if (blue2Box.Visible)
                {
                    blue2Box.Visible = false;
                }
                else
                {
                    blue1Box.Visible = false;
                }
            }
            else if (p == 3) // yellow
            {
                if (yellow4Box.Visible)
                {
                    yellow4Box.Visible = false;
                }
                else if (yellow3Box.Visible)
                {
                    yellow3Box.Visible = false;
                }
                else if (yellow2Box.Visible)
                {
                    yellow2Box.Visible = false;
                }
                else
                {
                    yellow1Box.Visible = false;
                }
            }
            else // green
            {
                if (green4Box.Visible)
                {
                    green4Box.Visible = false;
                }
                else if (green3Box.Visible)
                {
                    green3Box.Visible = false;
                }
                else if (green2Box.Visible)
                {
                    green2Box.Visible = false;
                }
                else
                {
                    green1Box.Visible = false;
                }
            }
        }


        // functions to create spaces at startup

        private void create_spaces()
        {
            int dStep = 0;
            bool safe = false;
            Point pnt = new Point();
            Color boardBlue = Color.FromArgb(170, 204, 255);

            // loops through all spots that need to be created
            for (int i = 0; i <= 97; i++)
            {
                dStep = 0;
                safe = false;
                bool rotated = false;
                Color color = Color.White;

                // checks if the space if a player's doorstep
                if (i == pink.doorStep)
                {
                    dStep = 1;
                }
                else if (i == blue.doorStep)
                {
                    dStep = 2;
                }
                else if (i == yellow.doorStep)
                {
                    dStep = 3;
                }
                else if (i == green.doorStep)
                {
                    dStep = 4;
                }

                // checks if the space is a safe space
                if (i == 5 || i == 12 || i == 17 || i == 22 || i == 29 || i == 34 || 
                    i == 39 || i == 46 || i == 51 || i == 56 || i == 63 || i == 68) // if it's a safe spot
                {
                    safe = true;
                    color = boardBlue;
                }

                // sets all the points and rotations
                if (i >= 1 && i <= 8)
                {
                    pnt = new Point(329, 556 - ((i - 1) * 23));
                }
                else if (i >= 9 && i <= 16)
                {
                    pnt = new Point(394 + ((i - 9) * 23), 331);
                    rotated = true;
                }
                else if (i == 17)
                {
                    pnt = new Point(555, 270);
                    rotated = true;
                }
                else if (i >= 18 && i <= 25)
                {
                    pnt = new Point(555 - ((i - 18) * 23), 209);
                    rotated = true;
                }
                else if (i >= 26 && i <= 33)
                {
                    pnt = new Point(329, 180 - ((i - 26) * 23));
                }
                else if (i == 34)
                {
                    pnt = new Point(269, 18);
                }
                else if (i >= 35 && i <= 42)
                {
                    pnt = new Point(208, 19 + ((i - 35) * 23));
                }
                else if (i >= 43 && i <= 50)
                {
                    pnt = new Point(179 - ((i - 43) * 23), 207);
                    rotated = true;
                }
                else if (i == 51)
                {
                    pnt = new Point(17, 267);
                    rotated = true;
                }
                else if (i >= 52 && i <= 59)
                {
                    pnt = new Point(18 + ((i - 52) * 23), 329);
                    rotated = true;
                }
                else if (i >= 60 && i <= 67)
                {
                    pnt = new Point(207, 395 + ((i - 60) * 23));
                }
                else if (i == 68)
                {
                    pnt = new Point(267, 556);
                }
                else if (i >= 69 && i <= 75)
                {
                    pnt = new Point(267, 533 - ((i - 69) * 23));
                    color = Color.Red;
                }
                else if (i >= 76 && i <= 82)
                {
                    pnt = new Point(531 - ((i - 76) * 23), 270);
                    rotated = true;
                    color = Color.Red;
                }
                else if (i >= 83 && i <= 89)
                {
                    pnt = new Point(269, 41 + ((i - 83) * 23));
                    color = Color.Red;
                }
                else if (i >= 90 && i <= 97)
                {
                    pnt = new Point(41 + ((i - 90) * 23), 267);
                    rotated = true;
                    color = Color.Red;
                }
                createSpace(i, dStep, safe, pnt, rotated, color);
            }
        }

        private void createSpace(int i, int dStep, bool safe, Point pntCurrent, bool rot, Color c)
        {
            spaces.Add(new parcheesi_space.space(i, dStep, safe, c)); // creates the custom space control
            spaces[i].Enabled = true;
            spaces[i].Visible = true;
            spaces[i].BackColor = c;
            spaces[i].BringToFront();
            boardBox.SendToBack();
            spaces[i].Location = pntCurrent;
            if (rot) // checsk if it needs to be rotated
            {
                spaces[i].Size = new System.Drawing.Size(20, 55);
                spaces[i].rotate();
            }
            else
            {
                spaces[i].Size = new System.Drawing.Size(55, 20);
            }
            this.Controls.Add(spaces[i]);
        }
    }
}

