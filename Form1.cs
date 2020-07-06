using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using static Morphing.Painting;


namespace Morphing
{
    public partial class Form1 : Form
    {
     Painting painting;
        public Form1()
        {
            
            InitializeComponent();
        }

        private void EnableDrawing(object sender, EventArgs e)
        {
             painting = new Painting(this);

            this.sourceImage.MouseDown += painting.getPosition;
            this.sourceImage.MouseMove += painting.mouseMove_notDown;
            this.sourceImage.Paint += painting.DrawLine;

            this.btnDelete.MouseClick += painting.deleteLine;

            this.destImage.MouseDown += painting.getPositionDuplicate;
            this.destImage.MouseMove += painting.mouseMove_notDownDuplicate;
            this.destImage.Paint += painting.DrawDuplicate;

        }

        private void Btn_sourceImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                sourceImage.Image = new Bitmap(op.FileName);
                sourceImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            }
        }

        private void BtnDestImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                destImage.Image = new Bitmap(op.FileName);
                destImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            }
        }


        public void BtnMorph_Click_1(object sender, EventArgs e)
        {
            List<Line> lines = painting.getLinesList();
            List<Line> duplicateLines = painting.getDuplicateLine();
            int num = Int32.Parse(txtNumberOfFrame.Text);
            Form2 form2 = new Form2(num, this, lines, duplicateLines) ;
            form2.Show();

        }
        
    }
}
