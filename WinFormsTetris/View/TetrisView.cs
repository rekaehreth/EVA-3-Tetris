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
using WinFormsTetris.Model;

namespace WinFormsTetris
{
    public partial class TetrisView : Form
    {
        TetrisModel model;
        SoundPlayer korobeiniki;
        SizeForm sizeForm;
        Timer timer;
        DateTime startTime;
        public TetrisView()
        {
            model = new TetrisModel();
            PlayingArea = new TableLayoutPanel();
            PlayingArea.Visible = false;
            sizeForm = new SizeForm();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            KeyDown += KeyPressed;
            sizeForm.ButtonClicked += SizeButtonClicked;
            model.UpdateTable += UpdateTable;
            model.GameOver += GameIsOver;
            InitializeComponent();
        }
        private void GameIsOver(object sender, EventArgs e)
        {
            timer.Stop();
            korobeiniki?.Stop();
            TimeSpan elapsedTime = DateTime.Now - startTime;
            MessageBox.Show($"Game lasted for {elapsedTime.Minutes} minutes {elapsedTime.Seconds} seconds", "Game Over", MessageBoxButtons.OK);
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
            else if (e.Control && e.KeyCode == Keys.N)
            {
                sizeForm.ShowDialog();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                SaveGameAsync();
            }
            else if (e.Control && e.KeyCode == Keys.L)
            {
                LoadGameAsync();
            }
        }
        private async Task SaveGameAsync()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = fileDialog.FileName;
                await model.SaveGameAsync(path);
            }
        }
        private async Task LoadGameAsync()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = fileDialog.FileName;
                await model.LoadGameAsync(path);
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            model.MovePieceDown();
        }
        private void UpdateTable(object sender, EventArgs e)
        {
            for (int row = 0; row < 16; ++row)
            {
                for (int column = 0; column < model.Size; ++column)
                {
                    switch ((int)model.Table[row, column])
                    {
                        case (int)PieceType.Smashboy + 1:
                            PlayingArea.Controls[row + column * 16].BackColor = Color.Yellow;
                            break;
                        case (int)PieceType.Hero + 1:
                            PlayingArea.Controls[row + column * 16].BackColor = Color.Blue;
                            break;
                        case (int)PieceType.Ricky + 1:
                            PlayingArea.Controls[row + column * 16].BackColor = Color.Orange;
                            break;
                        case (int)PieceType.Z + 1:
                            PlayingArea.Controls[row + column * 16].BackColor = Color.Green;
                            break;
                        case (int)PieceType.TeeWee + 1:
                            PlayingArea.Controls[row + column * 16].BackColor = Color.Purple;
                            break;
                        default:
                            // empty
                            PlayingArea.Controls[row + column * 16].BackColor = Color.LightGray;
                            break;
                    }
                }
            }
            PlayingArea.Visible = true;
        }
        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(e.ClickedItem.Text == "New Game")
            {
                sizeForm.ShowDialog();
            }
            if (e.ClickedItem.Text == "Save Game")
            {
                SaveGameAsync();
            }
            if (e.ClickedItem.Text == "Load Game")
            {
                LoadGameAsync();
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
                    button.BackColor = Color.LightGray;
                    PlayingArea.Controls.Add(button, row, column);
                    PlayingArea.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    PlayingArea.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                }
            }
            UpdateTable(null, null);
            timer.Start();
            startTime = DateTime.Now;
            korobeiniki = new SoundPlayer(@"..\Resources\Korobeiniki.wav");
            korobeiniki.PlayLooping();
        }
    }
}
