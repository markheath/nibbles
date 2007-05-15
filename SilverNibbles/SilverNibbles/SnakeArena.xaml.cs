using System;
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

        const int Columns = 80;
        const int Rows = 50;
        PauseControl pauseControl;
        TextBlock numberTextBlock;

        Random rand;
        Position numberPosition;
        GameStatus gameStatus;

        public const string Instructions =
    "Press 1 to start a new one player game\r\n" +
    "Press 2 to start a new two player game\r\n" +
    "Sammy Keys: J=Left, K=Down, L=Right, I=Up\r\n" +
    "Jake Keys: A=Left, S=Down, D=Right, W=Up";

        public Position NumberPosition
        { 
            get { return numberPosition; } 
        }

        public SnakeArena()
        {
            System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream("SilverNibbles.SnakeArena.xaml");
            rootElement = (Canvas)this.InitializeFromXaml(new System.IO.StreamReader(s).ReadToEnd());
            this.Width = Columns * DefaultBlockSize;
            this.Height = Rows * DefaultBlockSize;
            CurrentNumber = -1;


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
                "SilverNibbles 1.0 by Mark Heath\r\n" +
                Instructions;

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
                numberPosition.X = rand.Next(1, 79);
                numberPosition.Y = rand.Next(3, 49);
            } while ((arena[numberPosition.X, numberPosition.Y].CellType != CellType.Blank) ||
                (arena[numberPosition.X, numberPosition.Y + 1].CellType != CellType.Blank));
            //arena[numberPosition.X, numberPosition.Y].CellType = CellType.TargetNumber;
            //arena[numberPosition.X, numberPosition.Y + 1].CellType = CellType.TargetNumber;

            numberTextBlock.SetValue<double>(Canvas.LeftProperty, numberPosition.X * DefaultBlockSize);
            // slightly adjust y value to make number sit nicely over the two squares
            numberTextBlock.SetValue<double>(Canvas.TopProperty, (numberPosition.Y * DefaultBlockSize) - 3);
            numberTextBlock.Text = CurrentNumber.ToString();
            NoNumber = false;
        }

        public int CurrentNumber { get; set; }
        private bool noNumber;
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
            for (int x = 0; x < 80; x++)
            {
                for (int y = 2; y < 50; y++)
                {
                    if (x == 0 || x == 79 || y == 2 || y == 49)
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
                        arena[i, 25].CellType = CellType.Wall;
                    }
                    break;

                case 3:
                    for (int i = 9; i < 40; i++)
                    {
                        arena[19, i].CellType = CellType.Wall;
                        arena[59, i].CellType = CellType.Wall;
                    }
                    break;

                case 4:
                    for (int i = 3; i < 30; i++)
                    {
                        arena[19, i].CellType = CellType.Wall;
                        arena[59, 51 - i].CellType = CellType.Wall;
                    }
                    for (int i = 1; i < 40; i++)
                    {
                        arena[i, 37].CellType = CellType.Wall;
                        arena[79 - i, 14].CellType = CellType.Wall;
                    }
                    break;

                case 5:
                    for (int i = 12; i < 39; i++)
                    {
                        arena[20, i].CellType = CellType.Wall;
                        arena[58, i].CellType = CellType.Wall;
                    }
                    for (int i = 22; i < 57; i++)
                    {
                        arena[i, 10].CellType = CellType.Wall;
                        arena[i, 40].CellType = CellType.Wall;
                    }
                    break;

                case 6:
                    for (int i = 3; i < 49; i++)
                    {
                        if ((i > 29) || (i < 22))
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
                    for (int i = 3; i < 49; i += 2)
                    {
                        arena[39, i].CellType = CellType.Wall;
                    }
                    break;

                case 8:
                    for (int i = 3; i < 40; i++)
                    {
                        arena[9, i].CellType = CellType.Wall;
                        arena[19, 51 - i].CellType = CellType.Wall;
                        arena[29, i].CellType = CellType.Wall;
                        arena[39, 51 - i].CellType = CellType.Wall;
                        arena[49, i].CellType = CellType.Wall;
                        arena[59, 51 - i].CellType = CellType.Wall;
                        arena[69, i].CellType = CellType.Wall;
                    }
                    break;

                case 9:
                    for (int i = 5; i < 47; i++)
                    {
                        arena[i, i].CellType = CellType.Wall;
                        arena[i + 28, i].CellType = CellType.Wall;
                    }
                    break;

                default:
                    for (int i = 3; i < 49; i += 2)
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
