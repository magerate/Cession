using CoreGraphics;
using Cession.Geometries;

namespace Cession.Drawing
{
    public static class RectangleExtension
    {
        public static Rect ToRect (this CGRect rect)
        {
            return new Rect ((double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height);
        }

        public static CGRect ToCGRect (this Rect rect)
        {
            return new CGRect ((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }
    }
}

