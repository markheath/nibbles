using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MarkHeath.Nibbles
{
    class Snake
    {
        string name;
        SnakeDirection direction;
        Queue<SnakeDirection> desiredDirection;
        Queue<Point> body;
        Brush brush;
        int length;
        Point currentPos;
        bool alive;
        int lives;
        int score;

        public Snake(string name, Color color)
        {
            body = new Queue<Point>();
            desiredDirection = new Queue<SnakeDirection>();
            length = 2;
            score = 0;
            lives = 5;
            brush = new SolidBrush(color);
            this.name = name;
        }

        public Queue<Point> Body
        {
            get { return body; }
        }

        public Queue<SnakeDirection> DesiredDirection
        {
            get { return desiredDirection; }
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

        public Point CurrentPosition
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

        public Brush Brush
        {
            get { return brush; }
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
}
