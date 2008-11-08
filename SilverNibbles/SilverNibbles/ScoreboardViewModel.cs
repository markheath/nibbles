using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace SilverNibbles
{
    public class ScoreboardViewModel : INotifyPropertyChanged
    {
        private int players;
        private int level;
        private int speed;
        private int jakeScore;
        private int sammyScore;
        private string record;

        public int Players
        {
            get
            {
                return players;
            }
            set
            {
                players = value;
                RaisePropertyChanged("Players");
                RaisePropertyChanged("JakeVisible");
            }
        }

        public Visibility JakeVisible
        {
            get
            {
                return players == 2 ? Visibility.Visible : Visibility.Collapsed;
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
                RaisePropertyChanged("Level");
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
                RaisePropertyChanged("Speed");
            }
        }

        public string Record
        {
            get
            {
                return record;
            }
            set
            {
                record = value;
                RaisePropertyChanged("Record");
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
                RaisePropertyChanged("SammyScore");
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
                RaisePropertyChanged("JakeScore");
            }
        }

        int jakeLives;
        int sammyLives;

        public int JakeLives
        {
            get
            {
                return jakeLives;
            }
            set
            {
                jakeLives = value;
                RaisePropertyChanged("JakeLives");
            }
        }

        public int SammyLives
        {
            get
            {
                return sammyLives;
            }
            set
            {
                sammyLives = value;
                RaisePropertyChanged("SammyLives");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
