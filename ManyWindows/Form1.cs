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
using System.Xml;

namespace ManyWindows
{
    public partial class Form1 : Form
    {

        List<Shape> shapes = new List<Shape>();

        public Form1()
        {
            InitializeComponent();
            shapes.Add(new Circle(000, 000, 100));
            shapes.Add(new RectangleMy(20, 400, 300, 150));
            shapes.Add(new RegularPolygon(6, 100, 500, 400));
            shapes.Add(new LineMy(300, 300, 350, 410));
            shapes.Add(new RegularTriangleMy(300, 200, 150));
        }
       
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // GDI+

            // DirectDraw, OpenGL, Vulcan

            // game engines: ioquake, unity, SDL...

            // создаём холст - canvas
            Graphics g = panel1.CreateGraphics();
            g.Clear(SystemColors.Control);

            // рисуем все фигуры
            foreach (Shape s in this.shapes)
            {
                s.Paint(g);
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            this.panel1_Paint(sender, null);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (Shape s in this.shapes)
            {
                if (s.isInside(e.X, e.Y))
                {
                    this.Text = "Выбран элемент №" + s.Id;
                }

            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        // событие для соханиния svg файла
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // создаем класс для сохранения
            SaveFileDialog saving = new SaveFileDialog();
            saving.Filter = "XML File|*.xml";
            if (saving.ShowDialog() == DialogResult.OK)
            {
                string path = saving.FileName;
                // сначала создадим xml файл
                XmlTextWriter textWriter = new XmlTextWriter(path, Encoding.UTF8);
                // создаем заголовок xml
                textWriter.WriteStartDocument();
                textWriter.WriteStartElement("root");
                textWriter.WriteEndElement();
                textWriter.Close();

                // загружаем наш файл и редактиркем
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);
                XmlElement xRoot = xDoc.DocumentElement;
                // создаем новый елемент svg
                XmlElement svgElem = xDoc.CreateElement("svg");
                // создаем атрибуты для svg
                XmlAttribute widthAttr = xDoc.CreateAttribute("width");
                XmlAttribute heightAttr = xDoc.CreateAttribute("height");
                // задаем значения  атрибутам
                XmlText widthText = xDoc.CreateTextNode(panel1.Size.Width.ToString());
                XmlText heightText = xDoc.CreateTextNode(panel1.Size.Height.ToString());
                // добавляем узлы
                widthAttr.AppendChild(widthText);
                heightAttr.AppendChild(heightText);

                svgElem.Attributes.Append(widthAttr);
                svgElem.Attributes.Append(heightAttr);

                xRoot.AppendChild(svgElem);

                /////////

                // обход нашей коллекции из обьектов
                foreach (Shape objects in shapes)
                {
                    if (objects is Circle)
                    {
                        // создадим элемент круг и необходимые атрибуты
                        XmlElement circleElem = xDoc.CreateElement("circle");
                        XmlAttribute cxAttr = xDoc.CreateAttribute("cx");
                        XmlAttribute cyAttr = xDoc.CreateAttribute("cy");
                        XmlAttribute rAttr = xDoc.CreateAttribute("r");
                        XmlAttribute strokeAttr = xDoc.CreateAttribute("stroke");
                        XmlAttribute stroke_widthAttr = xDoc.CreateAttribute("stroke-width");
                        XmlAttribute fillAttr = xDoc.CreateAttribute("fill");
                        // заполним атрибуты
                        XmlText cxText = xDoc.CreateTextNode(((objects.point.X) + ((Circle)objects).Radius).ToString());
                        XmlText cyText = xDoc.CreateTextNode(((objects.point.Y) + ((Circle)objects).Radius).ToString());
                        XmlText rText = xDoc.CreateTextNode(((Circle)objects).Radius.ToString());
                        XmlText strokeText = xDoc.CreateTextNode(objects.pen.Color.Name);
                        XmlText Stroke_widthText = xDoc.CreateTextNode(objects.pen.Width.ToString());
                        XmlText fillText = xDoc.CreateTextNode(((SolidBrush)objects.brush).Color.Name);
                        // добавим узлы(сначала текст)
                        cxAttr.AppendChild(cxText);
                        cyAttr.AppendChild(cyText);
                        rAttr.AppendChild(rText);
                        strokeAttr.AppendChild(strokeText);
                        stroke_widthAttr.AppendChild(Stroke_widthText);
                        fillAttr.AppendChild(fillText);
                        // теперь сами атрибуты
                        circleElem.Attributes.Append(cxAttr);
                        circleElem.Attributes.Append(cyAttr);
                        circleElem.Attributes.Append(rAttr);
                        circleElem.Attributes.Append(strokeAttr);
                        circleElem.Attributes.Append(stroke_widthAttr);
                        circleElem.Attributes.Append(fillAttr);
                        // и добавляем елемен круг в svg
                        svgElem.AppendChild(circleElem);
                    }
                    else if (objects is RectangleMy)
                    {
                        // создаем елемент и атрибуты
                        XmlElement rectElem = xDoc.CreateElement("rect");
                        XmlAttribute xAttr = xDoc.CreateAttribute("x");
                        XmlAttribute yAttr = xDoc.CreateAttribute("y");
                        XmlAttribute widhAttr = xDoc.CreateAttribute("width");
                        XmlAttribute heighAttr = xDoc.CreateAttribute("height");
                        XmlAttribute strokeAttr = xDoc.CreateAttribute("stroke");
                        XmlAttribute stroke_widthAttr = xDoc.CreateAttribute("stroke-width");
                        XmlAttribute fillAttr = xDoc.CreateAttribute("fill");
                        // создаем текст для атрибутов
                        XmlText xText = xDoc.CreateTextNode(objects.point.X.ToString());
                        XmlText yText = xDoc.CreateTextNode(objects.point.Y.ToString());
                        XmlText widhText = xDoc.CreateTextNode(((RectangleMy)objects).Width.ToString());
                        XmlText heighText = xDoc.CreateTextNode(((RectangleMy)objects).Height.ToString());
                        XmlText strokeText = xDoc.CreateTextNode(objects.pen.Color.Name);
                        XmlText stroke_widthText = xDoc.CreateTextNode(objects.pen.Width.ToString());
                        XmlText fillText = xDoc.CreateTextNode(((SolidBrush)objects.brush).Color.Name);
                        // добаввляем текст в атрибуты в элемент
                        xAttr.AppendChild(xText);
                        yAttr.AppendChild(yText);
                        widhAttr.AppendChild(widhText);
                        heighAttr.AppendChild(heighText);
                        strokeAttr.AppendChild(strokeText);
                        stroke_widthAttr.AppendChild(stroke_widthText);
                        fillAttr.AppendChild(fillText);
                        // атриббуты в элемнет
                        rectElem.Attributes.Append(xAttr);
                        rectElem.Attributes.Append(yAttr);
                        rectElem.Attributes.Append(widhAttr);
                        rectElem.Attributes.Append(heighAttr);
                        rectElem.Attributes.Append(strokeAttr);
                        rectElem.Attributes.Append(stroke_widthAttr);
                        rectElem.Attributes.Append(fillAttr);
                        // и элемент к корневому
                        svgElem.AppendChild(rectElem);
                    }
                    else if (objects is RegularPolygon)
                    {
                        string points = "";         // переменная  для точек многоугольника
                                                    // создаем елемент и атрибуты
                        XmlElement polyElem = xDoc.CreateElement("polygon");
                        XmlAttribute pointsAttr = xDoc.CreateAttribute("points");
                        XmlAttribute strokeAttr = xDoc.CreateAttribute("stroke");
                        XmlAttribute stroke_widthAttr = xDoc.CreateAttribute("stroke-width");
                        XmlAttribute fillAttr = xDoc.CreateAttribute("fill");

                        // запоняем текст для атрибута точек
                        foreach (PointF p in ((RegularPolygon)objects).p)
                        {
                            points += p.X;
                            points += ",";
                            points += p.Y;
                            points += " ";
                        }

                        // создаем текст для атрибутов
                        XmlText pointsText = xDoc.CreateTextNode(points);
                        XmlText strokeText = xDoc.CreateTextNode(objects.pen.Color.Name);
                        XmlText stroke_widthText = xDoc.CreateTextNode(objects.pen.Width.ToString());
                        XmlText fillText = xDoc.CreateTextNode(((SolidBrush)objects.brush).Color.Name);
                        // добаввляем текст в атрибуты в элемент
                        pointsAttr.AppendChild(pointsText);
                        strokeAttr.AppendChild(strokeText);
                        stroke_widthAttr.AppendChild(stroke_widthText);
                        fillAttr.AppendChild(fillText);
                        // атриббуты в элемнет
                        polyElem.Attributes.Append(pointsAttr);
                        polyElem.Attributes.Append(strokeAttr);
                        polyElem.Attributes.Append(stroke_widthAttr);
                        polyElem.Attributes.Append(fillAttr);
                        // и элемент к корневому
                        svgElem.AppendChild(polyElem);
                    }
                    else if (objects is RegularTriangleMy)
                    {
                        string points = "";         // переменная  для точек многоугольника
                                                    // создаем елемент и атрибуты
                        XmlElement polyElem = xDoc.CreateElement("polygon");
                        XmlAttribute pointsAttr = xDoc.CreateAttribute("points");
                        XmlAttribute strokeAttr = xDoc.CreateAttribute("stroke");
                        XmlAttribute stroke_widthAttr = xDoc.CreateAttribute("stroke-width");
                        XmlAttribute fillAttr = xDoc.CreateAttribute("fill");

                        // запоняем текст для атрибута точек
                        foreach (PointF p in ((RegularTriangleMy)objects).p)
                        {
                            points += p.X;
                            points += ",";
                            points += p.Y;
                            points += " ";
                        }

                        // создаем текст для атрибутов
                        XmlText pointsText = xDoc.CreateTextNode(points);
                        XmlText strokeText = xDoc.CreateTextNode(objects.pen.Color.Name);
                        XmlText stroke_widthText = xDoc.CreateTextNode(objects.pen.Width.ToString());
                        XmlText fillText = xDoc.CreateTextNode(((SolidBrush)objects.brush).Color.Name);
                        // добаввляем текст в атрибуты в элемент
                        pointsAttr.AppendChild(pointsText);
                        strokeAttr.AppendChild(strokeText);
                        stroke_widthAttr.AppendChild(stroke_widthText);
                        fillAttr.AppendChild(fillText);
                        // атриббуты в элемнет
                        polyElem.Attributes.Append(pointsAttr);
                        polyElem.Attributes.Append(strokeAttr);
                        polyElem.Attributes.Append(stroke_widthAttr);
                        polyElem.Attributes.Append(fillAttr);
                        // и элемент к корневому
                        svgElem.AppendChild(polyElem);
                    }
                    else
                    {
                        // создаем елемент и атрибуты
                        XmlElement lineElem = xDoc.CreateElement("line");
                        XmlAttribute x1Attr = xDoc.CreateAttribute("x1");
                        XmlAttribute y1Attr = xDoc.CreateAttribute("y1");
                        XmlAttribute x2Attr = xDoc.CreateAttribute("x2");
                        XmlAttribute y2Attr = xDoc.CreateAttribute("y2");
                        XmlAttribute strokeAttr = xDoc.CreateAttribute("stroke");
                        XmlAttribute stroke_widthAttr = xDoc.CreateAttribute("stroke-width");
                        // создаем текст для атрибутов
                        XmlText x1Text = xDoc.CreateTextNode(objects.point.X.ToString());
                        XmlText y1Text = xDoc.CreateTextNode(objects.point.Y.ToString());
                        XmlText x2Text = xDoc.CreateTextNode(((LineMy)objects).PointSecond.X.ToString());
                        XmlText y2Text = xDoc.CreateTextNode(((LineMy)objects).PointSecond.Y.ToString());
                        XmlText strokeText = xDoc.CreateTextNode(objects.pen.Color.Name);
                        XmlText stroke_widthText = xDoc.CreateTextNode(objects.pen.Width.ToString());
                        // добаввляем текст в атрибуты в элемент
                        x1Attr.AppendChild(x1Text);
                        x2Attr.AppendChild(x2Text);
                        y1Attr.AppendChild(y1Text);
                        y2Attr.AppendChild(y2Text);
                        strokeAttr.AppendChild(strokeText);
                        stroke_widthAttr.AppendChild(stroke_widthText);
                        // атриббуты в элемнет
                        lineElem.Attributes.Append(x1Attr);
                        lineElem.Attributes.Append(y1Attr);
                        lineElem.Attributes.Append(x2Attr);
                        lineElem.Attributes.Append(y2Attr);
                        lineElem.Attributes.Append(strokeAttr);
                        lineElem.Attributes.Append(stroke_widthAttr);
                        // и элемент к корневому
                        svgElem.AppendChild(lineElem);
                    }
                }
                xDoc.Save(path);
            }
        }



        ///////////////////////////////////////////////////////////////////////////
        // событие для загзки svg файла
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // сначала откроем и обработаем выбор классе...
            OpenFileDialog opening = new OpenFileDialog();
            opening.Filter = "XML File|*.xml";
            if (opening.ShowDialog() == DialogResult.OK)
            {
                string path = opening.FileName;
                // загузили нужный файл
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);
                // получим корневой елемент
                XmlElement xRoot = xDoc.DocumentElement;
                // пройдемся по документу
                foreach(XmlNode svg in xRoot){
                    // получаем атрибуты размеров
                    if(svg.Attributes.Count > 0 && svg.Attributes.Count > 0)
                    {
                        // получили размер панели
                        panel1.Width = Convert.ToInt32(svg.Attributes.GetNamedItem("width").Value);
                        panel1.Height = Convert.ToInt32(svg.Attributes.GetNamedItem("height").Value);
                        // обходим дочерние узлы svg
                        // очистим нашу коллекцию
                        shapes.Clear();
                        
                        foreach(XmlNode fugures in svg.ChildNodes)
                        {
                            if (fugures.Name == "circle")
                            {
                                float x = Convert.ToInt32(fugures.Attributes.GetNamedItem("cx").Value);
                                float y = Convert.ToInt32(fugures.Attributes.GetNamedItem("cy").Value);
                                float r = Convert.ToInt32(fugures.Attributes.GetNamedItem("r").Value);
                                
                                Circle c = new Circle(x,y,r);   // создали обьект
                                
                                c.pen.Color = Color.FromName(fugures.Attributes.GetNamedItem("stroke").Value);
                                
                                c.pen.Width = Convert.ToInt32(fugures.Attributes.GetNamedItem("stroke-width").Value);
                                c.brush = new SolidBrush(Color.FromName(fugures.Attributes.GetNamedItem("fill").Value));
                                shapes.Add(c);
                                
                            }
                            
                            else if (fugures.Name == "rect")
                            {
                                float x = Convert.ToInt32(fugures.Attributes.GetNamedItem("x").Value);
                                float y = Convert.ToInt32(fugures.Attributes.GetNamedItem("y").Value);
                                float width = Convert.ToInt32(fugures.Attributes.GetNamedItem("width").Value);
                                float height = Convert.ToInt32(fugures.Attributes.GetNamedItem("height").Value);
                                RectangleMy R = new RectangleMy(x,y,width,height);
                                R.pen.Color = Color.FromName(fugures.Attributes.GetNamedItem("stroke").Value);
                                R.pen.Width = Convert.ToInt32(fugures.Attributes.GetNamedItem("stroke-width").Value);
                                R.brush = new SolidBrush(Color.FromName(fugures.Attributes.GetNamedItem("fill").Value));
                                shapes.Add(R);
                            }
                            else if (fugures.Name == "polygon")
                            {
                                ////////// этот скоуп не рабочий, нужно учить геометрию, фигуры треугольник и многоугольник открываться не будут//////////////////
                                /*
                                // в этом месте будут проблемы, так класс Polyon заполниться не полностью
                                PointF[] pBuffer;   // массив точек
                                PointF p = new PointF();           // текщая точка
                                
                                string buffer = ""; // общая строка
                                string s = "";      // точка
                                int check = 0;
                                int side = 0;           // количество точек
                                buffer += fugures.Attributes.GetNamedItem("points").Value;
                                // сначала посчитаем количество точек
                                for (int i = 0; i < buffer.Length; i++)
                                {
                                    
                                    if (buffer[i] == ' ')
                                    {
                                        side++;
                                    }
                                }
                                // достаем точки из svg файла
                                pBuffer = new PointF[side];
                                int j = 0;
                                for(int i = 0; i < buffer.Length; i++)
                                {
                                    
                                    if (check == 2)
                                    {
                                        pBuffer[j] = p;
                                        j++;
                                        check = 0;
                                    }
                                    else if (buffer[i] == ',')
                                    {
                                        p.X = Convert.ToInt32(s);
                                        s = "";
                                        check++;
                                    }
                                    else if (buffer[i] == ' ' && i != buffer.Length)
                                    {
                                        p.Y = Convert.ToInt32(s);
                                        s = "";
                                        check++;
                                    }
                                    else
                                    {
                                        s += buffer[i];
                                    }
                                }
                                // создаем обьект многоугольника
                                RegularPolygon R = new RegularPolygon(pBuffer);
                                R.pen.Color = Color.FromName(fugures.Attributes.GetNamedItem("stroke").Value);
                                R.pen.Width = Convert.ToInt32(fugures.Attributes.GetNamedItem("stroke-width").Value);
                                R.brush = new SolidBrush(Color.FromName(fugures.Attributes.GetNamedItem("fill").Value));
                                shapes.Add(R);
                                */
                            }
                            else
                            {
                                
                                float x1 = Convert.ToInt32(fugures.Attributes.GetNamedItem("x1").Value);
                                float y1 = Convert.ToInt32(fugures.Attributes.GetNamedItem("y1").Value);
                                float x2 = Convert.ToInt32(fugures.Attributes.GetNamedItem("x2").Value);
                                float y2 = Convert.ToInt32(fugures.Attributes.GetNamedItem("y2").Value);
                                 LineMy R = new LineMy(x1, y1, x2, y2);
                                
                                R.pen.Color = Color.FromName(fugures.Attributes.GetNamedItem("stroke").Value);
                                R.pen.Width = Convert.ToInt32(fugures.Attributes.GetNamedItem("stroke-width").Value);
                                shapes.Add(R);
                                
                            }
                            
                        }

                    }

                 }
                // перерисовываем панель(принудитеьно)        
                Graphics g = panel1.CreateGraphics();
                foreach (Shape s in this.shapes)
                {
                    // тут рисуем(дорисовываем к старому)
                    s.Paint(g);
                }
                // а тут обновляем(старое стираем)
                this.panel1_Resize(null, null); /// ???????? что это как это??????
            }
        }


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // очищаем наш лист фигур
            shapes.Clear();
            // перерисовываем панель(принудитеьно)        
            Graphics g = panel1.CreateGraphics();
            foreach (Shape s in this.shapes)
            {
                // тут рисуем(дорисовываем к старому)
                s.Paint(g);
            }
            // а тут обновляем(старое стираем)
            this.panel1_Resize(null, null); /// ???????? что это как это??????
        }


        // ///////////////////////////// Menu Up /////////////////////////////
        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Не доступно");
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegularTriangleMy Trngl = new RegularTriangleMy(panel1.Width/2, panel1.Height/2, 200);
            shapes.Add(Trngl);
            panel1_Paint(sender, null);
        }

        private void polygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegularPolygon plg = new RegularPolygon(5, 100, panel1.Width / 2, panel1.Height / 2);
            shapes.Add(plg);
            panel1_Paint(sender, null);
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float width = 200;
            float height = 100;
            RectangleMy rct = new RectangleMy(panel1.Width / 2 - (width/2), panel1.Height / 2 - (height/2), width, height);
            shapes.Add(rct);
            panel1_Paint(sender, null);
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float r = 100;
            Circle clc = new Circle(panel1.Width / 2 - r, panel1.Height / 2 - r, 100);
            shapes.Add(clc);
            panel1_Paint(sender, null);
        }


        /// menu left/////////////////////  круг ////////////////
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            circleToolStripMenuItem_Click(sender, null);
        }

        // многоугольник
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            polygonToolStripMenuItem_Click(sender, null);
        }

        // прямоугольник
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            rectangleToolStripMenuItem_Click(sender, null);
        }
        // треугольник
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            triangleToolStripMenuItem_Click(sender, null);
        }
        // линия
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            lineToolStripMenuItem_Click(sender, null);
        }
    }
}
