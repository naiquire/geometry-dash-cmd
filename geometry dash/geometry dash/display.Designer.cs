namespace geometry_dash
{
    partial class display
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
            this.pic_bg = new System.Windows.Forms.PictureBox();
            this.pic_bg_2 = new System.Windows.Forms.PictureBox();
            this.pic_bg_3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_bg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_bg_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_bg_3)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_bg
            // 
            this.pic_bg.BackgroundImage = global::geometry_dash.Properties.Resources.bg;
            this.pic_bg.Location = new System.Drawing.Point(0, 9);
            this.pic_bg.Name = "pic_bg";
            this.pic_bg.Size = new System.Drawing.Size(1920, 1023);
            this.pic_bg.TabIndex = 0;
            this.pic_bg.TabStop = false;
            // 
            // pic_bg_2
            // 
            this.pic_bg_2.BackgroundImage = global::geometry_dash.Properties.Resources.bg;
            this.pic_bg_2.Location = new System.Drawing.Point(0, 9);
            this.pic_bg_2.Margin = new System.Windows.Forms.Padding(0);
            this.pic_bg_2.Name = "pic_bg_2";
            this.pic_bg_2.Size = new System.Drawing.Size(1920, 1023);
            this.pic_bg_2.TabIndex = 1;
            this.pic_bg_2.TabStop = false;
            // 
            // pic_bg_3
            // 
            this.pic_bg_3.BackgroundImage = global::geometry_dash.Properties.Resources.bg;
            this.pic_bg_3.Location = new System.Drawing.Point(0, 9);
            this.pic_bg_3.Margin = new System.Windows.Forms.Padding(0);
            this.pic_bg_3.Name = "pic_bg_3";
            this.pic_bg_3.Size = new System.Drawing.Size(1920, 1023);
            this.pic_bg_3.TabIndex = 2;
            this.pic_bg_3.TabStop = false;
            // 
            // display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1061);
            this.Controls.Add(this.pic_bg_3);
            this.Controls.Add(this.pic_bg_2);
            this.Controls.Add(this.pic_bg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "display";
            this.Text = "display";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pic_bg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_bg_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_bg_3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_bg;
        private System.Windows.Forms.PictureBox pic_bg_2;
        private System.Windows.Forms.PictureBox pic_bg_3;
    }
}