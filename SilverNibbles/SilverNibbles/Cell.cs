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



        //jakeBrush = new SolidColorBrush(Color.FromRgb(90, 60, 170));


        SolidColorBrush WallBrush
        {
            get
            {
                if (wallBrush == null)
                    wallBrush = new SolidColorBrush(Color.FromRgb(40, 100, 160));
                return wallBrush;
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
                        case CellType.Jake:
                        case CellType.Sammy:
                            rectangle.Visibility = Visibility.Collapsed;
                            break;
                        case CellType.Wall:
                            rectangle.Visibility = Visibility.Visible;
                            rectangle.Fill = WallBrush;
                            break;
                    }
                }

            }
        }

    }

}
