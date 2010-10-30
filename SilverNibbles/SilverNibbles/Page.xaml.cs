using System;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;

namespace SilverNibbles
{
    [ScriptableType]
    public partial class Page : UserControl
    {
        private int currentLevel;
        private HighScore record = new HighScore();
        private int players;
        private Snake[] snake = new Snake[2];
        const int defaultSpeed = 5;
        private int startingSpeed = defaultSpeed;
        private int speed = defaultSpeed;
        private DispatcherTimer timer;
        private ScoreboardViewModel scoreData;

        public Page()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Page_Loaded);
            this.LostFocus += new RoutedEventHandler(Page_LostFocus);
            scoreData = new ScoreboardViewModel();
            scoreboard.DataContext = scoreData;
        }

        [ScriptableMember]
        public int StartingSpeed
        {
            get
            {
                return startingSpeed;
            }
            set
            {
                if (value < 1)
                    value = 1;
                if (value > 10)
                    value = 10;
                startingSpeed = value;
            }
        }

        // the timer intervals for each speed
        private readonly int[] timerIntervals = new int[] { 250, 150, 120, 100, 80, 65, 50, 35, 20, 10 };

        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                if (value < 1)
                    value = 1;
                if (value > 10)
                    value = 10;
                speed = value;
                timer.Interval = TimeSpan.FromMilliseconds(timerIntervals[speed-1]);
                SetLevelTextBlock();
            }
        }

        public int CurrentLevel
        {
            get
            {
                return currentLevel;
            }
            set
            {
                currentLevel = value;
                SetLevelTextBlock();
            }
        }

        private void SetLevelTextBlock()
        {
            scoreData.Level = CurrentLevel;
            scoreData.Speed = Speed;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            arena.OnePlayerClick += arena_OnePlayerClick;
            arena.TwoPlayerClick += arena_TwoPlayerClick;

            var persister = new HighScorePersister();
            this.record = persister.Load();
            ShowRecord();
            // just to redraw screen
            arena.DrawLevel(1);

            this.KeyDown += new System.Windows.Input.KeyEventHandler(rootElement_KeyDown);
            timer = new DispatcherTimer();
            timer.Tick += timer_Completed;
            timer.Start();

            HtmlPage.RegisterScriptableObject("SilverNibbles", this);
        }

        void arena_TwoPlayerClick(object sender, RoutedEventArgs e)
        {
            NewGame(2);
        }

        void arena_OnePlayerClick(object sender, RoutedEventArgs e)
        {
            NewGame(1);
        }

        void Page_LostFocus(object sender, RoutedEventArgs e)
        {
            if (arena.GameStatus == GameStatus.Running && e.OriginalSource == this)
            {
                Pause();
            }
        }

        [ScriptableMember]
        public void Pause()
        {
            if (arena.GameStatus == GameStatus.Running)
            {
                arena.Pause("Paused - Press Space to continue");
            }
        }

        void timer_Completed(object sender, EventArgs e)
        {
            // ... do per tick stuff here...
            if (arena.GameStatus == GameStatus.Running)
            {
                OnTimerTick(sender,e);
            }
        }

        // reset variables ready for the next level
        private void NewLevel(int levelNumber)
        {
            arena.NoNumber = true;
            arena.CurrentNumber = 1;
            CurrentLevel = levelNumber;

            // Speedup
            if (CurrentLevel > 5 && Speed < 5)
                Speed++;
            else if (CurrentLevel > 10 && Speed < 7)
                Speed++;
            else if (CurrentLevel > 15 && Speed < 9)
                Speed++;

            arena.DrawLevel(CurrentLevel);

            //Initialize Snakes;
            int initialLength = CurrentLevel > 10 ? CurrentLevel * 2 : 2;
            snake[0].Length = initialLength;
            snake[0].Alive = true;
            snake[0].Clear();
            snake[1].Length = initialLength;
            snake[1].Alive = true;
            snake[1].Clear();
            //InitColors

            // Set snake positions
            switch (CurrentLevel % 10)
            {
                case 1:
                    snake[0].CurrentPosition = new Position(49, 22);
                    snake[0].Direction = SnakeDirection.Right;

                    snake[1].CurrentPosition = new Position(29, 22);
                    snake[1].Direction = SnakeDirection.Left;
                    break;

                case 2:
                    snake[0].CurrentPosition = new Position(59, 4);
                    snake[0].Direction = SnakeDirection.Left;

                    snake[1].CurrentPosition = new Position(19, 40);
                    snake[1].Direction = SnakeDirection.Right;
                    break;

                case 3:
                    snake[0].CurrentPosition = new Position(49, 22);
                    snake[0].Direction = SnakeDirection.Up;

                    snake[1].CurrentPosition = new Position(29, 22);
                    snake[1].Direction = SnakeDirection.Down;
                    break;

                case 4:
                    snake[0].CurrentPosition = new Position(59, 4);
                    snake[0].Direction = SnakeDirection.Left;

                    snake[1].CurrentPosition = new Position(19, 40);
                    snake[1].Direction = SnakeDirection.Right;
                    break;

                case 5:
                    snake[0].CurrentPosition = new Position(49, 22);
                    snake[0].Direction = SnakeDirection.Up;

                    snake[1].CurrentPosition = new Position(29, 22);
                    snake[1].Direction = SnakeDirection.Down;
                    break;

                case 6:
                    snake[0].CurrentPosition = new Position(64, 4);
                    snake[0].Direction = SnakeDirection.Down;

                    snake[1].CurrentPosition = new Position(14, 40);
                    snake[1].Direction = SnakeDirection.Up;
                    break;

                case 7:
                    snake[0].CurrentPosition = new Position(64, 4);
                    snake[0].Direction = SnakeDirection.Down;

                    snake[1].CurrentPosition = new Position(14, 40);
                    snake[1].Direction = SnakeDirection.Up;
                    break;

                case 8:
                    snake[0].CurrentPosition = new Position(64, 4);
                    snake[0].Direction = SnakeDirection.Down;

                    snake[1].CurrentPosition = new Position(14, 40);
                    snake[1].Direction = SnakeDirection.Up;
                    break;

                case 9:
                    snake[0].CurrentPosition = new Position(74, 37);
                    snake[0].Direction = SnakeDirection.Up;

                    snake[1].CurrentPosition = new Position(4, 12);
                    snake[1].Direction = SnakeDirection.Down;
                    break;

                default:
                    snake[0].CurrentPosition = new Position(64, 4);
                    snake[0].Direction = SnakeDirection.Down;

                    snake[1].CurrentPosition = new Position(14, 40);
                    snake[1].Direction = SnakeDirection.Up;
                    break;
            }
            if (players == 1)
            {
                // to avoid head-on collisions with an invisible Jake
                snake[1].CurrentPosition = new Position(0, 0);
            }

            snake[0].DesiredDirection.Clear();
            snake[1].DesiredDirection.Clear();
        }

        // start a new game
        [ScriptableMember]
        public void NewGame(int players)
        {
            this.players = players;
            snake[0] = new Snake("Sammy", Color.FromArgb(255,255,128,0));
            snake[1] = new Snake("Jake", Color.FromArgb(255,255, 0, 255));
            arena.SetSnakes(snake);            
            Speed = StartingSpeed;
            NewLevel(1);
            UpdateScores();
            arena.Resume();
            scoreData.Players = players;
            //snakeArenaControl.Focus();
        }

        private void GameOver()
        {
            string message = "Game Over";
            if (players == 2)
            {
                if (snake[0].Lives > 0)
                {
                    message += " - " + snake[0].Name + " wins!";
                }
                else
                {
                    message += " - " + snake[1].Name + " wins!";
                }
            }
            arena.Stop(message);
        }

        private void UpdateScores()
        {
            scoreData.SammyScore = snake[0].Score;
            scoreData.SammyLives = snake[0].Lives;
            if (players == 2)
            {
                scoreData.JakeScore = snake[1].Score;
                scoreData.JakeLives = snake[1].Lives;
            }
        }

        // move the position on one
        private void OnTimerTick(object sender, EventArgs e)
        {
            if (arena.NoNumber)
            {
                arena.FindNumberLocation();
                arena.NoNumber = false;
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
                if (arena.GetCell(snake[n].CurrentPosition) == CellType.TargetNumber)
                {
                    /* Put the play sound in when we have got a good enough sound
                     * NumberSound.Stop();
                    NumberSound.Play();*/
                    snake[n].Length += (arena.CurrentNumber * ((CurrentLevel > 10) ? 8 : 4));
                    snake[n].Score += GetScore(arena.CurrentNumber);
                    UpdateScores();

                    arena.NoNumber = true;
                    arena.CurrentNumber++;
                    if (arena.CurrentNumber == 10)
                    {
                        CurrentLevel++;


                        NewLevel(CurrentLevel);
                        arena.Pause("Get Ready!\r\nPress SPACE to continue");
                    }
                }
            }

            bool playerDied = false;
            bool gameOver = false;

            for (int n = 0; n < players; n++)
            {
                CellType targetCellType = arena.GetCell(snake[n].CurrentPosition);
                //If player runs into any point, or the head of the other snake, it dies.
                if (((targetCellType != CellType.Blank) && (targetCellType != CellType.TargetNumber)) ||
                    (snake[0].CurrentPosition.Equals(snake[1].CurrentPosition)))
                {
                    
                    playerDied = true;
                    snake[n].Alive = false;
                    snake[n].Lives--;
                    UpdateScores();
                    if (snake[n].Lives <= 0)
                    {
                        gameOver = true;
                    }

                    arena.Pause(snake[n].Name + " Dies!\r\nPress SPACE to continue.");
                }
                else
                {
                    snake[n].Enqueue(snake[n].CurrentPosition);
                    arena.SetCell(snake[n].CurrentPosition.X, 
                        snake[n].CurrentPosition.Y, 
                        (n == 0) ? CellType.Sammy : CellType.Jake);
                    if (snake[n].Count > snake[n].Length)
                    {
                        Position erase = (Position)snake[n].Dequeue();
                        arena.SetCell(erase.X, erase.Y, CellType.Blank);
                    }
                }
            }

            if (gameOver)
            {
                GameOver();
                int bestScore = Math.Max(snake[0].Score, snake[1].Score);
                if (bestScore > record.Score)
                {
                    record.Score = bestScore;
                    record.Date = DateTime.Today;
                    HighScorePersister persister = new HighScorePersister();
                    persister.Save(record);
                    ShowRecord();
                }
            }
            else if (playerDied)
            {
                NewLevel(CurrentLevel);
            }
        }

        private int GetScore(int number)
        {
            return (10 + (CurrentLevel * 2) + Speed) * number;
        }

        /// <summary>
        /// Show the current record score
        /// </summary>
        private void ShowRecord()
        {
            if (record.Score > 0)
            {
                scoreData.Record = String.Format("{0} on {1}", record.Score, record.Date.ToShortDateString());
            }
            else
            {
                scoreData.Record = String.Format("0");
            }
        }

        void  rootElement_KeyDown(object sender, KeyEventArgs args)
        {
            Keys key = (Keys)args.Key;
            HandleKey(key);
        }

        void HandleKey(Keys key)
        {
            switch (arena.GameStatus)
            {
                case GameStatus.Paused:
                    switch (key)
                    {
                        case Keys.Space:
                        case Keys.Escape:
                        case Keys.Return:
                            arena.Resume();
                            break;
                    }
                    break;
                case GameStatus.Running:
                    switch (key)
                    {
                        case Keys.I:
                        case Keys.Up:
                            snake[0].EnqueueDirection(SnakeDirection.Up);
                            break;
                        case Keys.K:
                        case Keys.Down: 
                            snake[0].EnqueueDirection(SnakeDirection.Down);
                            break;
                        case Keys.J:
                        case Keys.Left:
                            snake[0].EnqueueDirection(SnakeDirection.Left);
                            break;
                        case Keys.L:
                        case Keys.Right:
                            snake[0].EnqueueDirection(SnakeDirection.Right);
                            break;
                        case Keys.W:
                            snake[1].EnqueueDirection(SnakeDirection.Up);
                            break;
                        case Keys.S:
                            snake[1].EnqueueDirection(SnakeDirection.Down);
                            break;
                        case Keys.A:
                            snake[1].EnqueueDirection(SnakeDirection.Left);
                            break;
                        case Keys.D:
                            snake[1].EnqueueDirection(SnakeDirection.Right);
                            break;
                        case Keys.Escape:
                            arena.Pause("Paused - Press Space to continue");
                            break;
                        // debug keys
                        case Keys.N0:
                            NewLevel(++CurrentLevel);
                            break;
                        case Keys.N8:
                            Speed--;
                            break;
                        case Keys.N9:
                            Speed++;
                            break;

                    }
                    break;
                case GameStatus.Stopped:
                    if (key == Keys.N1)
                    {
                        NewGame(1);
                    }
                    else if (key == Keys.N2)
                    {
                        NewGame(2);
                    }
                    break;
            }
        }
    }

    public enum CellType
    {
        Blank,
        Wall,
        Sammy,
        Jake,
        TargetNumber
    }
}
