
namespace WinFormsTetris.View
{
    partial class SizeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.SmallButton = new System.Windows.Forms.Button();
            this.MediumButton = new System.Windows.Forms.Button();
            this.LargeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please choose the size of your game!";
            // 
            // SmallButton
            // 
            this.SmallButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.SmallButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SmallButton.Location = new System.Drawing.Point(0, 19);
            this.SmallButton.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.SmallButton.Name = "SmallButton";
            this.SmallButton.Size = new System.Drawing.Size(234, 40);
            this.SmallButton.TabIndex = 1;
            this.SmallButton.Text = "4 x 16";
            this.SmallButton.UseVisualStyleBackColor = true;
            this.SmallButton.Click += new System.EventHandler(this.SmallButton_Click);
            // 
            // MediumButton
            // 
            this.MediumButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.MediumButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MediumButton.Location = new System.Drawing.Point(0, 59);
            this.MediumButton.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.MediumButton.Name = "MediumButton";
            this.MediumButton.Size = new System.Drawing.Size(234, 40);
            this.MediumButton.TabIndex = 2;
            this.MediumButton.Text = "8 x 16";
            this.MediumButton.UseVisualStyleBackColor = true;
            this.MediumButton.Click += new System.EventHandler(this.MediumButton_Click);
            // 
            // LargeButton
            // 
            this.LargeButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.LargeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LargeButton.Location = new System.Drawing.Point(0, 99);
            this.LargeButton.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.LargeButton.Name = "LargeButton";
            this.LargeButton.Size = new System.Drawing.Size(234, 40);
            this.LargeButton.TabIndex = 3;
            this.LargeButton.Text = "12 x 16";
            this.LargeButton.UseVisualStyleBackColor = true;
            this.LargeButton.Click += new System.EventHandler(this.LargeButton_Click);
            // 
            // SizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 141);
            this.Controls.Add(this.LargeButton);
            this.Controls.Add(this.MediumButton);
            this.Controls.Add(this.SmallButton);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SizeForm";
            this.Text = "Game size";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SmallButton;
        private System.Windows.Forms.Button MediumButton;
        private System.Windows.Forms.Button LargeButton;
    }
}