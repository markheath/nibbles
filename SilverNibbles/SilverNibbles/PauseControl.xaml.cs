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
    public class PauseControl : Control
    {
        TextBlock textBlockMessage;
        Rectangle rectBorder;
        Storyboard appear;
        Storyboard disappear;
        public PauseControl()
        {
            System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream("SilverNibbles.PauseControl.xaml");
            FrameworkElement rootElement = this.InitializeFromXaml(new System.IO.StreamReader(s).ReadToEnd());
            textBlockMessage = (TextBlock) rootElement.FindName("textBlockMessage");
            rectBorder = (Rectangle)rootElement.FindName("rectBorder");
            appear = (Storyboard)rootElement.FindName("Appear");
            disappear = (Storyboard)rootElement.FindName("Disappear");
            //this.Opacity = 0;
            
        }

        public void Appear()
        {
            //this.Visibility = Visibility.Visible;
            appear.Begin();
        }

        public void Disappear()
        {
            //this.Visibility = Visibility.Collapsed;
            disappear.Begin();
        }

        public string Text
        {
            get { return textBlockMessage.Text; }
            set { textBlockMessage.Text = value; }
        }

        // Sets/gets the Width of the actual control
        public new double Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                UpdateLayout();
            }
        }

        // Sets/gets the Height of the actual control
        public virtual new double Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
                UpdateLayout();
            }
        }

        protected virtual void UpdateLayout()        
        {
            rectBorder.Width = Width;
            rectBorder.Height = Height;
            textBlockMessage.Width = Width - 8 * 2;
            textBlockMessage.Height = Height - 8 * 2;
            
        }

    }

}
