using System;

using Cession.Diagrams;
using D = Cession.Diagrams;

namespace Cession.Drawing
{
    public class CircleDrawer:ShapeDrawer
    {
        protected override void DoDraw (DrawingContext drawingContext, Shape shape)
        {
            var circle = shape as D.Circle;
            if (null == circle)
                throw new ArgumentException ("shape");

            drawingContext.StrokeCircle (circle.GetBounds ());
        }
    }
}

