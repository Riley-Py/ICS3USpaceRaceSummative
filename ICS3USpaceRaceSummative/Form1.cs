using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Media;
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
            //Loads up the sounds right when the program is started
            playerSounds.Load();
            explosion.Load();
            end.Load();
        }
        //Variables for keys
        bool wDown = false;
        bool sDown = false;
        bool upKey = false;
        bool downKey = false;

        //Players
        Rectangle player1 = new Rectangle(230, 450, 10, 30);
        Rectangle player2 = new Rectangle(550, 450, 10, 30);

        //Lists for missles
        List<Rectangle> leftSide = new List<Rectangle>();
        List<Rectangle> rightSide = new List<Rectangle>();

        //Lists for speeds of missles
        List<int> rectangleSpeedLeft = new List<int>();
        List<int> rectangleSpeedRight = new List<int>();

        int playerSpeed = 5;

        //Size of missles
        int misslesX = 5;
        int misslesY = 7;

        //Scores
        int player1Score = 0;
        int player2Score = 0;

        Random randGen = new Random();

        SolidBrush whiteBrush = new SolidBrush(Color.White);

        //Used to change between screens (better way of doing this?)
        string gameState = "Waiting";

        SoundPlayer playerSounds = new SoundPlayer(Properties.Resources.PlayerSound);
        SoundPlayer explosion = new SoundPlayer(Properties.Resources.Explosion);
        SoundPlayer end = new SoundPlayer(Properties.Resources.End);
       
        
        

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //Key is up, down is false
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
            //Key is down, down is true
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
                //The space and escape key check to see what screen the game is on
                case Keys.Space:
                    if (gameState == "Waiting" || gameState == "Over")
                    {
                        GameInit();
                    }
                    break;
                case Keys.Escape:
                    if (gameState == "Waiting" || gameState == "Over")
                    {
                        Application.Exit();
                    }
                    break;
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Beginning screen
            if (gameState == "Waiting")
            {
                titleLabel.Text = "SPACE RACE";
                subTitleLabel.Text = "Press the Space Bar to get started or the Escape key to exit!";
            }
            //Drawing everything to screen
            else if (gameState == "Running")
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

                Scoring();
                

            }
            //End screen
            else if (gameState == "Over")
            {
                if (player1Score == 3)
                {
                    titleLabel.Text = "Player 1 has won!";
                    subTitleLabel.Text = "Do you want to play again?  Press the Space bar to try again\nor the Escape key to exit";

                }
                else if (player2Score == 3)
                {
                    titleLabel.Text = "Player 2 has won!";
                    subTitleLabel.Text = "Do you want to play again?  Press the Space bar to try again\nor the Escape key to exit";
                }

            }
            

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //Game loop
            PlayerMovements();

            LeftSideMovements();

            RightSideMovements();

            Collision();

            Refresh();

        }
        /// <summary>
        /// Controls the player movements
        /// </summary>
        public void PlayerMovements()
        {
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
                playerSounds.Play();
               

            }
            if (upKey == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
                playerSounds.Play();

            }
            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
                playerSounds.Play();
                ;
            }
            if (downKey == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
                playerSounds.Play();

            }

        }
        /// <summary>
        /// Controls the left side of the missles
        /// </summary>
        public void LeftSideMovements()
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
            //Limits the amount of missles on screen at a time
            if (leftSide.Count() < 15)
            {
                leftSide.Add(new Rectangle(10, y, misslesX, misslesY));
                rectangleSpeedLeft.Add(randGen.Next(5, 10));

            }

        }
        /// <summary>
        /// Controls the right side of the missles
        /// </summary>
        public void RightSideMovements()
        {
            for (int i = 0; i < rightSide.Count(); i++)
            {
                //Subtracts it for the missle to go the other way
                int x = rightSide[i].X - rectangleSpeedRight[i];

                rightSide[i] = new Rectangle(x, rightSide[i].Y, misslesX, misslesY);

                if (rightSide[i].X <= 0)
                {
                    rightSide.RemoveAt(i);
                    rectangleSpeedRight.RemoveAt(i);
                }

            }
            int y = randGen.Next(10, 400);
            //Limits the amount of missles on screen at a time
            if (rightSide.Count() < 10)
            {
                rightSide.Add(new Rectangle(790, y, misslesX, misslesY));
                rectangleSpeedRight.Add(randGen.Next(5, 10));
            }

        }
        /// <summary>
        /// Deals with collision with all objects on screen
        /// </summary>
        public void Collision()
        {
            for (int i = 0; i < leftSide.Count(); i++)
            {
                for (int i2 = 0; i2 < rightSide.Count; i2++)
                {
                    if (player1.IntersectsWith(leftSide[i]) || player1.IntersectsWith(rightSide[i2]))
                    {
                        RestartPlayer1();
                        explosion.Play();
                    }
                    else if (player2.IntersectsWith(leftSide[i]) || player2.IntersectsWith(rightSide[i2]))
                    {
                        RestartPlayer2();
                        explosion.Play();
                    }
                }
            }
            //Checks if either player got to the end
            if (player1.Y == 0)
            {
                player1Score++;

                RestartPlayer1();
                Scoring();

                
                

            }
            if (player2.Y == 0)
            {
                player2Score++;

                RestartPlayer2();
                Scoring();

               
            }
        }
        /// <summary>
        /// Restarts Player 1
        /// </summary>
        public void RestartPlayer1()
        {
            player1.X = 230;
            player1.Y = 450;
        }
        /// <summary>
        /// Restarts Player 2
        /// </summary>
        public void RestartPlayer2()
        {
            player2.X = 550;
            player2.Y = 450;
        }
        /// <summary>
        /// Keeps score of the game between the players
        /// </summary>
        public void Scoring()
        {
            if (scoreLabel1.Visible == false && scoreLabel2.Visible == false)
            {
                scoreLabel1.Visible = true;
                scoreLabel2.Visible = true;
            }

            scoreLabel1.Text = $"{player1Score}";
            scoreLabel2.Text = $"{player2Score}";

            if (player1Score == 3 || player2Score == 3)
            {
                GameOver();
                end.PlayLooping();
            }

        }
        /// <summary>
        /// Initializes the game if space bar is pressed at beginning or end
        /// </summary>
        public void GameInit()
        {
            titleLabel.Text = "";
            subTitleLabel.Text = "";

            gameState = "Running";
            gameTimer.Enabled = true;

            RestartPlayer1();
            RestartPlayer2();

            leftSide.Clear();
            rightSide.Clear();
            rectangleSpeedLeft.Clear();
            rectangleSpeedRight.Clear();

            player1Score = 0;
            player2Score = 0;

            Scoring();

            end.Stop();

            this.Focus();
            



        }
        /// <summary>
        /// What to do if either player gets to 3
        /// </summary>
        public void GameOver()
        { 
            

            scoreLabel1.Visible = false;
            scoreLabel2.Visible = false;

            gameState = "Over";

            
        }

        
    }
}
