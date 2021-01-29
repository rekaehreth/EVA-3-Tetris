
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
            this.PlayingArea = new System.Windows.Forms.TableLayoutPanel();
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
            this.menu.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menu.Size = new System.Drawing.Size(700, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "Menu";
            this.menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menu_ItemClicked);
            // 
            // newGame
            // 
            this.newGame.Name = "newGame";
            this.newGame.Size = new System.Drawing.Size(77, 20);
            this.newGame.Text = "New Game";
            // 
            // saveGame
            // 
            this.saveGame.Name = "saveGame";
            this.saveGame.Size = new System.Drawing.Size(77, 20);
            this.saveGame.Text = "Save Game";
            // 
            // loadGame
            // 
            this.loadGame.Name = "loadGame";
            this.loadGame.Size = new System.Drawing.Size(79, 20);
            this.loadGame.Text = "Load Game";
            // 
            // pauseGame
            // 
            this.pauseGame.Name = "pauseGame";
            this.pauseGame.Size = new System.Drawing.Size(50, 20);
            this.pauseGame.Text = "Pause";
            // 
            // PlayingArea
            // 
            this.PlayingArea.ColumnCount = 2;
            this.PlayingArea.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PlayingArea.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PlayingArea.Location = new System.Drawing.Point(139, 57);
            this.PlayingArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PlayingArea.Name = "PlayingArea";
            this.PlayingArea.RowCount = 2;
            this.PlayingArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PlayingArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PlayingArea.Size = new System.Drawing.Size(219, 94);
            this.PlayingArea.TabIndex = 1;
            // 
            // TetrisView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 338);
            this.Controls.Add(this.PlayingArea);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.TableLayoutPanel PlayingArea;
    }
}

