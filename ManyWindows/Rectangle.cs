using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ManyWindows.Shapes
{
    class RectangleMy : Shape
    {
        float width;
        float height;

        public RectangleMy(float x, float y, float width, float height)
        {
            point = new System.Drawing.PointF(x, y);
            this.width = width;
            this.height = height;
        }

        public override void Paint(System.Drawing.Graphics g)
        {
            g.FillRectangle(brush, point.X, point.Y, width, height);
            g.DrawRectangle(pen, point.X, point.Y, width, height);
        }

        public override bool isInside(float X, float Y)
        {
            var click = new PointF(X, Y);
            if (click.X > point.X && click.X <= point.X + width && click.Y > point.Y && click.Y <= point.Y + height)
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
