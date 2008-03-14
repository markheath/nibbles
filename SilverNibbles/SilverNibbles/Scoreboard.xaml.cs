using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverNibbles
{
    public partial class Scoreboard : UserControl
    {
        public Scoreboard()
        {
            InitializeComponent();
        }

        private int players;
        private int level;
        private int speed;
        private int jakeScore;
        private int sammyScore;

        public int Players
        {
            get
            {
                return players;
            }
            set
            {
                players = value;
                jakeLives.Visibility = players == 2 ? Visibility.Visible : Visibility.Collapsed;
                jakeScoreLabel.Visibility = players == 2 ? Visibility.Visible : Visibility.Collapsed;
                jakeScoreTextBlock.Visibility = players == 2 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
                levelTextBlock.Text = value.ToString();
            }
        }

        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
                speedTextBlock.Text = value.ToString();
            }
        }

        public int SammyScore
        {
            get
            {
                return sammyScore;
            }
            set
            {
                sammyScore = value;
                sammyScoreTextBlock.Text = value.ToString();
            }
        }

        public int JakeScore
        {
            get
            {
                return jakeScore;
            }
            set
            {
                jakeScore = value;
                jakeScoreTextBlock.Text = value.ToString();
            }
        }

        public int JakeLives
        {
            get
            {
                return jakeLives.Lives;
            }
            set
            {
                jakeLives.Lives = value;
            }
        }

        public int SammyLives
        {
            get
            {
                return sammyLives.Lives;
            }
            set
            {
                sammyLives.Lives = value;
            }
        }
    }
}
