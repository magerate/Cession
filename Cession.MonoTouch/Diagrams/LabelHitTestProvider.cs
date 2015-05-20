using System;

using UIKit;
using Foundation;

using Cession.Geometries;
using Cession.Drawing;
using Cession.Diagrams;

namespace Cession.Diagrams
{
    public class LabelHitTestProvider:HitTestProvider
    {
        protected override Rect DoGetBounds (Shape shape)
        {
            var label = shape as Label;
            if (null == label)
                throw new ArgumentException ("shape");

            using (var nsStr = new NSString (label.Text))
            {
                var stringAttribute = new UIStringAttributes (){ };
                var size = nsStr.GetSizeUsingAttributes (stringAttribute);

                var logicalSize = new Size (size.Width , size.Height);

                return new Rect (label.Location, logicalSize);
            }
        }
    }
}

