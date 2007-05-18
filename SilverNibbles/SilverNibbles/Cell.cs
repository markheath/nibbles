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
    public class Cell
    {
        SolidColorBrush wallBrush;
        SolidColorBrush sammyBrush;
        SolidColorBrush blankBrush;
        SolidColorBrush numberBrush;
        SolidColorBrush jakeBrush;

        CellType cellType = (CellType) (-1);
        Rectangle rectangle;

        public Cell(CellType cellType, Rectangle rect)
        {
            this.rectangle = rect;
            this.cellType = cellType;
        }

        public Rectangle Rectangle
        {
            get { return this.rectangle; }
        }

        SolidColorBrush BlankBrush
        {
            get
            {
                if (blankBrush == null)
                    //blankBrush = new SolidColorBrush(Color.FromRgb(160, 170, 90));
                    blankBrush = new SolidColorBrush(Color.FromRgb(137, 206, 255));
                return blankBrush;
            }
        }

        SolidColorBrush SammyBrush
        {
            get
            {
                if (sammyBrush == null)
                    //sammyBrush = new SolidColorBrush(Color.FromRgb(60, 170, 90));
                    sammyBrush = new SolidColorBrush(Color.FromRgb(255, 106, 0));
                return sammyBrush;
            }
        }

        SolidColorBrush JakeBrush
        {
            get
            {
                if (jakeBrush == null)
                    jakeBrush = new SolidColorBrush(Color.FromRgb(90, 60, 170));
                return jakeBrush;
            }
        }

        SolidColorBrush WallBrush
        {
            get
            {
                if (wallBrush == null)
                    wallBrush = new SolidColorBrush(Color.FromRgb(40, 100, 160));
                return wallBrush;
            }
        }

        SolidColorBrush NumberBrush
        {
            get
            {
                if(numberBrush == null)
                    numberBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                return numberBrush;
            }
        }

        public CellType CellType
        {
            get
            {
                return this.cellType;
            }
            set
            {
                if (this.cellType != value)
                {
                    this.cellType = value;
                    switch (this.cellType)
                    {
                        case CellType.Blank:
                        case CellType.TargetNumber:
                            //rectangle.Fill = BlankBrush;
                            rectangle.Visibility = Visibility.Collapsed;
                            break;
                        case CellType.Wall:
                            rectangle.Visibility = Visibility.Visible;
                            rectangle.Fill = WallBrush;
                            break;
                        case CellType.Sammy:
                            //rectangle.Visibility = Visibility.Visible;
                            //rectangle.Fill = SammyBrush;
                            break;
                        case CellType.Jake:
                            rectangle.Visibility = Visibility.Visible;
                            rectangle.Fill = JakeBrush;
                            break;
                    }
                }

            }
        }

    }

}
