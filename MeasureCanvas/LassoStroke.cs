using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Ink;


namespace MeasureCanvas
{
    public class LassoStroke : Stroke
    {
        public LassoStroke(StylusPointCollection pts, DrawingAttributes da)
            : base(pts, da)
        {
            this.StylusPoints = pts;
            this.DrawingAttributes = da;
        }

        protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
        {
            Pen pen = new Pen
            {
                StartLineCap = PenLineCap.Round,
                EndLineCap = PenLineCap.Round,
                Brush = new SolidColorBrush(this.DrawingAttributes.Color),
                Thickness = this.DrawingAttributes.Width,
            };

            drawingContext.DrawGeometry(new SolidColorBrush(Colors.Transparent), pen, this.GetGeometry(this.DrawingAttributes));
            //base.DrawCore(drawingContext, drawingAttributes);
        }

    }
}
