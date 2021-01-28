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


            sizeForm.ButtonClicked += SizeButtonClicked;
            model.UpdateTable += UpdateTable;
            InitializeComponent();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            model.MovePieceDown();
        }
        private void UpdateTable(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(e.ClickedItem.Text == "New Game")
            {
                sizeForm.ShowDialog();
            }
            if (e.ClickedItem.Text == "Save Game")
            {
                // **TODO** show saveDialog to get a path
                // **TODO** call model's save
            }
            if (e.ClickedItem.Text == "Load Game")
            {
                // **TODO** show saveDialog to get a path
                // **TODO** call model's save
            }
            if (e.ClickedItem.Text == "Pause Game")
            {
                korobeiniki?.Stop();
                // **TODO** Game Stopped dialog window
                // **TODO** Make game window non-clickable ( .ShowDialog opens a form modally so the main window won't be clickable)
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
            timer.Start();
            korobeiniki = new SoundPlayer(@"..\Resources\Korobeiniki.wav");
            korobeiniki.PlayLooping();
            // **TODO** Dialog Window for size
            // **TODO** Draw table 
            throw new NotImplementedException();
        }
    }
}
