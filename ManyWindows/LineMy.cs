using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ManyWindows.Shapes
{
    class LineMy : Shape
    {
        PointF pointSecond;
        public LineMy(float Xfirst, float Yfirst, float Xsecond, float Ysecond)
        {
            pen = new Pen(Color.Green, 5);
            point.X = Xfirst;
            point.Y = Yfirst;
            pointSecond.X = Xsecond;
            pointSecond.Y = Ysecond;
        }

        public override void Paint(Graphics g)
        {
            g.DrawLine(pen, point, pointSecond);
            g.DrawArc(pen, 200, 200, 400, 200, 0, 360
                );
        }

        public override bool isInside(float p1, float p2) { return false; }
    }
}
