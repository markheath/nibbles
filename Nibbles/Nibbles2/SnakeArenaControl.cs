using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MarkHeath.Nibbles
{
    public partial class SnakeArenaControl : UserControl
    {
        const int DefaultBlockSize = 8;
        const int Columns = 80;
        const int Rows = 50;
        int blockSize;
        SolidBrush wallBrush; 
        SolidBrush backgroundBrush;
        SolidBrush sammyBrush;
        SolidBrush jakeBrush;
        CellType[,] arena = new CellType[Columns, Rows];
        bool noNumber;
        int currentNumber = 1;
        Point numberPosition;
        Random rand;
        bool paused;

        public SnakeArenaControl()
        {
            sammyBrush = new SolidBrush(Color.Yellow);
            jakeBrush = new SolidBrush(Color.LightGreen);

            InitializeComponent();
            this.Font = new Font("Small Fonts", 8, FontStyle.Regular);

            BlockSize = DefaultBlockSize;
            
            this.BackColorChanged += OnBackColorChanged;
            OnBackColorChanged(this,EventArgs.Empty);

            this.ForeColorChanged += OnForeColorChanged;
            OnForeColorChanged(this, EventArgs.Empty);
            noNumber = true;
            numberPosition = new Point();
            rand = new Random();
        }    

        void OnBackColorChanged(object sender, EventArgs e)
        {
            backgroundBrush = new SolidBrush(BackColor);             
        }

        void OnForeColorChanged(object sender, EventArgs e)
        {
            wallBrush = new SolidBrush(ForeColor);            
        }
            
        public int BlockSize
        {
            get
            {
                return blockSize;
            }
            set
            {
                blockSize = value;
                this.Size = new Size(Columns * blockSize, Rows * blockSize);
            }
        }

        public Color SammyColor
        {
            get
            {
                return sammyBrush.Color;
            }
            set
            {
                sammyBrush = new SolidBrush(value);
                Invalidate();
            }
        }

        public Color JakeColor
        {
            get
            {
                return jakeBrush.Color;
            }
            set
            {
                jakeBrush = new SolidBrush(value);
                Invalidate();
            }
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
                Invalidate();
            }
        }

        public void FindNumberLocation()
        {
            do
            {
                numberPosition.X = rand.Next(1, 79);
                numberPosition.Y = rand.Next(3, 49);
            } while ((arena[numberPosition.X, numberPosition.Y] != CellType.Blank) ||
                (arena[numberPosition.X, numberPosition.Y + 1] != CellType.Blank));
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
                    for (int i = 19; i < 60; i++)
                    {
                        arena[i, 25] = CellType.Wall;
                    }
                    break;

                case 3:
                    for (int i = 9; i < 40; i++)
                    {
                        arena[19, i] = CellType.Wall;
                        arena[59, i] = CellType.Wall;
                    }
                    break;

                case 4:
                    for (int i = 3; i < 30; i++)
                    {
                        arena[19, i] = CellType.Wall;
                        arena[59, 51 - i] = CellType.Wall;
                    }
                    for (int i = 1; i < 40; i++)
                    {
                        arena[i, 37] = CellType.Wall;
                        arena[79 - i, 14] = CellType.Wall;
                    }
                    break;

                case 5:
                    for (int i = 12; i < 39; i++)
                    {
                        arena[20, i] = CellType.Wall;
                        arena[58, i] = CellType.Wall;
                    }
                    for (int i = 22; i < 57; i++)
                    {
                        arena[i, 10] = CellType.Wall;
                        arena[i, 40] = CellType.Wall;
                    }
                    break;

                case 6:
                    for (int i = 3; i < 49; i++)
                    {
                        if ((i > 29) || (i < 22))
                        {
                            arena[9, i] = CellType.Wall;
                            arena[19, i] = CellType.Wall;
                            arena[29, i] = CellType.Wall;
                            arena[39, i] = CellType.Wall;
                            arena[49, i] = CellType.Wall;
                            arena[59, i] = CellType.Wall;
                            arena[69, i] = CellType.Wall;
                        }
                    }
                    break;

                case 7:
                    for (int i = 3; i < 49; i += 2)
                    {
                        arena[39, i] = CellType.Wall;
                    }
                    break;

                case 8:
                    for (int i = 3; i < 40; i++)
                    {
                        arena[9, i] = CellType.Wall;
                        arena[19, 51 - i] = CellType.Wall;
                        arena[29, i] = CellType.Wall;
                        arena[39, 51 - i] = CellType.Wall;
                        arena[49, i] = CellType.Wall;
                        arena[59, 51 - i] = CellType.Wall;
                        arena[69, i] = CellType.Wall;
                    }
                    break;

                case 9:
                    for (int i = 5; i < 47; i++)
                    {
                        arena[i, i] = CellType.Wall;
                        arena[i + 28, i] = CellType.Wall;
                    }
                    break;

                default:
                    for (int i = 3; i < 49; i += 2)
                    {
                        arena[9, i] = CellType.Wall;
                        arena[19, i + 1] = CellType.Wall;
                        arena[29, i] = CellType.Wall;
                        arena[39, i + 1] = CellType.Wall;
                        arena[49, i] = CellType.Wall;
                        arena[59, i + 1] = CellType.Wall;
                        arena[69, i] = CellType.Wall;
                    }
                    break;
            }
            Invalidate();
        }

        public bool Paused
        {
            get
            {
                return paused;
            }
            set
            {
                paused = value;
                Invalidate();
            }
        }        

        public CellType GetCell(Point point)
        {
            return arena[point.X, point.Y];
        }

        public void SetCell(int x, int y, CellType type)
        {
            arena[x, y] = type;
            Rectangle r = new Rectangle(x * BlockSize, y * BlockSize, BlockSize, BlockSize);                    
            Invalidate(r);
        }

        private void DrawNumber(Graphics g)
        {
            g.DrawString(currentNumber.ToString(), this.Font, new SolidBrush(Color.White), new Point(numberPosition.X * BlockSize, numberPosition.Y * BlockSize));
        }

        private void DrawPause(Graphics g)
        {
            g.DrawString("Paused", this.Font, new SolidBrush(Color.White), new Point(36 * BlockSize, 20 * BlockSize));
        }

        public Point NumberPosition
        {
            get
            {
                return numberPosition;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    Rectangle r = new Rectangle(x * BlockSize, y * BlockSize, BlockSize, BlockSize);
                    switch (arena[x, y])
                    {
                        case CellType.Wall:
                            e.Graphics.FillRectangle(wallBrush, r);
                            break;
                        case CellType.Sammy:
                            e.Graphics.FillRectangle(sammyBrush, r);
                            break;
                        case CellType.Jake:
                            e.Graphics.FillRectangle(jakeBrush, r);
                            break;
                        case CellType.Blank:
                            e.Graphics.FillRectangle(backgroundBrush, r);
                            break;
                    }
                }
            }
            if (!noNumber)
            {
                DrawNumber(e.Graphics);
            }
            if (paused)
            {
                DrawPause(e.Graphics);
            }
        }


        private void SnakeArenaControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // this will recieve cursor keys if snakeAreaControl is enabled
        }
    }
}
