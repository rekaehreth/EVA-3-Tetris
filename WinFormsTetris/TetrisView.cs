using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTetris
{
    public partial class TetrisView : Form
    {
        TetrisModel model;
        SoundPlayer korobeiniki;
        SizeForm sizeForm;
        Timer timer;
        public TetrisView()
        {
            model = new TetrisModel();
            PlayingArea.Visible = false;
            sizeForm = new SizeForm();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            KeyDown += KeyPressed;
            sizeForm.ButtonClicked += SizeButtonClicked;
            model.UpdateTable += UpdateTable;
            InitializeComponent();
        }
        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                model.MovePieceRight();
            }
            else if(e.KeyCode == Keys.Left)
            {
                model.MovePieceLeft();
            }
            else if (e.KeyCode == Keys.Down)
            {
                model.MovePieceDown();
            }
            else if(e.KeyCode == Keys.Up)
            {
                model.RotatePiece();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                SaveGame();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                sizeForm.ShowDialog();
            }
            else if (e.Control && e.KeyCode == Keys.L)
            {
                LoadGame();
            }
        }
        private void SaveGame()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = fileDialog.FileName;
                model.SaveGame(path);
            }
        }
        private void LoadGame()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = fileDialog.FileName;
                model.LoadGame(path);
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            model.MovePieceDown();
        }
        private void UpdateTable(object sender, EventArgs e)
        {
            for (int row = 0; row < 5; ++row)
            {
                for (int column = 0; column < 5; ++column)
                {
                    switch ((int)model.Table[row, column])
                    {
                        case 0:
                            // empty
                            PlayingArea.Controls[row * 16 + column].BackColor = Color.LightGray;
                            break;
                        case 1:
                            // Smashboy
                            PlayingArea.Controls[row * 16 + column].BackColor = Color.Yellow;
                            break;
                        case 2:
                            // Hero
                            PlayingArea.Controls[row * 16 + column].BackColor = Color.Blue;
                            break;
                        case 3:
                            // Ricky
                            PlayingArea.Controls[row * 16 + column].BackColor = Color.Orange;
                            break;
                        case 4:
                            // Z
                            PlayingArea.Controls[row * 16 + column].BackColor = Color.Green;
                            break;
                        case 5:
                            // TeeWee
                            PlayingArea.Controls[row * 16 + column].BackColor = Color.Purple;
                            break;
                    }
                }
            }
        }
        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(e.ClickedItem.Text == "New Game")
            {
                sizeForm.ShowDialog();
            }
            if (e.ClickedItem.Text == "Save Game")
            {
                SaveGame();
            }
            if (e.ClickedItem.Text == "Load Game")
            {
                LoadGame();
            }
            if (e.ClickedItem.Text == "Pause")
            {
                model.PauseGame();
                timer.Stop();
                korobeiniki?.Stop();
                MessageBox.Show("Game Paused\nPress OK to continue", "Game Paused", MessageBoxButtons.OK);
                e.ClickedItem.Text = "Continue";
            }
            if (e.ClickedItem.Text == "Continue")
            {
                model.ContinueGame();
                timer.Start();
                korobeiniki.PlayLooping();
                e.ClickedItem.Text = "Pause";
            }
        }
        private void SizeButtonClicked(object sender, SizeButtonEventArgs sizeButtonEventArgs)
        {
            if(sizeButtonEventArgs.Size == "Small")
            {
                model.NewGame(4);
            }
            if(sizeButtonEventArgs.Size == "Medium")
            {
                model.NewGame(8);
            }
            if(sizeButtonEventArgs.Size == "Large")
            {
                model.NewGame(12);
            } 
            PlayingArea.Dock = DockStyle.Fill;
            PlayingArea.AutoSize = true;
            PlayingArea.RowCount = 16;
            PlayingArea.ColumnCount = model.Size;
            for (int row = 0; row < PlayingArea.RowCount; ++row)
            {
                for (int column = 0; column < PlayingArea.ColumnCount; ++column)
                {
                    Button button = new Button();
                    button.Enabled = false;
                    button.Dock = DockStyle.Fill;
                    button.Height = PlayingArea.Height / PlayingArea.RowCount;
                    button.Width = PlayingArea.Width / PlayingArea.ColumnCount;
                    switch((int) model.Table[row, column])
                    {
                        case 0:
                            // empty
                            button.BackColor = Color.LightGray;
                            break;
                        case 1:
                            // Smashboy
                            button.BackColor = Color.Yellow;
                            break;
                        case 2:
                            // Hero
                            button.BackColor = Color.Blue;
                            break;
                        case 3:
                            // Ricky
                            button.BackColor = Color.Orange;
                            break;
                        case 4:
                            // Z
                            button.BackColor = Color.Green;
                            break;
                        case 5:
                            // TeeWee
                            button.BackColor = Color.Purple;
                            break;
                    }
                    PlayingArea.Controls.Add(button, row, column);
                    PlayingArea.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    PlayingArea.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                }
            }
            timer.Start();
            korobeiniki = new SoundPlayer(@"..\Resources\Korobeiniki.wav");
            korobeiniki.PlayLooping();
            throw new NotImplementedException();
        }
    }
}
