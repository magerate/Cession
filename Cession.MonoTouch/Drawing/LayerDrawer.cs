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

            foreach (var s in layer.Shapes)
            {
                s.Draw (drawingContext);
            }
        }
    }
}

