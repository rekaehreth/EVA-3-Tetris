
namespace WinFormsTetris
{
    partial class TetrisView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menu = new System.Windows.Forms.MenuStrip();
            this.newGame = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGame = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGame = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGame,
            this.saveGame,
            this.loadGame,
            this.pauseGame});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 28);
            this.menu.TabIndex = 0;
            this.menu.Text = "Menu";
            this.menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menu_ItemClicked);
            // 
            // newGame
            // 
            this.newGame.Name = "newGame";
            this.newGame.Size = new System.Drawing.Size(96, 24);
            this.newGame.Text = "New Game";
            // 
            // saveGame
            // 
            this.saveGame.Name = "saveGame";
            this.saveGame.Size = new System.Drawing.Size(97, 24);
            this.saveGame.Text = "Save Game";
            // 
            // loadGame
            // 
            this.loadGame.Name = "loadGame";
            this.loadGame.Size = new System.Drawing.Size(99, 24);
            this.loadGame.Text = "Load Game";
            // 
            // pauseGame
            // 
            this.pauseGame.Name = "pauseGame";
            this.pauseGame.Size = new System.Drawing.Size(103, 24);
            this.pauseGame.Text = "Pause Game";
            // 
            // TetrisView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "TetrisView";
            this.Text = "Form1";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem newGame;
        private System.Windows.Forms.ToolStripMenuItem saveGame;
        private System.Windows.Forms.ToolStripMenuItem loadGame;
        private System.Windows.Forms.ToolStripMenuItem pauseGame;
    }
}

