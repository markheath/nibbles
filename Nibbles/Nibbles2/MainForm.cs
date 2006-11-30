using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MarkHeath.Nibbles
{
    public partial class MainForm : Form
    {
        private int currentLevel;
        private int recordScore;
        private string recordName;
        private int players;
        private Snake[] snake = new Snake[2];

        public MainForm()
        {
            InitializeComponent();

            recordScore = (int)Application.UserAppDataRegistry.GetValue("Score", (int)0);
            recordName = (string)Application.UserAppDataRegistry.GetValue("Name", "nobody");

            ShowRecord();

            // just to redraw screen
            snakeArenaControl.DrawLevel(1);
        }

        // 1 player button clicked
        private void OnOnePlayer(object sender, EventArgs e)
        {
            players = 1;
            labelJake.Text = "";
            NewGame();
        }

        // 2 player button clicked
        private void OnTwoPlayer(object sender, EventArgs e)
        {
            players = 2;
            NewGame();
        }

        private void OnFilePause(object sender, EventArgs e)
        {
            // check that a game is running
            if (buttonOnePlayer.Enabled == false)
            {
                snakeArenaControl.Paused = !snakeArenaControl.Paused;
                timer1.Enabled = !snakeArenaControl.Paused;
            }
        }

        private void OnFileOptions(object sender, EventArgs e)
        {
            // TODO: implement options
        }

        private void OnFileExit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnHelpAbout(object sender, EventArgs e)
        {
            // TODO: about screen
        }

        private void OnHelpContents(object sender, EventArgs e)
        {
            // TODO: launch help
        }


        // start a new game
        private void NewGame()
        {
            snake[0] = new Snake("Sammy", labelSammy.ForeColor);
            snake[1] = new Snake("Jake", labelJake.ForeColor);
            buttonOnePlayer.Enabled = false;
            buttonTwoPlayer.Enabled = false;
            currentLevel = 0;
            NewLevel();
            UpdateScores();
            timer1.Start();
            //snakeArenaControl.Focus();
        }

        private void GameOver()
        {
            timer1.Stop();
            buttonOnePlayer.Enabled = true;
            buttonTwoPlayer.Enabled = true;
            buttonOnePlayer.Focus();
        }


        private void UpdateScores()
        {
            labelSammy.Text = String.Format("Sammy: Lives {0}: Score {1}", snake[0].Lives, snake[0].Score);
            if (players == 2)
            {
                labelJake.Text = String.Format("Jake: Lives {0}: Score {1}", snake[1].Lives, snake[1].Score);
            }
        }



        // move the position on one
        private void OnTimerTick(object sender, EventArgs e)
        {
            if (snakeArenaControl.NoNumber)
            {
                snakeArenaControl.FindNumberLocation();
                snakeArenaControl.NoNumber = false;
            }

            for (int n = 0; n < players; n++)
            {
                if (snake[n].DesiredDirection.Count > 0)
                {
                    SnakeDirection d = (SnakeDirection)snake[n].DesiredDirection.Dequeue();
                    switch (d)
                    {
                        case SnakeDirection.Up:
                            if (snake[n].Direction != SnakeDirection.Down)
                            {
                                snake[n].Direction = d;
                            }
                            break;
                        case SnakeDirection.Down:
                            if (snake[n].Direction != SnakeDirection.Up)
                            {
                                snake[n].Direction = d;
                            }
                            break;
                        case SnakeDirection.Left:
                            if (snake[n].Direction != SnakeDirection.Right)
                            {
                                snake[n].Direction = d;
                            }
                            break;
                        case SnakeDirection.Right:
                            if (snake[n].Direction != SnakeDirection.Left)
                            {
                                snake[n].Direction = d;
                            }
                            break;
                    }
                }
            }

            for (int n = 0; n < players; n++)
            {
                snake[n].Move();

                // see if we collided with the number
                if ((snakeArenaControl.NumberPosition.X == snake[n].CurrentPosition.X) &&
                   ((snakeArenaControl.NumberPosition.Y == snake[n].CurrentPosition.Y) ||
                    (snakeArenaControl.NumberPosition.Y + 1 == snake[n].CurrentPosition.Y)))
                {
                    snake[n].Length += (snakeArenaControl.CurrentNumber * 4);

                    // clear up the other half number
                    snakeArenaControl.SetCell(snakeArenaControl.NumberPosition.X, (snakeArenaControl.NumberPosition.Y == snake[n].CurrentPosition.Y) ? snakeArenaControl.NumberPosition.Y + 1 : snakeArenaControl.NumberPosition.Y, CellType.Blank);

                    snake[n].Score += snakeArenaControl.CurrentNumber;
                    UpdateScores();

                    snakeArenaControl.NoNumber = true;
                    snakeArenaControl.CurrentNumber++;
                    if (snakeArenaControl.CurrentNumber == 10)
                    {
                        NewLevel();
                        // TODO: speedup
                    }
                }
            }

            bool playerDied = false;
            bool gameOver = false;

            for (int n = 0; n < players; n++)
            {
                //If player runs into any point, or the head of the other snake, it dies.
                if ((snakeArenaControl.GetCell(snake[n].CurrentPosition) != CellType.Blank) ||
                    (snake[0].CurrentPosition == snake[1].CurrentPosition))
                {
                    timer1.Stop();
                    MessageBox.Show(snake[n].Name + " Dies!");

                    playerDied = true;
                    snake[n].Alive = false;
                    snake[n].Lives--;
                    UpdateScores();
                    if (snake[n].Lives <= 0)
                    {
                        gameOver = true;
                    }
                }
                else
                {
                    snake[n].Body.Enqueue(snake[n].CurrentPosition);
                    snakeArenaControl.SetCell(snake[n].CurrentPosition.X, snake[n].CurrentPosition.Y, (n == 0) ? CellType.Sammy : CellType.Jake);
                    if (snake[n].Body.Count > snake[n].Length)
                    {
                        Point erase = (Point)snake[n].Body.Dequeue();
                        snakeArenaControl.SetCell(erase.X, erase.Y, CellType.Blank);
                    }
                }
            }

            if (gameOver)
            {
                GameOver();
                MessageBox.Show("Game Over");
                int bestScore = Math.Max(snake[0].Score, snake[1].Score);

                if (bestScore > recordScore)
                {
                    recordScore = bestScore;
                    HighScoreForm highScoreForm = new HighScoreForm();
                    highScoreForm.UserName = recordName;
                    highScoreForm.ShowDialog();
                    recordName = highScoreForm.UserName;
                    ShowRecord();

                }
            }
            else if (playerDied)
            {
                currentLevel--;
                NewLevel();
                timer1.Start();
            }

        }

        // reset variables ready for the next level
        private void NewLevel()
        {
            snakeArenaControl.NoNumber = true;
            snakeArenaControl.CurrentNumber = 1;
            currentLevel = currentLevel + 1;
            labelLevel.Text = String.Format("Level {0}", currentLevel);

            snakeArenaControl.DrawLevel(currentLevel);

            //Initialize Snakes;
            snake[0].Length = 2;
            snake[0].Alive = true;
            snake[0].Body.Clear();
            snake[1].Length = 2;
            snake[1].Alive = true;
            snake[1].Body.Clear();
            //InitColors

            // Set snake positions
            switch (currentLevel)
            {
                case 1:
                    snake[0].CurrentPosition = new Point(49, 24);
                    snake[0].Direction = SnakeDirection.Right;

                    snake[1].CurrentPosition = new Point(29, 24);
                    snake[1].Direction = SnakeDirection.Left;
                    break;

                case 2:
                    snake[0].CurrentPosition = new Point(59, 6);
                    snake[0].Direction = SnakeDirection.Left;

                    snake[1].CurrentPosition = new Point(19, 42);
                    snake[1].Direction = SnakeDirection.Right;
                    break;

                case 3:
                    snake[0].CurrentPosition = new Point(49, 24);
                    snake[0].Direction = SnakeDirection.Up;

                    snake[1].CurrentPosition = new Point(29, 24);
                    snake[1].Direction = SnakeDirection.Down;
                    break;

                case 4:
                    snake[0].CurrentPosition = new Point(59, 6);
                    snake[0].Direction = SnakeDirection.Left;

                    snake[1].CurrentPosition = new Point(19, 42);
                    snake[1].Direction = SnakeDirection.Right;
                    break;

                case 5:
                    snake[0].CurrentPosition = new Point(49, 24);
                    snake[0].Direction = SnakeDirection.Up;

                    snake[1].CurrentPosition = new Point(29, 24);
                    snake[1].Direction = SnakeDirection.Down;
                    break;

                case 6:
                    snake[0].CurrentPosition = new Point(64, 6);
                    snake[0].Direction = SnakeDirection.Down;

                    snake[1].CurrentPosition = new Point(14, 42);
                    snake[1].Direction = SnakeDirection.Up;
                    break;

                case 7:
                    snake[0].CurrentPosition = new Point(64, 6);
                    snake[0].Direction = SnakeDirection.Down;

                    snake[1].CurrentPosition = new Point(14, 42);
                    snake[1].Direction = SnakeDirection.Up;
                    break;

                case 8:
                    snake[0].CurrentPosition = new Point(64, 6);
                    snake[0].Direction = SnakeDirection.Down;

                    snake[1].CurrentPosition = new Point(14, 42);
                    snake[1].Direction = SnakeDirection.Up;
                    break;

                case 9:
                    snake[0].CurrentPosition = new Point(74, 39);
                    snake[0].Direction = SnakeDirection.Up;

                    snake[1].CurrentPosition = new Point(4, 14);
                    snake[1].Direction = SnakeDirection.Down;
                    break;

                default:
                    snake[0].CurrentPosition = new Point(64, 6);
                    snake[0].Direction = SnakeDirection.Down;

                    snake[1].CurrentPosition = new Point(14, 42);
                    snake[1].Direction = SnakeDirection.Up;
                    break;
            }
            if (players == 1)
            {
                // to avoid head-on collisions with an invisible Jake
                snake[1].CurrentPosition = new Point(0, 0);
            }

            snake[0].DesiredDirection.Clear();
            snake[1].DesiredDirection.Clear();
        }

        // save the current record in the registry and update the label
        private void ShowRecord()
        {
            Application.UserAppDataRegistry.SetValue("Score", recordScore);
            Application.UserAppDataRegistry.SetValue("Name", recordName);
            labelRecord.Text = String.Format("High Score: {0} by {1}", recordScore, recordName);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // stop the game when the application closes
            timer1.Stop();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //TODO: pause
            //Sammy - arrow keys
            //Jake - ASDW
            switch (e.KeyCode)
            {
                case Keys.Up:
                    snake[0].DesiredDirection.Enqueue(SnakeDirection.Up);
                    break;
                case Keys.Down:
                    snake[0].DesiredDirection.Enqueue(SnakeDirection.Down);
                    break;
                case Keys.Left:
                    snake[0].DesiredDirection.Enqueue(SnakeDirection.Left);
                    break;
                case Keys.Right:
                    snake[0].DesiredDirection.Enqueue(SnakeDirection.Right);
                    break;
                case Keys.W:
                    snake[1].DesiredDirection.Enqueue(SnakeDirection.Up);
                    break;
                case Keys.S:
                    snake[1].DesiredDirection.Enqueue(SnakeDirection.Down);
                    break;
                case Keys.A:
                    snake[1].DesiredDirection.Enqueue(SnakeDirection.Left);
                    break;
                case Keys.D:
                    snake[1].DesiredDirection.Enqueue(SnakeDirection.Right);
                    break;
            }
        }

    }
}