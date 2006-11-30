namespace MarkHeath.Nibbles
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelLevel = new System.Windows.Forms.Label();
            this.labelRecord = new System.Windows.Forms.Label();
            this.labelSammy = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelJake = new System.Windows.Forms.Label();
            this.buttonOnePlayer = new System.Windows.Forms.Button();
            this.buttonTwoPlayer = new System.Windows.Forms.Button();
            this.snakeArenaControl = new MarkHeath.Nibbles.SnakeArenaControl();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(678, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playerToolStripMenuItem,
            this.playerToolStripMenuItem1,
            this.pauseToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // playerToolStripMenuItem
            // 
            this.playerToolStripMenuItem.Name = "playerToolStripMenuItem";
            this.playerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.playerToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.playerToolStripMenuItem.Text = "&1 Player";
            this.playerToolStripMenuItem.Click += new System.EventHandler(this.OnOnePlayer);
            // 
            // playerToolStripMenuItem1
            // 
            this.playerToolStripMenuItem1.Name = "playerToolStripMenuItem1";
            this.playerToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.playerToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
            this.playerToolStripMenuItem1.Text = "&2 Player";
            this.playerToolStripMenuItem1.Click += new System.EventHandler(this.OnTwoPlayer);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.pauseToolStripMenuItem.Text = "&Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.OnFilePause);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.optionsToolStripMenuItem.Text = "&Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OnFileOptions);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnFileExit);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.contentsToolStripMenuItem.Text = "&Contents";
            this.contentsToolStripMenuItem.Click += new System.EventHandler(this.OnHelpContents);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnHelpAbout);
            // 
            // timer1
            // 
            this.timer1.Interval = 80;
            this.timer1.Tick += new System.EventHandler(this.OnTimerTick);
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(13, 28);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(42, 13);
            this.labelLevel.TabIndex = 1;
            this.labelLevel.Text = "Level 1";
            // 
            // labelRecord
            // 
            this.labelRecord.AutoSize = true;
            this.labelRecord.Location = new System.Drawing.Point(361, 28);
            this.labelRecord.Name = "labelRecord";
            this.labelRecord.Size = new System.Drawing.Size(124, 13);
            this.labelRecord.TabIndex = 1;
            this.labelRecord.Text = "High Score: 0 by nobody";
            // 
            // labelSammy
            // 
            this.labelSammy.AutoSize = true;
            this.labelSammy.Location = new System.Drawing.Point(16, 45);
            this.labelSammy.Name = "labelSammy";
            this.labelSammy.Size = new System.Drawing.Size(124, 13);
            this.labelSammy.TabIndex = 2;
            this.labelSammy.Text = "Sammy: Lives 5: Score 0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sammy: Lives 5: Score 0";
            // 
            // labelJake
            // 
            this.labelJake.AutoSize = true;
            this.labelJake.Location = new System.Drawing.Point(361, 45);
            this.labelJake.Name = "labelJake";
            this.labelJake.Size = new System.Drawing.Size(113, 13);
            this.labelJake.TabIndex = 2;
            this.labelJake.Text = "Jake: Lives 5: Score 0";
            // 
            // buttonOnePlayer
            // 
            this.buttonOnePlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOnePlayer.Location = new System.Drawing.Point(12, 479);
            this.buttonOnePlayer.Name = "buttonOnePlayer";
            this.buttonOnePlayer.Size = new System.Drawing.Size(75, 23);
            this.buttonOnePlayer.TabIndex = 3;
            this.buttonOnePlayer.Text = "&1 Player";
            this.buttonOnePlayer.UseVisualStyleBackColor = true;
            this.buttonOnePlayer.Click += new System.EventHandler(this.OnOnePlayer);
            // 
            // buttonTwoPlayer
            // 
            this.buttonTwoPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTwoPlayer.Location = new System.Drawing.Point(102, 479);
            this.buttonTwoPlayer.Name = "buttonTwoPlayer";
            this.buttonTwoPlayer.Size = new System.Drawing.Size(75, 23);
            this.buttonTwoPlayer.TabIndex = 4;
            this.buttonTwoPlayer.Text = "&2 Player";
            this.buttonTwoPlayer.UseVisualStyleBackColor = true;
            this.buttonTwoPlayer.Click += new System.EventHandler(this.OnTwoPlayer);
            // 
            // snakeArenaControl
            // 
            this.snakeArenaControl.BackColor = System.Drawing.Color.Blue;
            this.snakeArenaControl.BlockSize = 8;
            this.snakeArenaControl.CurrentNumber = 1;
            this.snakeArenaControl.Enabled = false;
            this.snakeArenaControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.snakeArenaControl.ForeColor = System.Drawing.Color.Magenta;
            this.snakeArenaControl.JakeColor = System.Drawing.Color.LightGreen;
            this.snakeArenaControl.Location = new System.Drawing.Point(19, 71);
            this.snakeArenaControl.Name = "snakeArenaControl";
            this.snakeArenaControl.NoNumber = true;
            this.snakeArenaControl.Paused = false;
            this.snakeArenaControl.SammyColor = System.Drawing.Color.Yellow;
            this.snakeArenaControl.Size = new System.Drawing.Size(640, 400);
            this.snakeArenaControl.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 514);
            this.Controls.Add(this.snakeArenaControl);
            this.Controls.Add(this.buttonTwoPlayer);
            this.Controls.Add(this.buttonOnePlayer);
            this.Controls.Add(this.labelJake);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelSammy);
            this.Controls.Add(this.labelRecord);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Nibbles .NET";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.Label labelRecord;
        private System.Windows.Forms.Label labelSammy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelJake;
        private System.Windows.Forms.Button buttonOnePlayer;
        private System.Windows.Forms.Button buttonTwoPlayer;
        private SnakeArenaControl snakeArenaControl;
    }
}

