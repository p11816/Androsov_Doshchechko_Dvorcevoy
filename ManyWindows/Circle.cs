using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyWindows.Shapes
{
    class Circle: Shape
    {
        float radius;

        public Circle(float x, float y, float r)
        {
            point = new System.Drawing.PointF(x, y);
            radius = r;
        }
        public override void Paint(System.Drawing.Graphics g)
        {
            
            g.FillEllipse(brush, point.X,point.Y,radius*2,radius*2);
            g.DrawEllipse(pen, point.X, point.Y, radius * 2, radius * 2);
        }
        public override bool isInside(float X, float Y)
        {
            // координаты центра круга
            var center = new System.Drawing.PointF( point.X+radius, point.Y+radius);
            // координаты клика
            var click = new System.Drawing.PointF(X, Y);
            // если клик ближе, чем на радиус круга - то мы внутри круга

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
