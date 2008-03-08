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
    public partial class LevelControl : UserControl
    {
        Color wallColor = Color.FromArgb(255, 40, 100, 160);
        Color backgroundColor = Color.FromArgb(255, 173, 216, 230); // light blue
        const int Columns = 80;
        const int Rows = 48;
        
        public LevelControl()
        {
            InitializeComponent();
        }

        public double Scale
        {
            get 
            { 
                return scale.ScaleX; 
            }
            set
            {
                scale.ScaleX = value;
                scale.ScaleY = value;
            }
        }

        private void AddLine(int X1, int Y1, int X2, int Y2, CellType[,] arena)
        {
            Line line = new Line();
            line.X1 = X1;
            line.X2 = X2;
            line.Y1 = Y1;
            line.Y2 = Y2;
            line.StrokeThickness = 1;
            line.Stroke = new SolidColorBrush(wallColor);
            line.StrokeEndLineCap = PenLineCap.Round;
            line.StrokeStartLineCap = PenLineCap.Round;
            wallCanvas.Children.Add(line);
            if (X1 == X2)
            {
                for (int y = Y1; y <= Y2; y++)
                    arena[X1, y] = CellType.Wall;
            }
            else if (Y1 == Y2)
            {
                for (int x = X1; x <= X2; x++)
                    arena[x, Y1] = CellType.Wall;
            }
            else
            {
                throw new ArgumentException("Line must be horizontal or vertical");
            }

        }

        public void DrawLevel(int level, CellType[,] arena)
        {
            wallCanvas.Children.Clear();


            // 1. blank the grid
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    if (x == 0 || x == Columns - 1 || y == 0 || y == Rows - 1)
                    {
                        arena[x, y] = CellType.Wall;
                    }
                    else
                    {
                        arena[x, y] = CellType.Blank;
                    }
                }
            }
            /*
            if (level == 1)
            {
                backgroundRect.Fill = new SolidColorBrush(backgroundColor);
            }
            else if (level == 11)
            {
                // Reward the player with a new colour background (how exciting!)
                backgroundRect.Fill = new SolidColorBrush(Color.FromArgb(255,238,221,130));
            }*/

            switch (level % 10)
            {
                case 1:
                    break;

                case 2:
                    // one horizontal line
                    AddLine(19, 23, 59, 23, arena);
                    break;

                case 3:
                    // two vertical lines
                    AddLine(19, 7, 19, 37, arena);
                    AddLine(59, 7, 59, 37, arena);
                    break;

                case 4:
                    // one line coming out of each wall
                    AddLine(19, 0, 19, 27, arena);
                    AddLine(59, 20, 59, 47, arena);

                    AddLine(0, 35, 39, 35, arena);
                    AddLine(40, 12, 79, 12, arena);

                    break;

                case 5:
                    // a square with gaps at the corners
                    AddLine(20, 10, 20, 36, arena);
                    AddLine(58, 10, 58, 36, arena);

                    AddLine(22, 8, 56, 8, arena);
                    AddLine(22, 38, 56, 38, arena);
                    break;

                case 6:
                    // multiple vertical lines with a gap through the middle
                    for (int x = 9; x < 70; x += 10)
                    {
                        AddLine(x, 0, x, 19, arena);
                        AddLine(x, 28, x, 47, arena);
                    }
                    break;

                case 7:
                    // dotted vertical line down the middle
                    for (int y = 1; y < 47; y += 2)
                    {
                        AddLine(39, y, 39, y, arena);
                    }
                    break;

                case 8:
                    // alternating vertical lines some from top, some from bottom

                    bool down = true;
                    for (int x = 9; x < 70; x += 10)
                    {
                        if (down)
                        {
                            AddLine(x, 0, x, 37, arena);
                        }
                        else
                        {
                            AddLine(x, 10, x, 47, arena);

                        }
                        down = !down;
                    }
                    break;

                case 9:
                    // two diagonal lines
                    for (int i = 5; i < 47; i++)
                    {
                        AddLine(i, i - 2, i, i - 2, arena);
                        AddLine(i + 28, i - 2, i + 28, i - 2, arena);
                    }
                    break;

                default: // level 10 and above
                    // multiple vertical dotted lines
                    bool odd = true;
                    for (int x = 9; x < 70; x += 10)
                    {
                        for (int y = 1; y < 47; y += 2)
                        {
                            int ypos = (odd) ? y : y + 1;
                            AddLine(x, ypos, x, ypos, arena);
                        }
                        odd = !odd;
                    }
                    break;
            }
        }
    }
}
