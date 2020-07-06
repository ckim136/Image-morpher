using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Morphing
{
    public partial class Form2 : Form
    {
        int numberOfFrames;
        PictureBox[] pics;
        int pictureBoxSize = 500;

        Form1 f;
        List<Line> Lines;
        List<Line> DuplicateLine;
        List<Bitmap> intermediateFrame = new List<Bitmap>();
        List<Bitmap> intermediateFrameReverse = new List<Bitmap>();
        List<Bitmap> finalFrames = new List<Bitmap>();
        Bitmap srcImg, destImg;
        List<Line> intermediateLines;
        Warping w;

        public Form2(int n, Form1 form1, List<Line> l, List<Line> dl)
        {
            numberOfFrames = n;
            f = form1;
            Lines = l;
            DuplicateLine = dl;
            InitializeComponent();
        }
        public void setUpPage(object sender, System.EventArgs e)
        {
            w = new Warping();
            lbNumberOfFrames.Text = numberOfFrames.ToString();
            pics = new PictureBox[numberOfFrames + 2];
            srcImg = (Bitmap)f.sourceImage.Image;
            destImg = (Bitmap)f.destImage.Image;

            for (int i = 0; i < numberOfFrames + 2; ++i)
            {
                pics[i] = new PictureBox();
                pics[i].BackColor = Color.White;
                pics[i].Size = new Size(pictureBoxSize, pictureBoxSize);
                pics[i].SizeMode = PictureBoxSizeMode.CenterImage;
                pics[i].Location = new Point(i * pictureBoxSize, 100);
             
                this.Controls.Add(pics[i]);
                this.AutoScroll = true;
            }


            calculateIntermediateLines();
        
            for(int frame = 0; frame<numberOfFrames+1; frame++)
            {
                 createIntermediateFrames(frame);
                createIntermediateFramesReverse(frame);
            }
            createFinalFrame();
            drawFrames();


    

        }
        public void drawFrames()
        {

            pics[0].Image = srcImg;
            for (int i = 1; i< numberOfFrames+1; i++)
            {
                pics[i].Image = finalFrames[i - 1];
                this.Controls.Add(pics[i]);
            }
            pics[numberOfFrames + 1].Image = destImg;
        }
        public void calculateIntermediateLines()
        {
            intermediateLines = new List<Line>();
            for(int i = 0; i< Lines.Count; i++)
            {
                Line srcLine = Lines[i];
                Line destLine = DuplicateLine[i];
                float startX, startY, endX, endY;
                startX = (destLine.startPoint.X - srcLine.startPoint.X) / (numberOfFrames + 1);
                startY = (destLine.startPoint.Y - srcLine.startPoint.Y) / (numberOfFrames + 1);

                endX = (destLine.endPoint.X - srcLine.endPoint.X) / (numberOfFrames + 1);
                endY = (destLine.endPoint.Y - srcLine.endPoint.Y) / (numberOfFrames + 1);

                PointF s = new PointF(startX, startY);
                PointF e = new PointF(endX, endY);

                Line inter = new Line(s, e);

                intermediateLines.Add(inter);
            }

        }

        public PointF adjustPixel(PointF sourcePoint)
        {
            if (sourcePoint.X < 0)
            {
                sourcePoint.X = 0;
            }
            if (sourcePoint.X >= srcImg.Width)
            {
                sourcePoint.X = srcImg.Width - 1;
            }
            if (sourcePoint.Y < 0)
            {
                sourcePoint.Y = 0;
            }
            if (sourcePoint.Y >= srcImg.Height)
            {
                sourcePoint.Y = srcImg.Height - 1;
            }

            return sourcePoint;
        }
        PointF multiplyDWeight(PointF D, float w)
        {
            return new PointF(D.X * w, D.Y* w);
        }
        PointF divideDWeight(PointF D, float w)
        {
            return new PointF(D.X / w, D.Y / w);
        }
        public void createIntermediateFramesReverse(int frame)
        {
            Bitmap intermediateImg = new Bitmap(srcImg.Width, srcImg.Height);
            for (int pixelW = 0; pixelW < destImg.Width; pixelW++)
            {
                for (int pixelH = 0; pixelH < destImg.Height; pixelH++)
                {
                    PointF currentPoint = new PointF(pixelW, pixelH);
                    PointF DSUM = new PointF(0, 0);
                    float weightSum = 0;
                    for (int line = 0; line < Lines.Count; line++)
                    {
                        Line srcLine = DuplicateLine[line];
                        float startX, startY, endX, endY;
                        startX = srcLine.startPoint.X - intermediateLines[line].startPoint.X * (1 + frame);
                        startY = srcLine.startPoint.Y - intermediateLines[line].startPoint.Y * (1 + frame);
                        endX = srcLine.endPoint.X - intermediateLines[line].endPoint.X * (1 + frame);
                        endY = srcLine.endPoint.Y - intermediateLines[line].endPoint.Y * (1 + frame);

                        PointF s = new PointF(startX, startY);
                        PointF e = new PointF(endX, endY);
                        Line destLine = new Line(s, e);



                        PointF XP = w.calculateVector(currentPoint, destLine.startPoint);
                        PointF QP = w.calculateVector(destLine.endPoint, destLine.startPoint);


                        float u = w.calculateU(XP, QP);

                        PointF normal = w.calculateNormal(QP);
                        float normalL = w.calculateLength(normal);

                        float v = w.calculateV(XP, normal, normalL);

                        PointF Pprime = srcLine.startPoint;
                        PointF QPrime = srcLine.endPoint;
                        PointF PQPrime = w.calculateVector(Pprime, QPrime);
                        PointF PQPrimeNormal = w.calculateNormal(PQPrime);
                        float PQNormalL = w.calculateLength(PQPrime);


                        PointF sp = w.calculatePoint(Pprime, u, PQPrime, v, PQPrimeNormal, PQNormalL);

                        PointF displacement = w.calculateVector(currentPoint, sp);

                        float dist = 0;
                        if (u > 0 && u < 1)
                        {
                            dist = v;
                        }
                        if (u < 0)
                        {
                            PointF PX = w.calculateVector(destLine.startPoint, currentPoint);
                            dist = w.calculateLength(PX);
                        }
                        if (u > 1)
                        {
                            PointF QX = w.calculateVector(destLine.endPoint, currentPoint);
                            dist = w.calculateLength(QX);
                        }

                        float weight = w.calculateWeight(destLine.getLength(), 0, 2, 0.01, dist);

                        PointF DWeight = multiplyDWeight(displacement, weight);
                        DSUM = new PointF(DSUM.X + DWeight.X, DSUM.Y + DWeight.Y);

                        weightSum += weight;

                    }
                    PointF totalDelta = divideDWeight(DSUM, weightSum);
                    PointF sourcePoint = adjustPixel(new PointF(currentPoint.X + totalDelta.X, currentPoint.Y + totalDelta.Y));
                    intermediateImg.SetPixel(pixelW, pixelH, destImg.GetPixel((int)sourcePoint.X, (int)sourcePoint.Y));
                }
            }
            intermediateFrameReverse.Add(intermediateImg);


        }

   
        public void createIntermediateFrames(int frame)
        {
            Bitmap intermediateImg = new Bitmap(srcImg.Width, srcImg.Height);
            for(int pixelW = 0; pixelW<destImg.Width; pixelW++)
            {
                for(int pixelH = 0; pixelH<destImg.Height; pixelH++)
                {
                    PointF currentPoint = new PointF(pixelW, pixelH);
                    PointF DSUM = new PointF(0, 0);
                    float weightSum = 0;
                    for(int line = 0; line< Lines.Count; line++)
                    {
                        Line srcLine = Lines[line];
                        float startX, startY, endX, endY;
                        startX = srcLine.startPoint.X + intermediateLines[line].startPoint.X * (1+frame);
                        startY = srcLine.startPoint.Y + intermediateLines[line].startPoint.Y * (1+frame);
                        endX = srcLine.endPoint.X + intermediateLines[line].endPoint.X * (1 + frame);
                        endY = srcLine.endPoint.Y + intermediateLines[line].endPoint.Y * (1 + frame);

                        PointF s = new PointF(startX, startY);
                        PointF e = new PointF(endX, endY);
                        Line destLine = new Line(s, e);

                      

                        PointF XP = w.calculateVector(currentPoint, destLine.startPoint);
                        PointF QP = w.calculateVector(destLine.endPoint, destLine.startPoint);


                        float u = w.calculateU(XP, QP);

                        PointF normal = w.calculateNormal(QP);
                        float normalL = w.calculateLength(normal);

                        float v = w.calculateV(XP, normal, normalL);

                        PointF Pprime = srcLine.startPoint;
                        PointF QPrime = srcLine.endPoint;
                        PointF PQPrime = w.calculateVector(Pprime, QPrime);
                        PointF PQPrimeNormal = w.calculateNormal(PQPrime);
                        float PQNormalL = w.calculateLength(PQPrime);


                        PointF sp =  w.calculatePoint(Pprime, u, PQPrime, v, PQPrimeNormal, PQNormalL);

                        PointF displacement = w.calculateVector(currentPoint, sp);
                      
                      
                        float dist = 0;
                        if(u>0 && u < 1)
                        {
                            dist = v;
                        }
                        if (u < 0)
                        {
                            PointF PX = w.calculateVector(destLine.startPoint, currentPoint);
                            dist = w.calculateLength(PX);
                        }
                        if (u > 1)
                        {
                            PointF QX = w.calculateVector(destLine.endPoint, currentPoint);
                            dist = w.calculateLength(QX);
                        }

                        float weight = w.calculateWeight(destLine.getLength(), 0, 2, 0.01, dist);

                        PointF DWeight = multiplyDWeight(displacement, weight);
                        DSUM = new PointF(DSUM.X + DWeight.X, DSUM.Y + DWeight.Y);

                        weightSum += weight;

                    }
                    PointF totalDelta = divideDWeight(DSUM, weightSum);
                    PointF sourcePoint = adjustPixel(new PointF(currentPoint.X + totalDelta.X, currentPoint.Y + totalDelta.Y));
                    intermediateImg.SetPixel(pixelW, pixelH, srcImg.GetPixel((int)sourcePoint.X, (int)sourcePoint.Y));
                }
            }
            intermediateFrame.Add(intermediateImg);
        }


        private Color crossDissolve(Color src, Color dest, double amount)

        {
            byte a = (byte)((src.A * amount) + dest.A * (1 - amount));
            byte r = (byte)((src.R * amount) + dest.R * (1 - amount));
            byte g = (byte)((src.G * amount) + dest.G * (1 - amount));
            byte b = (byte)((src.B * amount) + dest.B * (1 - amount));
            return Color.FromArgb(a, r, g, b);

        }
        private static DateTime Delay(int sec)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, sec);
            DateTime Afterwards = ThisMoment.Add(duration);

            while (Afterwards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
        
            for (int i = 0; i < finalFrames.Count; i++)
            {
                pics[0].Image = finalFrames[i];
                Delay(600);

            }
            pics[0].Image = destImg;
          
        }
    

        public void createFinalFrame()
        {
            for(int i = 0; i<intermediateFrame.Count; i++)
            {
                Bitmap finalframe = new Bitmap(srcImg.Width, srcImg.Height);

                for(int pixelW = 0; pixelW< srcImg.Width; pixelW++)
                {
                    for(int pixelH = 0; pixelH<srcImg.Height; pixelH++)
                    {
                        Color src = intermediateFrame[i].GetPixel(pixelW, pixelH);
                        Color dest = intermediateFrameReverse[intermediateFrame.Count - 1 - i].GetPixel(pixelW, pixelH);
                        double amount = ((double)intermediateFrame.Count - i-1) / (double)intermediateFrame.Count;

                        finalframe.SetPixel(pixelW, pixelH, crossDissolve(src, dest, amount));
                    }
                }
                finalFrames.Add(finalframe);
            }
        }

    }
  
}
