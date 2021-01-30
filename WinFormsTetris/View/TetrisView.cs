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
        TableLayoutPanel PlayingArea;
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

            this.AutoSize = true;

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
                NewGame();
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
        private void NewGame()
        {
            model.EndGame();
            PlayingArea.Controls.Clear();
            sizeForm.ShowDialog();
        }
        private async Task SaveGameAsync()
        {
            model.PauseGame();
            timer.Stop();
            korobeiniki?.Stop();
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = fileDialog.FileName;
                await model.SaveGameAsync(path);
            }
            if (MessageBox.Show("Game Paused\nPress OK to continue", "Game Paused", MessageBoxButtons.OK) == DialogResult.OK)
            {
                model.ContinueGame();
                timer.Start();
                korobeiniki.PlayLooping();
            }
        }
        private async Task LoadGameAsync()
        {
            model.PauseGame();
            timer.Stop();
            korobeiniki?.Stop();
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
                            PlayingArea.Controls[row * model.Size + column].BackColor = Color.Yellow;
                            break;
                        case (int)PieceType.Hero + 1:
                            PlayingArea.Controls[row * model.Size + column].BackColor = Color.Blue;
                            break;
                        case (int)PieceType.Ricky + 1:
                            PlayingArea.Controls[row * model.Size + column].BackColor = Color.Orange;
                            break;
                        case (int)PieceType.Z + 1:
                            PlayingArea.Controls[row * model.Size + column].BackColor = Color.Green;
                            break;
                        case (int)PieceType.TeeWee + 1:
                            PlayingArea.Controls[row * model.Size + column].BackColor = Color.Purple;
                            break;
                        default:
                            // empty
                            PlayingArea.Controls[row * model.Size + column].BackColor = Color.LightGray;
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
                NewGame();
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
                Pause();
            }
        }
        private void Pause()
        {
            model.PauseGame();
            timer.Stop();
            korobeiniki?.Stop();
            if (MessageBox.Show("Game Paused\nPress OK to continue", "Game Paused", MessageBoxButtons.OK) == DialogResult.OK)
            {
                model.ContinueGame();
                timer.Start();
                korobeiniki.PlayLooping();
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
            PlayingArea.RowCount = 16;
            PlayingArea.ColumnCount = model.Size;
            for (int row = 0; row < PlayingArea.RowCount; ++row)
            {
                RowStyle rowStyle = new RowStyle();
                rowStyle.SizeType = SizeType.Percent;
                rowStyle.Height = PlayingArea.Height / PlayingArea.RowCount;
                PlayingArea.RowStyles.Add(rowStyle);
                for (int column = 0; column < PlayingArea.ColumnCount; ++column)
                {
                    ColumnStyle columnStyle = new ColumnStyle();
                    columnStyle.SizeType = SizeType.Percent;
                    columnStyle.Width = this.Width / PlayingArea.ColumnCount;
                    PlayingArea.ColumnStyles.Add(columnStyle);

                    Button button = new Button();
                    button.Enabled = false;
                    button.Dock = DockStyle.Fill;
                    button.BackColor = Color.LightGray;
                    PlayingArea.Controls.Add(button, column, row);
                }
            }
            UpdateTable(null, null);
            timer.Start();
            startTime = DateTime.Now;
            korobeiniki = new SoundPlayer(@"..\Resources\Korobeiniki.wav");
            korobeiniki.PlayLooping();
            panel.Controls.Add(PlayingArea);
        }
    }
}