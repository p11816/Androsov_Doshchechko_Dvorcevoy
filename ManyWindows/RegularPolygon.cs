using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ManyWindows.Shapes
{
    class RegularPolygon : Shape
    {
        int nSide;          // количество сторон многоугольника
        float radius;         // радиус многоугольника
        PointF center;      // коор. центра многоугольника
        PointF[] p;         // точки многоугольника

        public RegularPolygon(int nSide, float radius, float X, float Y)
        {
            this.nSide = nSide;
            this.radius = radius;
            center.X = X;
            center.Y = Y;
        }

        // метод для вычисления точек многоугольника относительно введенных данных
        private void SearchPoint(float angle)
        {
            float sumAngle = 0;

            for(int i = 0; i < nSide; i++)
            {
                p[i].X = center.X + (int)(Math.Round(Math.Cos(sumAngle / 180 * Math.PI) * radius));
                p[i].Y = center.Y - (int)(Math.Round(Math.Sin(sumAngle / 180 * Math.PI) * radius));
                sumAngle += angle;
            }
        }


        public override void Paint(System.Drawing.Graphics g)
        {
            if (nSide < 0 || radius < 0)
            {
                throw new Exception("Uncorrect Data");
            }
            else
            {
                p = new PointF[nSide];
                SearchPoint((float)360 / nSide);
                g.DrawPolygon(pen, p);
                g.FillPolygon(brush, p);
            }
        }

        public override bool isInside(float X, float Y)
        {
            var click = new PointF(X,Y);
            if (Distance(center, click) <= radius)
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
