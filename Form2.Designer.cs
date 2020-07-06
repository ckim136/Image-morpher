namespace Morphing
{
    partial class Form2
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
            this.lbNumberOfFrames = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbNumberOfFrames
            // 
            this.lbNumberOfFrames.AutoSize = true;
            this.lbNumberOfFrames.Location = new System.Drawing.Point(13, 13);
            this.lbNumberOfFrames.Name = "lbNumberOfFrames";
            this.lbNumberOfFrames.Size = new System.Drawing.Size(0, 17);
            this.lbNumberOfFrames.TabIndex = 0;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(273, 750);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(147, 59);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 953);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.lbNumberOfFrames);
            this.Name = "Form2";
            this.Text = "Morphing Tool";
            this.Load += new System.EventHandler(this.setUpPage);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbNumberOfFrames;
        private System.Windows.Forms.Button btnPlay;
    }
}