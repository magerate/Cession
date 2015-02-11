using System;

using CoreGraphics;
using UIKit;

using Cession.Geometries;
using Cession.Alignments;

namespace Cession.Drawing
{
    public static class AlignmentDrawing
    {
        public static void Draw (this DevicePointToPointRule rule,
                          CGContext context,
                          Matrix transform)
        {
            if (!rule.IsAligned)
                return;

            var point = transform.Transform (rule.ReferencePoint.Value).ToCGPoint ();
            context.SaveState ();
            context.SetAlpha (.4f);
            UIColor.Blue.SetFill ();
            context.FillCircle (point, 16);
            context.RestoreState ();
        }
    }
}

