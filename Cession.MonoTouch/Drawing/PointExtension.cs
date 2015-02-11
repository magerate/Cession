using CoreGraphics;
using Cession.Geometries;

namespace Cession.Drawing
{
    public static class PointExtension
    {
        public static Point ToPoint (this CGPoint point)
        {
            return new Point ((double)point.X, (double)point.Y);
        }

        public static CGPoint ToCGPoint (this Point point)
        {
            return new CGPoint ((float)point.X, (float)point.Y);
        }
    }
}

