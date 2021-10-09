using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Ink;
using System.Windows.Controls;

namespace MeasureCanvas
{
    interface Measure     //形状（测量）基类
    {
        void Draw();  //画图
        void Measure_MouseDown(object sender, MouseButtonEventArgs e,InkCanvas inkCanvasMeasure, ViewModel viewModel);  //鼠标按下事件
        void Measure_MouseMove(object sender, MouseEventArgs e, InkCanvas inkCanvasMeasure, ViewModel viewModel);  //鼠标移动事件
        void Measure_MouseUp(object sender, MouseButtonEventArgs e, InkCanvas inkCanvasMeasure, ViewModel viewModel);  // 鼠标松开事件

    }
}
