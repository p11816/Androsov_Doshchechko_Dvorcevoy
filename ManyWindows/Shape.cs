using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyWindows.Shapes
{
    abstract public class Shape
    {
        public int Id { get; private set; }

        static private int lastId = 0;

        public static float Distance(PointF a, PointF b)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
        protected Shape()
        {
            point = new PointF(0, 0);
            pen = new Pen(Color.Pink);
            pen.Width = 5;
            brush = new SolidBrush(Color.Lime);
            lastId++;
            Id = lastId;
        }
        public abstract void Paint(Graphics g);
        // положение
        public PointF point;
        // контур
        public Pen pen;
        // заливка
        public Brush brush;

        public abstract bool isInside(float p1, float p2);
    }
}
