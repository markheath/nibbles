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



        public int Lives
        {
            get { return (int)GetValue(LivesProperty); }
            set { SetValue(LivesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Lives.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LivesProperty =
            DependencyProperty.Register("Lives", typeof(int), typeof(LivesControl), new PropertyMetadata(0, new PropertyChangedCallback(LivesChangedCallback)));



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

        private static void LivesChangedCallback(DependencyObject obj,
                        DependencyPropertyChangedEventArgs args)
        {
            LivesControl livesControl = (LivesControl)obj;
            livesControl.L1.Visibility = (livesControl.Lives >= 1) ? Visibility.Visible : Visibility.Collapsed;
            livesControl.L2.Visibility = (livesControl.Lives >= 2) ? Visibility.Visible : Visibility.Collapsed;
            livesControl.L3.Visibility = (livesControl.Lives >= 3) ? Visibility.Visible : Visibility.Collapsed;
            livesControl.L4.Visibility = (livesControl.Lives >= 4) ? Visibility.Visible : Visibility.Collapsed;
            livesControl.L5.Visibility = (livesControl.Lives >= 5) ? Visibility.Visible : Visibility.Collapsed;
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

        
    }
}
