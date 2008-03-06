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
    public class Snake
    {
        string name;
        SnakeDirection direction;
        SnakeDirection lastQueuedDirection = (SnakeDirection) (-1);
        Queue<SnakeDirection> desiredDirection;
        int length;
        bool alive;
        int lives;
        int score;
        Color color;
        Position currentPos;
        Polyline polyline;        
        
        public Snake(string name, Color color)
        {
            desiredDirection = new Queue<SnakeDirection>();
            length = 2;
            score = 0;
            lives = 5;
            this.color = color;
            this.name = name;
            polyline = new Polyline();
            polyline.Stroke = new SolidColorBrush(color);
            polyline.StrokeThickness = 0.8;
            polyline.StrokeLineJoin = PenLineJoin.Round;
            polyline.StrokeEndLineCap = PenLineCap.Round;
            polyline.StrokeStartLineCap = PenLineCap.Round;
            //ScaleTransform transform = new ScaleTransform();
            //transform.ScaleX = 8;
            //transform.ScaleY = 8;
            //polyline.RenderTransform = transform;
            
        }

        public FrameworkElement Graphics
        {
            get { return polyline; }
        }

        public void Clear()
        {
            polyline.Points.Clear();
        }

        public void Enqueue(Position position)
        {
            polyline.Points.Add(new Point(position.X + 0.5, position.Y + 0.5));
            // yuck! need to do this to make the polyline redraw!
            polyline.Points = polyline.Points;
        }

        public Position Dequeue()
        {
            Point point = polyline.Points[0];
            polyline.Points.RemoveAt(0);
            // yuck! need to do this to make the polyline redraw!
            polyline.Points = polyline.Points;
            // will truncate the 0.5
            return new Position((int)point.X,(int)point.Y);
        }

        public int Count
        {
            get 
            {
                return polyline.Points.Count; 
            }
        }

        /*public Queue<Position> Body
        {
            get { return body; }
        }*/

        public Queue<SnakeDirection> DesiredDirection
        {
            get { return desiredDirection; }
        }

        /// <summary>
        /// Helper function ensures that we don't fill up the desired
        /// direction queue with the same direction multiple times
        /// when a user holds a key down
        /// </summary>
        public void EnqueueDirection(SnakeDirection newDirection)
        {            
            if (newDirection != lastQueuedDirection || desiredDirection.Count == 0)
            {
                desiredDirection.Enqueue(newDirection);
                lastQueuedDirection = newDirection;
            }
        }

        public string Name
        {
            get { return name; }
        }

        public SnakeDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Position CurrentPosition
        {
            get { return currentPos; }
            set { currentPos = value; }
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public Color Color
        {
            get { return color; }
        }

        public void Move()
        {
            switch (direction)
            {
                case SnakeDirection.Up:
                    currentPos.Y--;
                    break;
                case SnakeDirection.Down:
                    currentPos.Y++;
                    break;
                case SnakeDirection.Left:
                    currentPos.X--;
                    break;
                case SnakeDirection.Right:
                    currentPos.X++;
                    break;
            }
        }
    }
    
    public enum SnakeDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
