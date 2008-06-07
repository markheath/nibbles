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
    public partial class LivesControl : UserControl
    {
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(LivesControl),
                        new PropertyMetadata(new PropertyChangedCallback(FillChangedCallback)));

        private static void FillChangedCallback(DependencyObject obj,
                                DependencyPropertyChangedEventArgs args)
        {
            LivesControl livesControl = (LivesControl)obj;
            livesControl.L1.Fill = livesControl.Fill;
            livesControl.L2.Fill = livesControl.Fill;
            livesControl.L3.Fill = livesControl.Fill;
            livesControl.L4.Fill = livesControl.Fill;
            livesControl.L5.Fill = livesControl.Fill;
        }

        public Brush Fill
        {
            get
            {
                return (Brush)GetValue(FillProperty);
            }

            set
            {
                SetValue(FillProperty, value);
            }
        }
        
        public LivesControl()
        {
            InitializeComponent();
        }

        int lives;

        public int Lives
        {
            get { return lives; }
            set
            {
                lives = value;
                L1.Visibility = (lives >= 1) ? Visibility.Visible : Visibility.Collapsed;
                L2.Visibility = (lives >= 2) ? Visibility.Visible : Visibility.Collapsed;
                L3.Visibility = (lives >= 3) ? Visibility.Visible : Visibility.Collapsed;
                L4.Visibility = (lives >= 4) ? Visibility.Visible : Visibility.Collapsed;
                L5.Visibility = (lives >= 5) ? Visibility.Visible : Visibility.Collapsed;                
            }
        }


    }
}
