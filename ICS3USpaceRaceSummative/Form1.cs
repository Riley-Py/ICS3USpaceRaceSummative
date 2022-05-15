using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
/*********************************************************************************
 * Author: Riley Cant
 * Course: ICS3U
 * Program: Space Race
 * Description: A clone of the Atari game "Space Race" from the Summer of 1973
 * Date of comment: 05/14/22
 ********************************************************************************/
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

        Rectangle player1 = new Rectangle(230, 450, 10, 30);
        Rectangle player2 = new Rectangle(550, 450, 10, 30);

        List<Rectangle> leftSide = new List<Rectangle>();
        List<Rectangle> rightSide = new List<Rectangle>();

        List<int> rectangleSpeedLeft = new List<int>();
        List<int> rectangleSpeedRight = new List<int>();

        int playerSpeed = 5;
        int misslesX = 5;
        int misslesY = 7;
        int player1Score = 0;
        int player2Score = 0;

        Random randGen = new Random();

        SolidBrush whiteBrush = new SolidBrush(Color.White);

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
            e.Graphics.FillRectangle(whiteBrush, player1);
            e.Graphics.FillRectangle(whiteBrush, player2);

            for (int i = 0; i < leftSide.Count(); i++)
            {
                e.Graphics.FillRectangle(whiteBrush, leftSide[i]);
            }
            for (int i = 0; i < rightSide.Count(); i++)
            {
                e.Graphics.FillRectangle(whiteBrush, rightSide[i]);
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            playerMovements();

            leftSideMovements();

            rightSideMovements();

            collision();

            Refresh();

        }

        public void playerMovements()
        {
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;

            }
            if (upKey == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }
            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }
            if (downKey == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }

        }

        public void leftSideMovements()
        {
            for (int i = 0; i < leftSide.Count(); i++)
            {
                int x = leftSide[i].X + rectangleSpeedLeft[i];
                leftSide[i] = new Rectangle(x, leftSide[i].Y, misslesX, misslesY);

                if (leftSide[i].X >= this.Width)
                {
                    leftSide.RemoveAt(i);
                    rectangleSpeedLeft.RemoveAt(i);
                }

            }
            int y = randGen.Next(10, 400);
            if (leftSide.Count() < 15)
            {
                leftSide.Add(new Rectangle(10, y, misslesX, misslesY));
                rectangleSpeedLeft.Add(randGen.Next(5, 10));

            }

        }
        public void rightSideMovements()
        {
            for (int i = 0; i < rightSide.Count(); i++)
            {
                int x = rightSide[i].X - rectangleSpeedRight[i];
                rightSide[i] = new Rectangle(x, rightSide[i].Y, misslesX, misslesY);

                if (rightSide[i].X <= 0)
                {
                    rightSide.RemoveAt(i);
                    rectangleSpeedRight.RemoveAt(i);
                }

            }
            int y = randGen.Next(10, 400);
            if (rightSide.Count() < 10)
            {
                rightSide.Add(new Rectangle(790, y, misslesX, misslesY));
                rectangleSpeedRight.Add(randGen.Next(5, 10));
            }

        }

        public void collision()
        {
            for (int i = 0; i < leftSide.Count(); i++)
            {
                for (int i2 = 0; i2 < rightSide.Count; i2++)
                {
                    if (player1.IntersectsWith(leftSide[i]) || player1.IntersectsWith(rightSide[i2]))
                    {
                        restartPlayer1();
                    }
                    else if (player2.IntersectsWith(leftSide[i]) || player2.IntersectsWith(rightSide[i2]))
                    {
                        restartPlayer2();
                    }
                }
            }
            if (player1.Y == 0)
            {
                player1Score++;
                restartPlayer1();
                scoring();
                

            }
            if (player2.Y == 0)
            {
                player2Score++;
                restartPlayer2();
                scoring();
            }
        }
        public void restartPlayer1()
        {
            player1.X = 230;
            player1.Y = 450;
        }
        public void restartPlayer2()
        {
            player2.X = 550;
            player2.Y = 450;
        }

        public void scoring()
        {
            scoreLabel1.Text = $"{player1Score}";
            scoreLabel2.Text = $"{player2Score}";

        }
    }
}
