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
	public partial class Key : UserControl
	{
		public Key()
		{
			// Required to initialize variables
			InitializeComponent();
		}

        public string Text
        {
            get { return runKey.Text; }
            set { runKey.Text = value; }
        }
	}
}