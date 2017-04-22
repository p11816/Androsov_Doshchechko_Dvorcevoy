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
        Shape shape_focus = null;


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
                    shape_focus = s;
                    SolidBrush Buff_brush = (SolidBrush)shape_focus.brush;

                    panel1.BackColor = Buff_brush.Color;
                    panel2.BackColor = shape_focus.pen.Color;
                }
            }
        }


        //цвет фигуры--------------
        private void panel1_Click(object sender, EventArgs e)
        {

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                panel1.BackColor = colorDialog1.Color;

                if (shape_focus == null)
                    return;

                shape_focus.brush = new SolidBrush(colorDialog1.Color);

                this.Form1_Paint(sender, null);
            }
        }

        //цвет обводки-----------------
        private void panel2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                panel2.BackColor = colorDialog1.Color;

                if (shape_focus == null)
                    return;

                Pen new_pen = new Pen(colorDialog1.Color);
                new_pen.Width = shape_focus.pen.Width;
                shape_focus.pen = new_pen;

                this.Form1_Paint(sender, null);
            }
        }
    }
}
