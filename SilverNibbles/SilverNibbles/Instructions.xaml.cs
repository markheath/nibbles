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
	public partial class Instructions : UserControl
	{
		public Instructions()
		{
			// Required to initialize variables
			InitializeComponent();
		}

        public event RoutedEventHandler OnePlayerButtonClicked
        {
            add { button1Player.Click += value; }
            remove { button1Player.Click -= value; }
        }

        public event RoutedEventHandler TwoPlayerButtonClicked
        {
            add { button2Players.Click += value; }
            remove { button2Players.Click -= value; }
        }

        public bool ButtonsEnabled
        {

            get { return button1Player.IsEnabled; }
            set
            {
                button1Player.IsEnabled = value;
                button2Players.IsEnabled = value;
            }
        }
	}
}