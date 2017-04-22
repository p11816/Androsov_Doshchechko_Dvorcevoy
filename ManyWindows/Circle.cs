using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyWindows.Shapes
{
    class Circle: Shape
    {
        public float Radius { get; private set; }

        public Circle(float x, float y, float r)
        {
            point = new System.Drawing.PointF(x, y);
            Radius = r;
        }
        public override void Paint(System.Drawing.Graphics g)
        {
            
            g.FillEllipse(brush, point.X,point.Y,Radius*2,Radius*2);
            g.DrawEllipse(pen, point.X, point.Y, Radius * 2, Radius * 2);
        }
        public override bool isInside(float X, float Y)
        {
            // координаты центра круга
            var center = new System.Drawing.PointF( point.X+Radius, point.Y+Radius);
            // координаты клика
            var click = new System.Drawing.PointF(X, Y);
            // если клик ближе, чем на радиус круга - то мы внутри круга

            if (Distance(center, click) <= Radius)
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
