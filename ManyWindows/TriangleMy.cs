using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ManyWindows.Shapes
{
    class RegularTriangleMy : Shape
    {
        float side;           // длина стороны
        static int nSide = 3;
        public PointF[] p;
        float radius;

        public RegularTriangleMy(float X, float Y, float side)
        {
            point.X = X;        // центр треугольника вписаного в окружность
            point.Y = Y;
            this.side = side;
            radius = side / (float)Math.Sqrt(3);            // нашли длину радиуса
            p = new PointF[nSide];
        }

        // метод для вычисления точек треугольника равностороннего
        private void SearchPoint(float angle)
        {
            float sumAngle = 0;

            for (int i = 0; i < nSide; i++)
            {
                p[i].X = point.X + (int)(Math.Round(Math.Cos(sumAngle / 180 * Math.PI) * radius));
                p[i].Y = point.Y - (int)(Math.Round(Math.Sin(sumAngle / 180 * Math.PI) * radius));
                sumAngle += angle;
            }
        }

        public override void Paint(Graphics g)
        {
            SearchPoint((float)360 / nSide);
            g.DrawPolygon(pen, p);
            g.FillPolygon(brush, p);
        }

        public override bool isInside(float X, float Y)
        {
            var click = new PointF(X, Y);
            if (Distance(point, click) <= radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
