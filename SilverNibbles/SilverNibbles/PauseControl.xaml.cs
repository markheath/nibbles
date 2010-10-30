using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;

namespace SilverNibbles
{
    public partial class PauseControl : UserControl
    {
        public event RoutedEventHandler OnePlayerClick
        {
            add { instructions.OnePlayerButtonClicked += value; }
            remove { instructions.OnePlayerButtonClicked -= value; }
        }

        public event RoutedEventHandler TwoPlayerClick
        {
            add { instructions.TwoPlayerButtonClicked += value; }
            remove { instructions.TwoPlayerButtonClicked -= value; }
        }

        public PauseControl()
        {
            InitializeComponent();
        }

        public void Appear(bool paused)
        {
            InstructionsVisible = !paused;
            appearStoryboard.Begin();
        }

        public void Disappear()
        {
            disappearStoryboard.Begin();
            InstructionsVisible = false;
        }

        bool InstructionsVisible
        {
            get 
            { 
                return instructions.Visibility == Visibility.Visible; 
            }
            set 
            { 
                instructions.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                // despite not being visible, the buttons still seem to pick up
                // the space bar, so we will disable them.
                instructions.ButtonsEnabled = value;
            }

        }

        /// <summary>
        /// Pause control text
        /// </summary>
        public string Text
        {
            get { return textBlockMessage.Text; }
            set { textBlockMessage.Text = value; }
        }

        private void textBlockMessage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(textBlockMessage.Text.Contains("http://"))
            {
                HtmlPage.Window.Navigate(new Uri("http://nibbles.codeplex.com"),"nibbles");
            }
        }

    }

}
