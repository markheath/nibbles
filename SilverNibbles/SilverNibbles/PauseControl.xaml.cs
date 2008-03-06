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
    public partial class PauseControl : UserControl
    {
        public PauseControl()
        {
            InitializeComponent();     
        }

        public void Appear()
        {
            //this.Visibility = Visibility.Visible;
            appearStoryboard.Begin();
        }

        public void Disappear()
        {
            //this.Visibility = Visibility.Collapsed;
            disappearStoryboard.Begin();
        }

        public string Text
        {
            get { return textBlockMessage.Text; }
            set { textBlockMessage.Text = value; }
        }

    }

}
