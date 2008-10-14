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
        public event EventHandler<RoutedEventArgs> OnePlayerClick;
        public event EventHandler<RoutedEventArgs> TwoPlayerClick;

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

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (OnePlayerClick != null)
                OnePlayerClick(sender, e);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (TwoPlayerClick != null)
                TwoPlayerClick(sender, e);

        }

        private void textBlockMessage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(textBlockMessage.Text.Contains("http://"))
            {
                HtmlPage.Window.Navigate(new Uri("http://www.codeplex.com/nibbles"),"nibbles");
            }
        }

    }

}
