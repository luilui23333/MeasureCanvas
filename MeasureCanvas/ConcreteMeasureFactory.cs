using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasureCanvas
{

   class RectangleFactory : MeasureFactory
    {
        public  Measure CreateMeasure()
        {
            Console.WriteLine("Create a Rectangle");
            return new Rectangle();
        }
    }

    class EllipseFactory : MeasureFactory
    {
        public Measure CreateMeasure()
        {
            Console.WriteLine("Create a Ellipse");
            return new Ellipse();
        }
    }

    class LineFactory: MeasureFactory
    {
        public Measure CreateMeasure()
        {
            Console.WriteLine("Create a Line");
            return new Line();
        }
    }

    class TrackFactory : MeasureFactory
    {
        public Measure CreateMeasure()
        {
            Console.WriteLine("Create a Track");
            return new Track();
        }
    }
}
