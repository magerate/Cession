using System;
using System.Collections.Generic;

using CoreGraphics;
using Foundation;
using UIKit;

using Cession.Diagrams;
using Cession.Geometries;
using Cession.Dimensions;

namespace Cession.Drawing
{
    public static class DrawHelper
    {
        public static Matrix Transform{ get; set; }

        private static Stack<Matrix> matrices = new Stack<Matrix> ();

        public static void PushTransform (Matrix transform)
        {
            matrices.Push (transform);
            Transform = transform;
        }

        public static void PopTransform ()
        {
            Transform = matrices.Pop ();
        }


        static DrawHelper ()
        {
            Transform = Matrix.Identity;
        }

        public static void StrokeLine (this CGContext context, Point p1, Point p2)
        {
            p1 = Transform.Transform (p1);
            p2 = Transform.Transform (p2);

            context.MoveTo ((nfloat)p1.X, (nfloat)p1.Y);
            context.AddLineToPoint ((nfloat)p2.X, (nfloat)p2.Y);
            context.StrokePath ();
        }

//        public static void StrokePolygon (this CGContext context, PathDiagram polygon)
//        {
//            context.BuildPolygonPath (polygon);
//            context.StrokePath ();
//        }
//
//        public static void BuildPolygonPath (this CGContext context, PathDiagram polygon)
//        {
//            var points = polygon.Points;
//            var p0 = Transform.Transform (points [0]);
//            context.MoveTo ((float)p0.X, (float)p0.Y);
//
//            Point pi;
//            for (int i = 1; i < points.Count; i++)
//            {
//                pi = Transform.Transform (points [i]);
//                context.AddLineToPoint ((float)pi.X, (float)pi.Y);
//            }
//            context.ClosePath ();
//        }

        public static void BuildPolygonPath (this CGContext context, IReadOnlyList<Point> polygon)
        {
            var p0 = Transform.Transform (polygon [0]);
            context.MoveTo ((nfloat)p0.X, (nfloat)p0.Y);

            Point pi;
            for (int i = 1; i < polygon.Count; i++)
            {
                pi = Transform.Transform (polygon [i]);
                context.AddLineToPoint ((nfloat)pi.X, (nfloat)pi.Y);
            }
            context.ClosePath ();
        }

        private static bool NeedReverse (double angle)
        {
            return angle >= Math.PI / 2 && angle <= Math.PI * 3 / 2;
        }

        public static void DrawDimension (this CGContext context, Point p1, Point p2)
        {
            var logicalLength = p1.DistanceBetween (p2);
            var length = new Length (logicalLength);

            using (var nsStr = new NSString (length.ToString ()))
            {
                Vector vector = p2 - p1;
                double angle = vector.Angle;

                var stringAttribute = new UIStringAttributes (){ };
                CGSize size = nsStr.GetSizeUsingAttributes (stringAttribute);
                double logicalWidth = size.Width / Transform.M11;
                double logicalHeight = size.Height / Transform.M11;

                double position;
                if (NeedReverse (angle))
                    position = (logicalLength + logicalWidth) / 2;
                else
                    position = (logicalLength - logicalWidth) / 2;

                Vector offsetVector = vector * position / vector.Length;
                Vector rotateVector = vector * logicalHeight / vector.Length;
                rotateVector.Rotate (Math.PI / 2);

                Point dimensionPoint;
                if (NeedReverse (angle))
                    dimensionPoint = p1 + offsetVector + rotateVector;
                else
                    dimensionPoint = p1 + offsetVector;

                Point ddPoint = Transform.Transform (dimensionPoint);
                context.SaveState ();
                context.TranslateCTM ((nfloat)ddPoint.X, (nfloat)ddPoint.Y);
                if (NeedReverse (angle))
                    context.RotateCTM ((nfloat)(Math.PI + angle));
                else
                    context.RotateCTM ((nfloat)angle);

                nsStr.DrawString (CGPoint.Empty, stringAttribute);
                context.RestoreState ();
            }
        }


    }
}

