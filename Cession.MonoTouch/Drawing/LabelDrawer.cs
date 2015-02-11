using System;

using CoreGraphics;
using UIKit;

using Cession.Diagrams;

namespace Cession.Drawing
{
    public class LabelDrawer:ShapeDrawer
    {
        protected override void DoDraw (DrawingContext context, Shape shape)
        {
            var label = shape as Label;
            if (null == label)
                throw new ArgumentException ("shape");

            var layer = shape.Owner as Layer;
            var point = layer.ConvertToViewPoint (label.Location).ToCGPoint ();
            label.Text.DrawString (point, new UIStringAttributes ());
        }
    }
}

