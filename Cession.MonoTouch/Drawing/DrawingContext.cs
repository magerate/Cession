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
    public class DrawingContext
    {
        private Stack<Matrix> _matrices = new Stack<Matrix> ();
        private CGContext _context;

        public Matrix Transform{ get; set; }
        public CGContext CGContext
        {
            get{ return _context; }
        }

        public void PushTransform (Matrix transform)
        {
            _matrices.Push (Transform);
            Transform = transform;
        }

        public void PopTransform ()
        {
            Transform = _matrices.Pop ();
        }

        public DrawingContext(CGContext context)
        {
            if (null == context)
                throw new ArgumentException ();

            _context = context;
            Transform = Matrix.Identity;
        }

        public void StrokeLine (Point p1, Point p2)
        {
            p1 = Transform.Transform (p1);
            p2 = Transform.Transform (p2);

            _context.MoveTo ((nfloat)p1.X, (nfloat)p1.Y);
            _context.AddLineToPoint ((nfloat)p2.X, (nfloat)p2.Y);
            _context.StrokePath ();
        }

        public void StrokePolygon (IReadOnlyList<Point> polygon)
        {
            BuildPolygonPath (polygon);
            _context.StrokePath ();
        }

        public void BuildPolygonPath (IReadOnlyList<Point> polygon)
        {
            if (null == polygon)
                throw new ArgumentNullException ();

            if (polygon.Count < 3)
                throw new ArgumentException ();

            BuildPolyLinePath (polygon);
            _context.ClosePath ();
        }

        private bool NeedReverse (double angle)
        {
            return angle >= Math.PI / 2 && angle <= Math.PI * 3 / 2;
        }

        public void DrawDimension (Point p1, Point p2)
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
                _context.SaveState ();
                _context.TranslateCTM ((nfloat)ddPoint.X, (nfloat)ddPoint.Y);
                if (NeedReverse (angle))
                    _context.RotateCTM ((nfloat)(Math.PI + angle));
                else
                    _context.RotateCTM ((nfloat)angle);

                nsStr.DrawString (CGPoint.Empty, stringAttribute);
                _context.RestoreState ();
            }
        }

        public void DrawPolyline(IReadOnlyList<Point> polyline)
        {
            if (null == polyline)
                throw new ArgumentNullException ();

            if (polyline.Count <= 1)
                return;

            BuildPolyLinePath (polyline);
            CGContext.StrokePath ();

            for (int i = 0; i < polyline.Count - 1; i++)
            {
                DrawDimension (polyline [i], polyline [i + 1]);
            }
        }

        public void BuildPolyLinePath(IReadOnlyList<Point> polyline)
        {
            Point p1 = Transform.Transform (polyline [0]);
            CGContext context = CGContext;
            context.MoveTo ((nfloat)p1.X, (nfloat)p1.Y);
            for (int i = 1; i < polyline.Count; i++)
            {
                Point pi = Transform.Transform(polyline [i]);
                context.AddLineToPoint ((nfloat)pi.X, (nfloat)pi.Y);
            }
        }

        public void StrokeCircle(Rect rect)
        {
            var cr = GetCGRect (rect);
            CGContext.StrokeEllipseInRect (cr);
        }

        public void StrokeRect(Rect rect)
        {
            var cr = GetCGRect (rect);
            CGContext.StrokeRect (cr);
        }

        private CGRect GetCGRect(Rect rect)
        {
            CGPoint location = Transform.Transform (rect.Location).ToCGPoint ();
            nfloat width = (nfloat)(Transform.M11 * rect.Width);
            nfloat height = (nfloat)(Transform.M11 * rect.Height); 
            return new CGRect (location.X, location.Y, width, height);
        }


    }
}

