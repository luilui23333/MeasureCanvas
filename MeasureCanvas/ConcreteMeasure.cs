using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;



namespace MeasureCanvas
{

    class Rectangle : Measure       //矩形类
    {
        private System.Windows.Point iniP;
        private System.Windows.Point endP;

        public void Draw()
        {
            Console.WriteLine("Draw a Rectangle");
        }

        public void Measure_MouseDown(object sender, MouseButtonEventArgs e, InkCanvas inkCanvasMeasure, ViewModel viewModel) { 
            iniP = e.GetPosition(inkCanvasMeasure);
        }

        public void Measure_MouseMove(object sender, MouseEventArgs e, InkCanvas inkCanvasMeasure, ViewModel viewModel) 
        {
            endP = e.GetPosition(inkCanvasMeasure);
            List<System.Windows.Point> pointList = new List<System.Windows.Point>
                    {
                        new System.Windows.Point(iniP.X, iniP.Y),
                        new System.Windows.Point(iniP.X, endP.Y),
                        new System.Windows.Point(endP.X, endP.Y),
                        new System.Windows.Point(endP.X, iniP.Y),
                        new System.Windows.Point(iniP.X, iniP.Y),
                    };
            StylusPointCollection stylusPoints = new StylusPointCollection(pointList);
            Stroke stroke = new Stroke(stylusPoints)
            {
                DrawingAttributes = inkCanvasMeasure.DefaultDrawingAttributes.Clone()
            };
            viewModel.InkStrokes.Clear();
            viewModel.InkStrokes.Add(stroke);
        }
        public void Measure_MouseUp(object sender, MouseButtonEventArgs e, InkCanvas inkCanvasMeasure, ViewModel viewModel)
        {

        }

}

    class Ellipse : Measure    //椭圆类
    {
        private System.Windows.Point iniP;
        private System.Windows.Point endP;
        public void Draw()
        {
            Console.WriteLine("Draw a Ellipse");
        }

        public void Measure_MouseDown(object sender, MouseButtonEventArgs e, InkCanvas inkCanvasMeasure, ViewModel viewModel) //鼠标点击事件
        {
            iniP = e.GetPosition(inkCanvasMeasure);
        }

        public void Measure_MouseMove(object sender, MouseEventArgs e, InkCanvas inkCanvasMeasure, ViewModel viewModel) //鼠标移动事件
        {
            endP = e.GetPosition(inkCanvasMeasure);
            List<System.Windows.Point> pointList = GenerateEllipseGeometry(iniP, endP);
            StylusPointCollection stylusPoints = new StylusPointCollection(pointList);
            Stroke stroke = new Stroke(stylusPoints)
            {
                DrawingAttributes = inkCanvasMeasure.DefaultDrawingAttributes.Clone()
            };
            viewModel.InkStrokes.Clear();
            viewModel.InkStrokes.Add(stroke);
        }

        private List<System.Windows.Point> GenerateEllipseGeometry(System.Windows.Point st, System.Windows.Point ed)
        {
            double a = 0.5 * (ed.X - st.X);
            double b = 0.5 * (ed.Y - st.Y);
            List<System.Windows.Point> pointList = new List<System.Windows.Point>();
            //绘制椭圆，极坐标坐标系
            for (double r = 0; r <= 2 * Math.PI; r = r + 0.01)
            {
                pointList.Add(new System.Windows.Point(0.5 * (st.X + ed.X) + a * Math.Cos(r), 0.5 * (st.Y + ed.Y) + b * Math.Sin(r)));
            }
            return pointList;
        }
        public void Measure_MouseUp(object sender, MouseButtonEventArgs e, InkCanvas inkCanvasMeasure, ViewModel viewModel)
        {

        }

    }

    class Line : Measure
    {
        private System.Windows.Point iniP;
        private System.Windows.Point endP;
        public void Draw()  //画图
        {
            Console.WriteLine("Draw a Line");
        }
        public void Measure_MouseDown(object sender, MouseButtonEventArgs e, InkCanvas inkCanvasMeasure, ViewModel viewModel)
        {
            iniP = e.GetPosition(inkCanvasMeasure);
        }
        public void Measure_MouseMove(object sender, MouseEventArgs e, InkCanvas inkCanvasMeasure, ViewModel viewModel)
        {
            endP = e.GetPosition(inkCanvasMeasure);
            List<System.Windows.Point> pointList = new List<System.Windows.Point>
            {
                new System.Windows.Point(iniP.X,iniP.Y),
                new System.Windows.Point(endP.X,endP.Y),
                new System.Windows.Point(iniP.X,iniP.Y),
            };
            StylusPointCollection stylusPoints = new StylusPointCollection(pointList);
            Stroke stroke = new Stroke(stylusPoints)
            {
                DrawingAttributes = inkCanvasMeasure.DefaultDrawingAttributes.Clone()
            };
            viewModel.InkStrokes.Clear();
            viewModel.InkStrokes.Add(stroke);
                 
        }
        public void Measure_MouseUp(object sender, MouseButtonEventArgs e, InkCanvas inkCanvasMeasure, ViewModel viewModel)
        {

        }

    }

    class Track:Measure
    {
        private System.Windows.Point iniP;
        private System.Windows.Point endP;
        private Stroke lassoStroke;
        public void Draw() //画图
        {
            Console.WriteLine("Draw a Track");
        }
       public void Measure_MouseDown(object sender, MouseButtonEventArgs e, InkCanvas inkCanvasMeasure, ViewModel viewModel)  
        {
            iniP = e.GetPosition(inkCanvasMeasure);
        }
       public void Measure_MouseMove(object sender, MouseEventArgs e, InkCanvas inkCanvasMeasure, ViewModel viewModel) 
        {
            endP = e.GetPosition(inkCanvasMeasure);
            //List<System.Windows.Point> pointList = GenerateTrackGeometry(iniP, endP);
            if (lassoStroke == null)
            {

                StylusPointCollection stylusPoints = new StylusPointCollection();
                stylusPoints.Add(new StylusPoint(iniP.X, iniP.Y));
                stylusPoints.Add(new StylusPoint(endP.X, endP.Y));
                //Stroke stroke = new Stroke(stylusPoints)
                //{
                // DrawingAttributes = inkCanvasMeasure.DefaultDrawingAttributes.Clone()
                // };
                lassoStroke = new LassoStroke(stylusPoints, inkCanvasMeasure.DefaultDrawingAttributes.Clone());

            }
            else
            {
               this.lassoStroke.StylusPoints.Add(new StylusPoint(endP.X, endP.Y));
               
            }

            viewModel.InkStrokes.Clear();
            viewModel.InkStrokes.Add(lassoStroke);
        }

        public void Measure_MouseUp(object sender, MouseButtonEventArgs e, InkCanvas inkCanvasMeasure, ViewModel viewModel)
        {
            if (lassoStroke == null)
            {
                return;
            }
            //ink.Strokes.Remove(lassoStroke);
            lassoStroke.StylusPoints.Add(lassoStroke.StylusPoints[0]);
            iniP = new System.Windows.Point(0, 0);
            endP = new System.Windows.Point(0, 0);
            lassoStroke = null;

        }

        private List<System.Windows.Point> GenerateTrackGeometry(System.Windows.Point st, System.Windows.Point ed)
        {
            List<System.Windows.Point> pointList = new List<System.Windows.Point>();
            //绘制轨迹
            StylusPoint stylusPoint = new StylusPoint(ed.X, ed.Y);
            pointList.Add(new System.Windows.Point(stylusPoint.X,stylusPoint.Y));
            
            return pointList;
        }

    }


    
}
