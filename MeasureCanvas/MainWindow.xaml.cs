using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Ink;

namespace MeasureCanvas
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;
        private MeasureFactory measureFactory;
        private Measure measure;
        //private bool ButtonLock=false;
        public MainWindow()
        {
            InitializeComponent();
            DrawingAttributes drawingAttributes = new DrawingAttributes
            {
                Color = Colors.Green,
                Width = 2,
                Height = 2,
                StylusTip = StylusTip.Rectangle,
                //FitToCurve = true,
                IsHighlighter = false,
                IgnorePressure = true,

            };
            inkCanvasMeasure.DefaultDrawingAttributes = drawingAttributes;

            viewModel = new ViewModel
            {
                MeaInfo = "测试······",
                InkStrokes = new StrokeCollection(),
            };
            DataContext = viewModel;  //Source

        }
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg)|*.jpg|Image Files (*.png)|*.png|Image Files (*.bmp)|*.bmp",
                Title = "Open Image File"
            };
            if (openDialog.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(openDialog.FileName, UriKind.RelativeOrAbsolute);
                image.EndInit();
                imgMeasure.Source = image;
            }
            btnOpen.IsChecked=false;
            
        }
        private void DrawRectangle_Click(object sender, RoutedEventArgs e)
        {
            if (btnRectangle.IsChecked == true)
            {
                btnEllipse.IsChecked = false;
                btnLine.IsChecked = false;
                btnTrack.IsChecked = false;
            }
            measureFactory = new RectangleFactory(); //模拟数据库传递工厂参数：矩形工厂
                measure = measureFactory.CreateMeasure();
                measure.Draw();
         
        }

        private void DrawEllipse_Click(object sender, RoutedEventArgs e)
        {
            if (btnEllipse.IsChecked == true)
            {
                btnRectangle.IsChecked = false;
                btnLine.IsChecked = false;
                btnTrack.IsChecked = false;
            }
            measureFactory = new EllipseFactory();  //模拟数据库传递工厂参数：椭圆工厂
                measure = measureFactory.CreateMeasure();
                measure.Draw();
       
        }


        private void DrawLine_Click(object sender, RoutedEventArgs e)
        {
            if (btnLine.IsChecked == true)
            {
                btnRectangle.IsChecked = false;
                btnEllipse.IsChecked = false;
                btnTrack.IsChecked = false;
            }
            measureFactory = new LineFactory();  //模拟数据库传递工厂参数：直线工厂
            measure = measureFactory.CreateMeasure();
            measure.Draw();
        }


        private void DrawTrack_Click(object sender, RoutedEventArgs e)
        {
            if (btnTrack.IsChecked == true)
            {
                btnRectangle.IsChecked = false;
                btnEllipse.IsChecked = false;
                btnLine.IsChecked = false;
            }
            measureFactory = new TrackFactory();  //模拟数据库传递工厂参数：轨迹工厂
            measure = measureFactory.CreateMeasure();
            measure.Draw();
        }



        private void InkCanvasMeasure_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (measureFactory != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    measure.Measure_MouseDown(sender, e, inkCanvasMeasure, viewModel);
                }
            }
        }

        private void InkCanvasMeasure_MouseMove(object sender, MouseEventArgs e)
        {
            if (measureFactory != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    measure.Measure_MouseMove(sender, e, inkCanvasMeasure, viewModel);
                }
            }
        }

        private void InkCanvasMeasure_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (measureFactory != null)
            {
                if (e.LeftButton == MouseButtonState.Released)
                {
                    measure.Measure_MouseUp(sender, e, inkCanvasMeasure, viewModel);
                }
            }
        }

    }
}
