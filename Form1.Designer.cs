namespace Morphing
{
    partial class Form1
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
            this.sourceImage = new System.Windows.Forms.PictureBox();
            this.destImage = new System.Windows.Forms.PictureBox();
            this.btn_sourceImg = new System.Windows.Forms.Button();
            this.btn_DestImg = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMorph = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumberOfFrame = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sourceImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.destImage)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sourceImage
            // 
            this.sourceImage.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sourceImage.Location = new System.Drawing.Point(35, 28);
            this.sourceImage.Name = "sourceImage";
            this.sourceImage.Size = new System.Drawing.Size(400, 450);
            this.sourceImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.sourceImage.TabIndex = 0;
            this.sourceImage.TabStop = false;
            // 
            // destImage
            // 
            this.destImage.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.destImage.Location = new System.Drawing.Point(463, 28);
            this.destImage.Name = "destImage";
            this.destImage.Size = new System.Drawing.Size(400, 450);
            this.destImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.destImage.TabIndex = 1;
            this.destImage.TabStop = false;
            // 
            // btn_sourceImg
            // 
            this.btn_sourceImg.Location = new System.Drawing.Point(21, 27);
            this.btn_sourceImg.Name = "btn_sourceImg";
            this.btn_sourceImg.Size = new System.Drawing.Size(150, 50);
            this.btn_sourceImg.TabIndex = 2;
            this.btn_sourceImg.Text = "Load Image 1";
            this.btn_sourceImg.UseVisualStyleBackColor = true;
            this.btn_sourceImg.Click += new System.EventHandler(this.Btn_sourceImg_Click);
            // 
            // btn_DestImg
            // 
            this.btn_DestImg.Location = new System.Drawing.Point(21, 94);
            this.btn_DestImg.Name = "btn_DestImg";
            this.btn_DestImg.Size = new System.Drawing.Size(150, 50);
            this.btn_DestImg.TabIndex = 3;
            this.btn_DestImg.Text = "Load Image 2";
            this.btn_DestImg.UseVisualStyleBackColor = true;
            this.btn_DestImg.Click += new System.EventHandler(this.BtnDestImg_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(21, 160);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(150, 50);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete Line";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.btnMorph);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtNumberOfFrame);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btn_DestImg);
            this.panel1.Controls.Add(this.btn_sourceImg);
            this.panel1.Location = new System.Drawing.Point(890, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 450);
            this.panel1.TabIndex = 5;
            // 
            // btnMorph
            // 
            this.btnMorph.Location = new System.Drawing.Point(24, 286);
            this.btnMorph.Name = "btnMorph";
            this.btnMorph.Size = new System.Drawing.Size(150, 50);
            this.btnMorph.TabIndex = 7;
            this.btnMorph.Text = "Morph";
            this.btnMorph.UseVisualStyleBackColor = true;
            this.btnMorph.Click += new System.EventHandler(this.BtnMorph_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Number of Frames";
            // 
            // txtNumberOfFrame
            // 
            this.txtNumberOfFrame.Location = new System.Drawing.Point(21, 247);
            this.txtNumberOfFrame.Name = "txtNumberOfFrame";
            this.txtNumberOfFrame.Size = new System.Drawing.Size(150, 22);
            this.txtNumberOfFrame.TabIndex = 5;
            this.txtNumberOfFrame.Text = "10";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 512);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.destImage);
            this.Controls.Add(this.sourceImage);
            this.Name = "Form1";
            this.Text = "Morphing Tool";
            this.Load += new System.EventHandler(this.EnableDrawing);
            ((System.ComponentModel.ISupportInitialize)(this.sourceImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.destImage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox sourceImage;
        public System.Windows.Forms.PictureBox destImage;
        private System.Windows.Forms.Button btn_sourceImg;
        private System.Windows.Forms.Button btn_DestImg;
        public System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnMorph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumberOfFrame;
    }
}

