using System;
using CoreGraphics;
using Cession.Geometries;

namespace Cession.Drawing
{
    public static class CGTypeExtensions
    {
        public static Rect ToRect (this CGRect rect)
        {
            return new Rect ((double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height);
        }

        public static CGRect ToCGRect (this Rect rect)
        {
            return new CGRect ((nfloat)rect.X, (nfloat)rect.Y, (nfloat)rect.Width, (nfloat)rect.Height);
        }

        public static Size ToGeometrySize(this CGSize size)
        {
            return new Size ((double)size.Width, (double)size.Height);
        }

        public static CGSize ToCGSize(this Size size)
        {
            return new CGSize ((nfloat)size.Width, (nfloat)size.Height);
        }

        public static Point ToPoint (this CGPoint point)
        {
            return new Point ((double)point.X, (double)point.Y);
        }

        public static CGPoint ToCGPoint (this Point point)
        {
            return new CGPoint ((nfloat)point.X, (nfloat)point.Y);
        }
    }
}

