using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasureCanvas
{
    interface MeasureFactory    //工厂基类
    {
        Measure CreateMeasure(); 
    }
}
