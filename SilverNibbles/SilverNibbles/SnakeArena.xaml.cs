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
            arena = new CellType[Columns, Rows];

            levelControl = new LevelControl();
            levelControl.Scale = DefaultBlockSize;
            /*ScaleTransform levelTransform = new ScaleTransform();
            levelTransform.ScaleX = DefaultBlockSize;
            levelTransform.ScaleY = DefaultBlockSize;

            levelControl.RenderTransform = levelTransform;*/
            rootElement.Children.Add(levelControl);

            numberTextBlock = new TextBlock();
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
            pauseControl.Width = 380;
            pauseControl.Height = 140;
            pauseControl.SetValue<double>(Canvas.LeftProperty, (this.Width - pauseControl.Width) / 2);
            pauseControl.SetValue<double>(Canvas.TopProperty, (this.Height - pauseControl.Height) / 2);
            rootElement.Children.Add(pauseControl);
            
            pauseControl.Text =
                String.Format("SilverNibbles 1.10 by Mark Heath\r\n{0}",                
                Instructions);
            
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
            pauseControl.Appear(); // Visibility = Visibility.Visible;
            pauseControl.Text = message;
        }

        public void Stop(string message)
        {
            gameStatus = GameStatus.Stopped;
            pauseControl.Appear(); // Visibility = Visibility.Visible;
            pauseControl.Text = message;
        }


        public void Resume()
        {
            gameStatus = GameStatus.Running;
            pauseControl.Disappear(); //.Visibility = Visibility.Collapsed;
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
