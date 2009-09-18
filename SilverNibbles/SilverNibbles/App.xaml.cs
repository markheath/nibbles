using System;
using System.Diagnostics;
using System.Windows;

namespace SilverNibbles
{
    public partial class App : Application
    {
        public App()
        {
            this.Startup += this.Application_Startup;
            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Load the main control
            this.RootVisual = new Page();
        }
    }
}
