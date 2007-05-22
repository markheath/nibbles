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
    public class LevelControl : Control
    {
        Canvas rootElement;
        Canvas wallCanvas;
        Color wallColor = Color.FromRgb(40, 100, 160);
        Color backgroundColor = Color.FromRgb(173, 216, 230); // light blue
        const int Columns = 80;
        const int Rows = 48;
        ScaleTransform scale;
        
        public LevelControl()
        {
            System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream("SilverNibbles.LevelControl.xaml");
            rootElement = (Canvas)this.InitializeFromXaml(new System.IO.StreamReader(s).ReadToEnd());
            wallCanvas = new Canvas();

            // outer border on all levels
            Rectangle rectangle = new Rectangle();
            rectangle.StrokeThickness = 1;
            rectangle.Stroke = new SolidColorBrush(wallColor);
            rectangle.StrokeLineJoin = PenLineJoin.Round;
            rectangle.Height = Rows;
            rectangle.Width = Columns;
            rectangle.Fill = new SolidColorBrush(backgroundColor);
            rootElement.Children.Add(rectangle);

            scale = (ScaleTransform)rootElement.FindName("ScaleTransform");

            TranslateTransform translate = new TranslateTransform();
            translate.X = 0.5;
            translate.Y = 0.5;
            wallCanvas.RenderTransform = translate;
            rootElement.Children.Add(wallCanvas);
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

            switch (level)
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
                    /*for (int y = 1; y < 28; y++)
                    {
                        arena[19, y] = CellType.Wall;
                        arena[59, 47 - y] = CellType.Wall;
                    }*/
                    AddLine(19, 0, 19, 27, arena);
                    AddLine(59, 20, 59, 47, arena);

                    /*for (int x = 1; x < 40; x++)
                    {
                        arena[x, 35] = CellType.Wall;
                        arena[79 - x, 12] = CellType.Wall;
                    }*/
                    AddLine(0, 35, 39, 35, arena);
                    AddLine(40, 12, 79, 12, arena);

                    break;

                case 5:
                    // a square with gaps at the corners
                    /*for (int y = 10; y < 37; y++)
                    {
                        arena[20, y] = CellType.Wall;
                        arena[58, y] = CellType.Wall;
                    }*/
                    AddLine(20, 10, 20, 36, arena);
                    AddLine(58, 10, 58, 36, arena);

                    /*for (int x = 22; x < 57; x++)
                    {
                        arena[x, 8] = CellType.Wall;
                        arena[x, 38] = CellType.Wall;
                    }*/
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

                    /*
                    for (int y = 1; y < 47; y++)
                    {
                        if ((y > 27) || (y < 20))
                        {
                            arena[9, y] = CellType.Wall;
                            arena[19, y] = CellType.Wall;
                            arena[29, y] = CellType.Wall;
                            arena[39, y] = CellType.Wall;
                            arena[49, y] = CellType.Wall;
                            arena[59, y] = CellType.Wall;
                            arena[69, y] = CellType.Wall;
                        }
                    }*/
                    break;

                case 7:
                    // dotted vertical line down the middle
                    for (int y = 1; y < 47; y += 2)
                    {
                        AddLine(39, y, 39, y, arena);
                        //arena[39, i] = CellType.Wall;
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
                    /*
                    for (int y = 1; y < 38; y++)
                    {
                        arena[9, y] = CellType.Wall;
                        arena[19, 47 - y] = CellType.Wall;
                        arena[29, y] = CellType.Wall;
                        arena[39, 47 - y] = CellType.Wall;
                        arena[49, y] = CellType.Wall;
                        arena[59, 47 - y] = CellType.Wall;
                        arena[69, y] = CellType.Wall;
                    }*/
                    break;

                case 9:
                    // two diagonal lines
                    for (int i = 5; i < 47; i++)
                    {
                        AddLine(i, i - 2, i, i - 2, arena);
                        AddLine(i + 28, i - 2, i + 28, i - 2, arena);
                        //arena[i, i - 2] = CellType.Wall;
                        //arena[i + 28, i - 2] = CellType.Wall;
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

                    /*
                    for (int i = 1; i < 47; i += 2)
                    {
                        arena[9, i] = CellType.Wall;
                        arena[19, i + 1] = CellType.Wall;
                        arena[29, i] = CellType.Wall;
                        arena[39, i + 1] = CellType.Wall;
                        arena[49, i] = CellType.Wall;
                        arena[59, i + 1] = CellType.Wall;
                        arena[69, i] = CellType.Wall;
                    }*/
                    break;
            }
        }
    }
}
