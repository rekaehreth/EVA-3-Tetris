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
        public TetrisView()
        {
            model = new TetrisModel();

            InitializeComponent();
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(e.ClickedItem.Text == "New Game")
            {
                korobeiniki = new SoundPlayer(@"..\Resources\Korobeiniki.wav");
                korobeiniki.PlayLooping();
                // **TODO** Dialog Window for size
                model.newGame();
                // **TODO** Draw table 
            }
            if(e.ClickedItem.Text == "Pause Game")
            {
                korobeiniki.Stop();
                // **TODO** Game Stopped dialog window
                // **TODO** Make game window non-clickable
            }
        }
    }
}
