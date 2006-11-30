using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MarkHeath.Nibbles
{
    public partial class HighScoreForm : Form
    {
        public HighScoreForm()
        {
            InitializeComponent();
        }

        public string UserName
        {
            get { return textBoxName.Text; }
            set { textBoxName.Text = value; textBoxName.SelectAll(); }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}