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
        public float Width { get; private set; }
        public float Height { get; private set; }

        public RectangleMy(float x, float y, float width, float height)
        {
            point = new PointF(x, y);
            this.Width = width;
            this.Height = height;
        }

        public override void Paint(System.Drawing.Graphics g)
        {
            g.FillRectangle(brush, point.X, point.Y, Width, Height);
            g.DrawRectangle(pen, point.X, point.Y, Width, Height);
        }

        public override bool isInside(float X, float Y)
        {
            var click = new PointF(X, Y);
            if (click.X > point.X && click.X <= point.X + Width && click.Y > point.Y && click.Y <= point.Y + Height)
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

