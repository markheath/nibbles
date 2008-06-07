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

        void Button1_Click(object sender, RoutedEventArgs args)
        {
            if (OnePlayerButtonClicked != null)
            {
                OnePlayerButtonClicked(this, args);
            }
        }

        void Button2_Click(object sender, RoutedEventArgs args)
        {
            if (TwoPlayerButtonClicked != null)
            {
                TwoPlayerButtonClicked(this, args);
            }
        }

        public event RoutedEventHandler OnePlayerButtonClicked;
        public event RoutedEventHandler TwoPlayerButtonClicked;

        public bool IsEnabled
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