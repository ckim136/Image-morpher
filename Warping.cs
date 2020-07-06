using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Morphing;

namespace Morphing
{
    public class Warping
    {
        public float dotProduct(PointF p1, PointF p2)
        {
            float dp = (p1.X * p2.X) + (p1.Y * p2.Y);
            return dp;
        }
        public PointF calculateNormal(PointF PQ)
        {
            PointF normal = new PointF(PQ.Y * -1, PQ.X);
            return normal;
        }
        public float calculateLength(PointF QP)
        {
            float length = (float)Math.Sqrt(Math.Pow(QP.X, 2) + Math.Pow(QP.Y, 2));
            return length;
        }

        public float calculateLengthSquare(PointF QP)
        {
            float length = (float)(Math.Pow(QP.X, 2) + Math.Pow(QP.Y, 2));
            return length;
        }

        public float calculateV(PointF XP, PointF normal, float normalL)
        {
            
            float v = dotProduct(XP, normal) / normalL;
            return v;
        }



        public float calculateU(PointF XP, PointF QP)
        {
            float u = dotProduct(XP, QP) / calculateLengthSquare(QP);
            return u;
        }


        public PointF calculatePoint(PointF Pprime, float u, PointF QPPrime, float v, PointF normal, float normalLength)
        {
            float x = Pprime.X;
            float y = Pprime.Y;

            float xPrime = x + u * QPPrime.X +  v * normal.X / normalLength;
            float yPrime = y +u * QPPrime.Y  +   v * normal.Y / normalLength;

            return new PointF(xPrime, yPrime); ;
        }


        public PointF calculateVector(PointF P, PointF Q)
        {
            PointF vector = new PointF(Q.X - P.X, Q.Y - P.Y);
            return vector;
        }


        public float calculateWeight(int length, int p, int b, double a, float distance)
        {
            double w = Math.Pow((Math.Pow(length, p) / (a + Math.Abs(distance))), b);
            return (float)w;
        } 


        public PointF calculateSourcePoint(Line dest, Line src, PointF X)
        {
            PointF XP = calculateVector(X, dest.startPoint);
            PointF QP = calculateVector(dest.endPoint, dest.startPoint);


            float u = calculateU(XP, QP);

            PointF normal = calculateNormal(QP);
            float normalL = calculateLength(normal);

            float v = calculateV(XP, normal, normalL);

            PointF Pprime = src.startPoint;
            PointF QPrime = src.endPoint;
            PointF PQPrime = calculateVector(Pprime, QPrime);
            PointF PQPrimeNormal = calculateNormal(PQPrime);
            float PQNormalL = calculateLength(PQPrime);


            PointF sp = calculatePoint(Pprime, u, PQPrime, v, PQPrimeNormal, PQNormalL);
            return sp;
        }
    }
}
