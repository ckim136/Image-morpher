using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Morphing
{
    class Painting
    {
        public List<Line> Lines = new List<Line>();
        public  List<Line> DuplicateLines = new List<Line>();
        private bool isDrawing = false;
        private Line new_Line, duplicate_Line;
        private PointF startP, endP;
        private readonly Form1 f;
        private bool MovingStartEndPoint = false;
        private float OffsetX, OffsetY;
        private int movingSegment = -1;
        private const int over_dist_squared = 5 * 5;
        private const int radius = 5;

        public Painting(Form1 form)
        {
            this.f = form;
        }


        public void mouseMove_notDown(object sender, MouseEventArgs e)
        {
            Cursor cursor = Cursors.Cross;

            PointF hit_point;
            int segment_number;
            if (MouseIsOverEndPoint(e.Location, out segment_number, out hit_point))
            {
                cursor = Cursors.Arrow;
            }
            else if (MouseIsOverSegment(e.Location, out segment_number))
            {
                cursor = Cursors.Hand;
            }
            if (f.sourceImage.Cursor != cursor)
            {
                f.sourceImage.Cursor = cursor;
            }
        }

        public void getPosition(object sender, MouseEventArgs e)
        {

            Warping w = new Warping();
            //PointF P = new PointF(5, 5);
            ////PointF Q = new PointF(1, 20);
            ////PointF T = new PointF(10, 10);
            //PointF PQ = new PointF(15, 10);
            ////PointF TP = w.calculateVector(T, P);
            ////PointF PT = w.calculateVector(P, T);
            //PointF normal = new PointF(-10, 15);
            //float normalLength = w.calculateNormalLength(normal);


            //float distance = 5;
            ////double fl = w.calculateFractionalLength(PQ, PT);
            ////double f = w.calculateFraction(fl, PQ);


            ////PointF P2 = new PointF(1, 40);
            ////PointF Q2 = new PointF(5, 1);
            ////PointF PQ2 = w.calculateVector(P2, Q2);
            ////PointF normal2 = w.calculateNormal(PQ2);
            ////double normal2L = w.calculateNormalLength(normal2);
            ////PointF fp = w.calculateFractionPoint(f, P2, PQ2);
            ////PointF sp = w.calculateSourcePoint(fp, distance, normal2.X, normal2.Y, normal2L);


            ////double weight = w.calculateWeight(1, 0, 1, 0.01, distance);
            ////double deltaX = w.calculateDelta(sp.X, weight);
            ////double deltaY = w.calculateDelta(sp.Y, weight);

            //PointF srcPoint = w.calculateSourcePoint(P, (float)0.6, PQ, distance, normal, normalLength);

            //List<List<List<PointF>>> myList = new List<List<List<PointF>>>();
            
            
            //for(int x = 0; x<5; x++)
            //{
            //    List<List<PointF>> subList = new List<List<PointF>>();
            //    for(int y = 0; y<15; y++)
            //    {
            //        List<PointF> subSubList = new List<PointF>();
            //        for(int z = 0; z<15; z++)
            //        {
            //            subSubList.Add(new PointF(0, 0));
            //        }
            //        subList.Add(subSubList);
            //    }
            //    myList.Add(subList);
            //}
            //PointF first = new PointF(12, 12);
            //PointF second = new PointF(25, 14);
            //PointF third = new PointF(4, 44);

            //myList[0][8][10] = first;
            //myList[1][8][10] = second;
            //myList[2][8][10] = third;

            //for (int x = 0; x < 5; x++)
            //{
            //    for (int y = 0; y < 15; y++)
            //    {
            //        for (int z = 0; z< 15; z++)
            //        {
            //            if (x == 0 && y == 8 && z == 10)
            //            {
            //                myList[0][8][10]= (new PointF(12, 12));
            //            }
            //            if (x == 1 && y == 8 && z == 10)
            //            {
            //                myList[1][8][10]= (new PointF(25, 14));
            //            }
            //            if (x == 2 && y == 8 && z == 10)
            //            {
            //                myList[0][8][10] = (new PointF(4, 44));
            //            }

            //        }
            //    }

            //}

            //List<Line> lines = new List<Line>();
            //lines.Add(new Line(0, 0));
            //lines.Add(new Line(0, 0));
            //lines.Add(new Line(0, 0));
            //w.warpingCalculationMultipleLine(8, 10, myList, lines  );


            PointF hit_point;
            int segment_number;

            if (MouseIsOverEndPoint(e.Location, out segment_number, out hit_point))
            {
                f.sourceImage.MouseMove -= mouseMove_notDown;
                f.sourceImage.MouseMove += mouseMove_moveEndPoint;
                f.sourceImage.MouseUp += mouseUp_moveEndPoint;

                movingSegment = segment_number;

                MovingStartEndPoint = ((Lines[segment_number].startPoint).Equals(hit_point));

                OffsetX = hit_point.X - e.X;
                OffsetY = hit_point.Y - e.Y;

            }
            else if (MouseIsOverSegment(e.Location, out segment_number))
            {

                f.sourceImage.MouseMove -= mouseMove_notDown;
                f.sourceImage.MouseMove += mouseMove_moveSegment;
                f.sourceImage.MouseUp += mouseUp_moveSegment;

                movingSegment = segment_number;

                OffsetX = Lines[segment_number].startPoint.X - e.X;
                OffsetY = Lines[segment_number].startPoint.Y - e.Y;

            }
            else
            {
                f.sourceImage.MouseMove -= mouseMove_notDown;
                f.sourceImage.MouseMove += StartDrawing;
                f.sourceImage.MouseUp += EndDrawing;

                isDrawing = true;
                startP = new PointF(e.X, e.Y);
                endP = new PointF(e.X, e.Y);
                new_Line = new Line(startP, endP);
                new_Line.setState(true);
                new_Line.createDuplicate();
                duplicate_Line = new_Line.getDuplicate();

            }
        }

        public void StartDrawing(object sender, MouseEventArgs e)
        {

            endP = new PointF(e.X, e.Y);
            new_Line = new Line(startP, endP);
            duplicate_Line = new Line(startP, endP);

            f.sourceImage.Invalidate();
            if (new_Line.getState())
            {
                f.destImage.Invalidate();

            }

        }

        public void EndDrawing(object sender, MouseEventArgs e)
        {
            isDrawing = false;
            f.sourceImage.MouseMove += mouseMove_notDown;
            f.sourceImage.MouseMove -= StartDrawing;
            f.sourceImage.MouseUp -= EndDrawing;

            Lines.Add(new_Line);
            if (new_Line.getState())
            {
                DuplicateLines.Add(duplicate_Line);
                f.destImage.Invalidate();
            }
            f.sourceImage.Invalidate();



        }

        public void StartDrawingDuplicate(object sender, MouseEventArgs e)
        {

            endP = new PointF(e.X, e.Y);
            new_Line = new Line(startP, endP);
            duplicate_Line = new Line(startP, endP);

            //f.sourceImage.Invalidate();
            //if (new_Line.getState())
            //{
                f.destImage.Invalidate();

            //}

        }
        public void EndDrawingDuplicate(object sender, MouseEventArgs e)
        {
            isDrawing = false;
            f.destImage.MouseMove += mouseMove_notDown;
            f.destImage.MouseUp -= EndDrawingDuplicate;

            f.destImage.Invalidate();

        }
        public void mouseMove_moveEndPoint(object sender, MouseEventArgs e)
        {
            if (MovingStartEndPoint)
            {
                Lines[movingSegment].startPoint = new PointF(e.X + OffsetX, e.Y + OffsetY);
            }
            else
            {
                Lines[movingSegment].endPoint = new PointF(e.X + OffsetX, e.Y + OffsetY);
            }
            Lines[movingSegment].setState(false);

            f.sourceImage.Invalidate();

        }
        public void mouseUp_moveEndPoint(object sender, MouseEventArgs e)
        {
            f.sourceImage.MouseMove += mouseMove_notDown;
            f.sourceImage.MouseMove -= mouseMove_moveEndPoint;
            f.sourceImage.MouseUp += mouseUp_moveEndPoint;

            f.sourceImage.Invalidate();
        }

        public void deleteLine(object sender, MouseEventArgs e)
        {
            int x = movingSegment;
            Lines.RemoveAt(movingSegment);
            DuplicateLines.RemoveAt(movingSegment);
            f.sourceImage.Invalidate();
            f.destImage.Invalidate();


            return;

        }

        public float FindDistanceToPoint(PointF p1, PointF p2)
        {
            float dx = p1.X - p2.X;
            float dy = p1.Y - p2.Y;
            return dx * dx + dy * dy;
        }

        public double FindDistanceToSegment(PointF p, PointF p1, PointF p2, out PointF closest)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            if ((dx == 0) && (dy == 0))
            {
                closest = p1;
                dx = p.X - p1.X;
                dy = p.Y - p1.Y;
                return dx * dx + dy * dy;
            }
            // Calculate the t that minimizes the distance.
            float t = ((p.X - p1.X) * dx + (p.Y - p1.Y) * dy) / (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a PointF in the middle.
            if (t < 0)
            {
                closest = new PointF(p1.X, p1.Y);
                dx = p.X - p1.X;
                dy = p.Y - p1.Y;
            }
            else if (t > 1)
            {
                closest = new PointF(p2.X, p2.Y);
                dx = p.X - p2.X;
                dy = p.Y - p2.Y;
            }
            else
            {
                closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
                dx = p.X - closest.X;
                dy = p.Y - closest.Y;
            }

            return dx * dx + dy * dy;
        }

        public bool MouseIsOverEndPoint(PointF mouse_point, out int segment_number,
            out PointF hit_point)
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                if (FindDistanceToPoint(mouse_point, Lines[i].startPoint) < over_dist_squared)
                {
                    segment_number = i;
                    hit_point = Lines[i].startPoint;
                    return true;
                }

                if (FindDistanceToPoint(mouse_point, Lines[i].endPoint) < over_dist_squared)
                {
                    segment_number = i;
                    hit_point = Lines[i].endPoint;
                    return true;
                }


            }
            segment_number = -1;
            hit_point = new PointF(-1, -1);
            return false;

        }

        public bool MouseIsOverSegment(PointF mouse_point, out int segment_number)
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                PointF closest;
                if (FindDistanceToSegment(mouse_point, Lines[i].startPoint, Lines[i].endPoint, out closest) < over_dist_squared)
                {
                    segment_number = i;
                    return true;
                }

            }
            segment_number = -1;

            return false;
        }


        public void mouseMove_moveSegment(object sender, MouseEventArgs e)
        {
            float new_x1 = e.X + OffsetX;
            float new_y1 = e.Y + OffsetY;

            float dx = new_x1 - Lines[movingSegment].startPoint.X;
            float dy = new_y1 - Lines[movingSegment].startPoint.Y;

            if (dx == 0 && dy == 0) { return; }

            Lines[movingSegment] = new Line(new PointF(new_x1, new_y1), new PointF(Lines[movingSegment].endPoint.X + dx, Lines[movingSegment].endPoint.Y + dy));
            Lines[movingSegment].setState(false);

            f.sourceImage.Invalidate();


        }

        public void mouseUp_moveSegment(object sender, MouseEventArgs e)
        {
            f.sourceImage.MouseMove += mouseMove_notDown;
            f.sourceImage.MouseMove -= mouseMove_moveSegment;
            f.sourceImage.MouseUp -= mouseUp_moveEndPoint;
            f.btnDelete.Enabled = true;

            f.sourceImage.Invalidate();


        }

        public void DrawLine(object sender, PaintEventArgs e)
        {
            for(int i = 0; i< Lines.Count; i++)
            {
                PointF start = Lines[i].startPoint;
                PointF end = Lines[i].endPoint;


                RectangleF startEnd = new RectangleF(start.X - radius, start.Y - radius, 2 * radius + 1, 2 * radius + 1);
                RectangleF endEnd = new RectangleF(end.X - radius, end.Y - radius, 2 * radius + 1, 2 * radius + 1);


                e.Graphics.DrawLine(new Pen(Color.Black, 2), start, end);
             
                e.Graphics.FillEllipse(Brushes.White, startEnd);
                e.Graphics.DrawEllipse(Pens.Black, startEnd);
               
                e.Graphics.FillEllipse(Brushes.White, endEnd);
                e.Graphics.DrawEllipse(Pens.Black, endEnd);


            }
         
        }


        public void mouseMove_notDownDuplicate(object sender, MouseEventArgs e)
        {
            Cursor cursor = Cursors.Cross;

            PointF hit_point;
            int segment_number;
            if (MouseIsOverEndPointDuplicate(e.Location, out segment_number, out hit_point))
            {
                cursor = Cursors.Arrow;
            }
            else if (MouseIsOverSegmentDuplicate(e.Location, out segment_number))
            {
                cursor = Cursors.Hand;
            }
            if (f.destImage.Cursor != cursor)
            {
                f.destImage.Cursor = cursor;
            }


        }
        public void mouseMove_moveEndPointDuplicate(object sender, MouseEventArgs e)
        {
            if (MovingStartEndPoint)
            {
                DuplicateLines[movingSegment].startPoint = new PointF(e.X + OffsetX, e.Y + OffsetY);
            }
            else
            {
                DuplicateLines[movingSegment].endPoint = new PointF(e.X + OffsetX, e.Y + OffsetY);
            }
            DuplicateLines[movingSegment].setState(false);

            f.destImage.Invalidate();




        }
        public void mouseUp_moveEndPointDuplicate(object sender, MouseEventArgs e)
        {
            f.destImage.MouseMove += mouseMove_notDown;
            f.destImage.MouseMove -= mouseMove_moveEndPointDuplicate;
            f.destImage.MouseUp += mouseUp_moveEndPointDuplicate;

            f.destImage.Invalidate();
        }

        public void mouseMove_moveSegmentDuplicate(object sender, MouseEventArgs e)
        {
            float new_x1 = e.X + OffsetX;
            float new_y1 = e.Y + OffsetY;

            float dx = new_x1 - DuplicateLines[movingSegment].startPoint.X;
            float dy = new_y1 - DuplicateLines[movingSegment].startPoint.Y;

            if (dx == 0 && dy == 0) { return; }

            DuplicateLines[movingSegment] = new Line(new PointF(new_x1, new_y1), new PointF(DuplicateLines[movingSegment].endPoint.X + dx, DuplicateLines[movingSegment].endPoint.Y + dy));
            DuplicateLines[movingSegment].setState(false);

            f.destImage.Invalidate();


        }

        public void mouseUp_moveSegmentDuplicate(object sender, MouseEventArgs e)
        {
            f.destImage.MouseMove += mouseMove_notDown;
            f.destImage.MouseMove -= mouseMove_moveSegmentDuplicate;
            f.destImage.MouseUp -= mouseUp_moveEndPointDuplicate;
            f.btnDelete.Enabled = true;

            f.destImage.Invalidate();


        }
        public void getPositionDuplicate(object sender, MouseEventArgs e)
        {

            PointF hit_point;
            int segment_number;

            if (MouseIsOverEndPointDuplicate(e.Location, out segment_number, out hit_point))
            {
                f.destImage.MouseMove -= mouseMove_notDown;
                f.destImage.MouseMove += mouseMove_moveEndPointDuplicate;
                f.destImage.MouseUp += mouseUp_moveEndPointDuplicate;

                movingSegment = segment_number;

                MovingStartEndPoint = ((DuplicateLines[segment_number].startPoint).Equals(hit_point));

                OffsetX = hit_point.X - e.X;
                OffsetY = hit_point.Y - e.Y;
                 
            }
            else if (MouseIsOverSegmentDuplicate(e.Location, out segment_number))
            {

                f.destImage.MouseMove -= mouseMove_notDown;
                f.destImage.MouseMove += mouseMove_moveSegmentDuplicate;
                f.destImage.MouseUp += mouseUp_moveSegmentDuplicate;

                movingSegment = segment_number;

                OffsetX = DuplicateLines[segment_number].startPoint.X - e.X;
                OffsetY = DuplicateLines[segment_number].startPoint.Y - e.Y;
            }
            else
            {
                f.destImage.MouseMove -= mouseMove_notDown;
                f.destImage.MouseMove += StartDrawingDuplicate;
                f.destImage.MouseUp += EndDrawingDuplicate;


                isDrawing = true;
                startP = new PointF(e.X, e.Y);
                endP = new PointF(e.X, e.Y);
                new_Line = new Line(startP, endP);
                new_Line.setState(true);
                new_Line.createDuplicate();
                duplicate_Line = new_Line.getDuplicate();

            }

        }
       
        public bool MouseIsOverEndPointDuplicate(PointF mouse_point, out int segment_number,
          out PointF hit_point)
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                if (FindDistanceToPoint(mouse_point, DuplicateLines[i].startPoint) < over_dist_squared)
                {
                    segment_number = i;
                    hit_point = DuplicateLines[i].startPoint;
                    return true;
                }

                if (FindDistanceToPoint(mouse_point, DuplicateLines[i].endPoint) < over_dist_squared)
                {
                    segment_number = i;
                    hit_point = DuplicateLines[i].endPoint;
                    return true;
                }


            }
            segment_number = -1;
            hit_point = new PointF(-1, -1);
            return false;

        }

        public bool MouseIsOverSegmentDuplicate(PointF mouse_point, out int segment_number)
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                PointF closest;
                if (FindDistanceToSegment(mouse_point, DuplicateLines[i].startPoint, DuplicateLines[i].endPoint, out closest) < over_dist_squared)
                {
                    segment_number = i;
                    return true;
                }

            }
            segment_number = -1;

            return false;
        }

        public void DrawDuplicate(object sender, PaintEventArgs e)
    {
        for (int i = 0; i < DuplicateLines.Count; i++)
        {
            PointF start = DuplicateLines[i].startPoint;
            PointF end = DuplicateLines[i].endPoint;


            RectangleF startEnd = new RectangleF(start.X - radius, start.Y - radius, 2 * radius + 1, 2 * radius + 1);
            RectangleF endEnd = new RectangleF(end.X - radius, end.Y - radius, 2 * radius + 1, 2 * radius + 1);


            e.Graphics.DrawLine(new Pen(Color.Black, 2), start, end);

            e.Graphics.FillEllipse(Brushes.White, startEnd);
            e.Graphics.DrawEllipse(Pens.Black, startEnd);

            e.Graphics.FillEllipse(Brushes.White, endEnd);
            e.Graphics.DrawEllipse(Pens.Black, endEnd);


        }

    }

        public List<Line> getLinesList() { return Lines; }
        public List<Line> getDuplicateLine() { return DuplicateLines; }
    }
   



}

