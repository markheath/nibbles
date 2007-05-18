using System;
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
    public class SnakeArena : Control
    {
        Canvas rootElement;

        Cell[,] arena;
        const int DefaultBlockSize = 8;
        private bool noNumber;
        private int currentNumber;
        const int Columns = 80;
        const int Rows = 48;
        PauseControl pauseControl;
        TextBlock numberTextBlock;

        Random rand;
        Position numberPosition;
        GameStatus gameStatus;
        List<Snake> snakes;

        public const string Instructions =
    "Press 1 to start a new one player game\r\n" +
    "Press 2 to start a new two player game\r\n" +
    "Sammy Keys: J=Left, K=Down, L=Right, I=Up\r\n" +
    "Jake Keys: A=Left, S=Down, D=Right, W=Up";

        /*public Position NumberPosition
        { 
            get { return numberPosition; } 
        }*/

        public SnakeArena()
        {
            System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream("SilverNibbles.SnakeArena.xaml");
            rootElement = (Canvas)this.InitializeFromXaml(new System.IO.StreamReader(s).ReadToEnd());
            this.Width = Columns * DefaultBlockSize;
            this.Height = Rows * DefaultBlockSize;
            CurrentNumber = -1;
            this.snakes = new List<Snake>();

            numberPosition = new Position();
            rand = new Random();
            arena = new Cell[Columns, Rows];

            // Create the array of rectangles
            for (int col = 0; col < Columns; col++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    Rectangle rect = new Rectangle();
                    rect.SetValue<double>(Canvas.LeftProperty, col * DefaultBlockSize);
                    rect.SetValue<double>(Canvas.TopProperty, row * DefaultBlockSize);
                    rect.Width = DefaultBlockSize;
                    rect.Height = DefaultBlockSize;
                    arena[col, row] = new Cell(CellType.Sammy, rect);
                    rootElement.Children.Add(rect);
                }
            }

            numberTextBlock = new TextBlock();
            rootElement.Children.Add(numberTextBlock);

            NoNumber = true;

            pauseControl = new PauseControl();
            pauseControl.Width = 380;
            pauseControl.Height = 140;
            pauseControl.SetValue<double>(Canvas.LeftProperty, (this.Width - pauseControl.Width) / 2);
            pauseControl.SetValue<double>(Canvas.TopProperty, (this.Height - pauseControl.Height) / 2);
            rootElement.Children.Add(pauseControl);
            
            pauseControl.Text =
                String.Format("SilverNibbles 1.03 by Mark Heath\r\n{0}",
                Instructions);
            
        }

        public void SetSnakes(IEnumerable<Snake> newSnakes)
        {
            foreach(Snake snake in snakes)
            {
                rootElement.Children.Remove(snake.Graphics);
            }
            snakes.Clear();
            foreach (Snake snake in newSnakes)
            {
                rootElement.Children.Add(snake.Graphics);
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
            pauseControl.Visibility = Visibility.Visible;
            pauseControl.Text = message;
        }

        public void Stop(string message)
        {
            gameStatus = GameStatus.Stopped;
            pauseControl.Visibility = Visibility.Visible;
            pauseControl.Text = message;
        }


        public void Resume()
        {
            gameStatus = GameStatus.Running;
            pauseControl.Visibility = Visibility.Collapsed;
        }

        public void FindNumberLocation()
        {


            do
            {
                numberPosition.X = rand.Next(1, Columns - 1);
                numberPosition.Y = rand.Next(1, Rows - 1);
            } while ((arena[numberPosition.X, numberPosition.Y].CellType != CellType.Blank) ||
                (arena[numberPosition.X, numberPosition.Y + 1].CellType != CellType.Blank));
            arena[numberPosition.X, numberPosition.Y].CellType = CellType.TargetNumber;
            arena[numberPosition.X, numberPosition.Y + 1].CellType = CellType.TargetNumber;

            numberTextBlock.SetValue<double>(Canvas.LeftProperty, numberPosition.X * DefaultBlockSize);
            // slightly adjust y value to make number sit nicely over the two squares
            numberTextBlock.SetValue<double>(Canvas.TopProperty, (numberPosition.Y * DefaultBlockSize) - 3);
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
                    arena[numberPosition.X, numberPosition.Y].CellType = CellType.Blank;
                    arena[numberPosition.X, numberPosition.Y + 1].CellType = CellType.Blank;
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
            return arena[point.X, point.Y].CellType;
        }


        public void SetCell(int x, int y, CellType type)
        {
            Cell cell = arena[x, y];
            cell.CellType = type;
        }

        public void DrawLevel(int level)
        {
            // 1. blank the grid
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    if (x == 0 || x == Columns - 1 || y == 0 || y == Rows - 1)
                    {
                        arena[x, y].CellType = CellType.Wall;
                    }
                    else
                    {
                        arena[x, y].CellType = CellType.Blank;
                    }
                }
            }

            switch (level)
            {
                case 1:
                    break;

                case 2:
                    for (int i = 19; i < 60; i++)
                    {
                        arena[i, 23].CellType = CellType.Wall;
                    }
                    break;

                case 3:
                    for (int i = 7; i < 38; i++)
                    {
                        arena[19, i].CellType = CellType.Wall;
                        arena[59, i].CellType = CellType.Wall;
                    }
                    break;

                case 4:
                    for (int i = 1; i < 28; i++)
                    {
                        arena[19, i].CellType = CellType.Wall;
                        arena[59, 47 - i].CellType = CellType.Wall;
                    }
                    for (int i = 1; i < 40; i++)
                    {
                        arena[i, 35].CellType = CellType.Wall;
                        arena[79 - i, 12].CellType = CellType.Wall;
                    }
                    break;

                case 5:
                    for (int i = 10; i < 37; i++)
                    {
                        arena[20, i].CellType = CellType.Wall;
                        arena[58, i].CellType = CellType.Wall;
                    }
                    for (int i = 22; i < 57; i++)
                    {
                        arena[i, 8].CellType = CellType.Wall;
                        arena[i, 38].CellType = CellType.Wall;
                    }
                    break;

                case 6:
                    for (int i = 1; i < 47; i++)
                    {
                        if ((i > 27) || (i < 20))
                        {
                            arena[9, i].CellType = CellType.Wall;
                            arena[19, i].CellType = CellType.Wall;
                            arena[29, i].CellType = CellType.Wall;
                            arena[39, i].CellType = CellType.Wall;
                            arena[49, i].CellType = CellType.Wall;
                            arena[59, i].CellType = CellType.Wall;
                            arena[69, i].CellType = CellType.Wall;
                        }
                    }
                    break;

                case 7:
                    for (int i = 1; i < 47; i += 2)
                    {
                        arena[39, i].CellType = CellType.Wall;
                    }
                    break;

                case 8:
                    for (int i = 1; i < 38; i++)
                    {
                        arena[9, i].CellType = CellType.Wall;
                        arena[19, 47 - i].CellType = CellType.Wall;
                        arena[29, i].CellType = CellType.Wall;
                        arena[39, 47 - i].CellType = CellType.Wall;
                        arena[49, i].CellType = CellType.Wall;
                        arena[59, 47 - i].CellType = CellType.Wall;
                        arena[69, i].CellType = CellType.Wall;
                    }
                    break;

                case 9:
                    for (int i = 5; i < 47; i++)
                    {
                        arena[i, i-2].CellType = CellType.Wall;
                        arena[i + 28, i-2].CellType = CellType.Wall;
                    }
                    break;

                default:
                    for (int i = 1; i < 47; i += 2)
                    {
                        arena[9, i].CellType = CellType.Wall;
                        arena[19, i + 1].CellType = CellType.Wall;
                        arena[29, i].CellType = CellType.Wall;
                        arena[39, i + 1].CellType = CellType.Wall;
                        arena[49, i].CellType = CellType.Wall;
                        arena[59, i + 1].CellType = CellType.Wall;
                        arena[69, i].CellType = CellType.Wall;
                    }
                    break;
            }

        }

        



    }
}
