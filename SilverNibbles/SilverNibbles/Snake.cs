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
        Queue<SnakeDirection> desiredDirection;
        Queue<Position> body;
        int length;
        bool alive;
        int lives;
        int score;
        Color color;
        Position currentPos;
        
        public Snake(string name, Color color)
        {
            body = new Queue<Position>();
            desiredDirection = new Queue<SnakeDirection>();
            length = 2;
            score = 0;
            lives = 5;
            this.color = color;
            this.name = name;
        }

        public Queue<Position> Body
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
