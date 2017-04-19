using ManyWindows.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManyWindows
{
    public partial class Form1 : Form
    {

        List<Shape> shapes = new List<Shape>();

        public Form1()
        {
            InitializeComponent();
            shapes.Add(new Circle(000, 000, 100));
            shapes.Add(new Circle(100, 100, 90));
            shapes.Add(new RegularPolygon(6, 100, 500, 400));
            shapes.Add(new LineMy(300, 300, 350, 410));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {


            // GDI+

            // DirectDraw, OpenGL, Vulcan

            // game engines: ioquake, unity, SDL...

            // создаём холст - canvas
            Graphics g = this.CreateGraphics();
            g.Clear(SystemColors.Control);

            // рисуем все фигуры
            foreach (Shape s in this.shapes)
            {
                s.Paint(g);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Form1_Paint(sender, null);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            
            foreach (Shape s in this.shapes)
            {
                if (s.isInside(e.X, e.Y))
                {
                    this.Text = "Выбран элемент №"+s.Id;
                }

            }
        }
    }
}
