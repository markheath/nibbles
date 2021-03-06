﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverNibbles
{
    public partial class SnakeArena : UserControl
    {
        public event RoutedEventHandler OnePlayerClick
        {
            add { pauseControl.OnePlayerClick += value; }
            remove { pauseControl.OnePlayerClick -= value; }
        }

        public event RoutedEventHandler TwoPlayerClick
        {
            add { pauseControl.TwoPlayerClick += value; }
            remove { pauseControl.TwoPlayerClick -= value; }
        }
        CellType[,] arena;
        const int DefaultBlockSize = 8;
        private bool noNumber;
        private int currentNumber;
        const int Columns = 80;
        const int Rows = 48;
        LevelControl levelControl;
        PauseControl pauseControl;
        TextBlock numberTextBlock;
        Canvas snakeCanvas;

        Random rand;
        Position numberPosition;
        GameStatus gameStatus;
        List<Snake> snakes;


        public SnakeArena()
        {
            InitializeComponent();
            this.Width = Columns * DefaultBlockSize;
            this.Height = Rows * DefaultBlockSize;
            CurrentNumber = -1;
            this.snakes = new List<Snake>();

            numberPosition = new Position();
            rand = new Random();
            arena = new CellType[Columns, Rows];

            levelControl = new LevelControl();
            levelControl.Scale = DefaultBlockSize;

            rootElement.Children.Add(levelControl);

            numberTextBlock = new TextBlock();
            numberTextBlock.FontSize = 14;
            rootElement.Children.Add(numberTextBlock);

            snakeCanvas = new Canvas();
            snakeCanvas.Width = this.Width;
            snakeCanvas.Height = this.Height;
            // snake canvas has no fill - it is transparent
            ScaleTransform transform = new ScaleTransform();
            transform.ScaleX = DefaultBlockSize;
            transform.ScaleY = DefaultBlockSize;
            snakeCanvas.RenderTransform = transform;
            rootElement.Children.Add(snakeCanvas);

            NoNumber = true;

            pauseControl = new PauseControl();
            pauseControl.SetValue(Canvas.LeftProperty, (this.Width - pauseControl.Width) / 2);
            pauseControl.SetValue(Canvas.TopProperty, (this.Height - pauseControl.Height) / 2);
            rootElement.Children.Add(pauseControl);            
            pauseControl.Text = "by Mark Heath\r\nhttp://nibbles.codeplex.com";
        }

        public void SetSnakes(IEnumerable<Snake> newSnakes)
        {
            snakeCanvas.Children.Clear();
            snakes.Clear();
            foreach (Snake snake in newSnakes)
            {
                snakeCanvas.Children.Add(snake.Graphics);
                snakes.Add(snake);
            }
        }

        public GameStatus GameStatus
        {
            get { return gameStatus; }
        }

        public void Pause(string message)
        {
            gameStatus = GameStatus.Paused;
            pauseControl.Appear(true);
            pauseControl.Text = message;
        }

        public void Stop(string message)
        {
            gameStatus = GameStatus.Stopped;
            pauseControl.Appear(false);
            pauseControl.Text = message;
        }

        public void Resume()
        {
            gameStatus = GameStatus.Running;
            pauseControl.Disappear();
        }

        public void FindNumberLocation()
        {
            do
            {
                numberPosition.X = rand.Next(1, Columns - 1);
                numberPosition.Y = rand.Next(1, Rows - 1);
            } while ((arena[numberPosition.X, numberPosition.Y] != CellType.Blank) ||
                (arena[numberPosition.X, numberPosition.Y + 1] != CellType.Blank));
            arena[numberPosition.X, numberPosition.Y] = CellType.TargetNumber;
            arena[numberPosition.X, numberPosition.Y + 1] = CellType.TargetNumber;

            numberTextBlock.SetValue(Canvas.LeftProperty, (double) (numberPosition.X * DefaultBlockSize));
            // slightly adjust y value to make number sit nicely over the two squares
            numberTextBlock.SetValue(Canvas.TopProperty, (double) ((numberPosition.Y * DefaultBlockSize) - 3));
            numberTextBlock.Text = CurrentNumber.ToString();
            NoNumber = false;
        }

        public int CurrentNumber 
        {
            get
            {
                return currentNumber;
            }
            set
            {
                currentNumber = value;
                if(currentNumber > 1)
                {
                    // clean up the position of the old number
                    arena[numberPosition.X, numberPosition.Y] = CellType.Blank;
                    arena[numberPosition.X, numberPosition.Y + 1] = CellType.Blank;
                }
            }
        }

        public bool NoNumber 
        {
            get
            {
                return noNumber;
            }
            set
            {
                noNumber = value;
                numberTextBlock.Visibility = noNumber ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public CellType GetCell(Position point)
        {
            return arena[point.X, point.Y];
        }


        public void SetCell(int x, int y, CellType type)
        {
            arena[x, y] = type;
        }

        public void DrawLevel(int level)
        {
            levelControl.DrawLevel(level,arena);
        }
    }
}
