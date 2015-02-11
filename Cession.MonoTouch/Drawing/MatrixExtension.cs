using System;
using Cession.Geometries;
using CoreGraphics;

namespace Cession.Drawing
{
    public static class MatrixExtension
    {
        public static CGAffineTransform ToCGAffineTransform (this Matrix matrix)
        {
            return new CGAffineTransform ((nfloat)matrix.M11,
                (nfloat)matrix.M12,
                (nfloat)matrix.M21,
                (nfloat)matrix.M22,
                (nfloat)matrix.OffsetX,
                (nfloat)matrix.OffsetY);
        }
    }
}

