using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsTetris
{
    public partial class SizeForm : Form
    {
        public EventHandler<SizeButtonEventArgs> ButtonClicked;
        public SizeForm()
        {
            InitializeComponent();
        }
        private void SmallButton_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, new SizeButtonEventArgs("Small"));
            Close();
        }

        private void MediumButton_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, new SizeButtonEventArgs("Medium"));
            Close();
        }

        private void LargeButton_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, new SizeButtonEventArgs("Large"));
            Close();
        }
    }
    public class SizeButtonEventArgs : EventArgs
    {
        public string Size { get; set; }
        public SizeButtonEventArgs(string size)
        {
            Size = size;
        }
    }
}
