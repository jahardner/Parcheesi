namespace parcheesi_space
{
    partial class space
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.piece1 = new System.Windows.Forms.PictureBox();
            this.piece2 = new System.Windows.Forms.PictureBox();
            this.piece3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.piece1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piece2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piece3)).BeginInit();
            this.SuspendLayout();
            // 
            // piece1
            // 
            this.piece1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.piece1.Location = new System.Drawing.Point(0, 2);
            this.piece1.Name = "piece1";
            this.piece1.Size = new System.Drawing.Size(16, 16);
            this.piece1.TabIndex = 0;
            this.piece1.TabStop = false;
            // 
            // piece2
            // 
            this.piece2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.piece2.Location = new System.Drawing.Point(19, 2);
            this.piece2.Name = "piece2";
            this.piece2.Size = new System.Drawing.Size(16, 16);
            this.piece2.TabIndex = 1;
            this.piece2.TabStop = false;
            // 
            // piece3
            // 
            this.piece3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.piece3.Location = new System.Drawing.Point(39, 2);
            this.piece3.Name = "piece3";
            this.piece3.Size = new System.Drawing.Size(16, 16);
            this.piece3.TabIndex = 2;
            this.piece3.TabStop = false;
            // 
            // space
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.piece3);
            this.Controls.Add(this.piece2);
            this.Controls.Add(this.piece1);
            this.Name = "space";
            this.Size = new System.Drawing.Size(55, 20);
            ((System.ComponentModel.ISupportInitialize)(this.piece1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.piece2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.piece3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox piece1;
        public System.Windows.Forms.PictureBox piece2;
        public System.Windows.Forms.PictureBox piece3;

    }
}
