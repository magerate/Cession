using System;

using Cession.Diagrams;
using Cession.Geometries;

namespace Cession.Drawing
{
    public class LayerDrawer:ShapeDrawer
    {
        protected override void DoDraw (DrawingContext drawingContext, Shape shape)
        {
            var layer = shape as Layer;
            if (null == layer)
                throw new ArgumentException ("shape");

            var context = drawingContext.CGContext;
            context.SaveState ();
            context.SetLineWidth (10);
            drawingContext.StrokeRect (layer.Bounds);
            context.RestoreState ();

            foreach (var s in layer.Shapes)
            {
                s.Draw (drawingContext);
            }
        }
    }
}

