using System;

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

            context.DrawString (label.Text, label.Location);
        }
    }
}

