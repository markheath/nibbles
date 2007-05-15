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
    public struct Position
    {
        public int X;
        public int Y;
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is Point)
            {
                Point other = (Point)obj;
                return ((X == other.X) && (Y == other.Y));
            }
            return false;
        }
        public override int GetHashCode()
        {
            return X.GetHashCode() | Y.GetHashCode();
        }

    }
}
