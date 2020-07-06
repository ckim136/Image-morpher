using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Morphing
{
   public class Line
    {
        public PointF startPoint;
        public PointF endPoint;
        public Line duplicate;
        public bool isOriginal;
        public bool isClicked = false;
        public double x, y;
        public int distance;
       
       
        public Line(PointF s, PointF e)
        {
            startPoint = s;
            endPoint = e;
            isOriginal = true;
            x = endPoint.X - startPoint.X;
            y = endPoint.Y - startPoint.Y;
        }

     public Line(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void createDuplicate()
        {
            duplicate = new Line(startPoint, endPoint);
        }
 
  

        public int getLength()
        {
            return (int)Math.Sqrt(x * x + y * y);
        }
     
  
        public Line getDuplicate() { return duplicate; }
        public void setState(bool state) { isOriginal = state; }
    public bool getState() { return isOriginal; }

    }
}
