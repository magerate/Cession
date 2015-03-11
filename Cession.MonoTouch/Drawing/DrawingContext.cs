using System;
using System.Collections.Generic;

using CoreGraphics;
using Foundation;
using UIKit;

using Cession.Diagrams;
using Cession.Geometries;
using Cession.Dimensions;
using G = Cession.Geometries;

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

        public void StrokeLine (Point point1, Point point2)
        {
            point1 = Transform.Transform (point1);
            point2 = Transform.Transform (point2);

            _context.MoveTo ((nfloat)point1.X, (nfloat)point1.Y);
            _context.AddLineToPoint ((nfloat)point2.X, (nfloat)point2.Y);
            _context.StrokePath ();
        }

        public void StrokeArc(Point point1,Point point2,Point point3)
        {
            Point center = G.Circle.GetCenter (point1, point2, point3).Value;
            nfloat r = (nfloat)(center.DistanceBetween (point1) * Transform.M11);
            nfloat startAngle = (nfloat)((point1 - center).Angle);
            nfloat endAngle = (nfloat)((point3 - center).Angle);
            bool isClockwise = G.Triangle.IsClockwise (point1, point2, point3);

            CGPoint deviceCenter = Transform.Transform (center).ToCGPoint ();
            _context.AddArc (deviceCenter.X, deviceCenter.Y, r, startAngle, endAngle, isClockwise);
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

//            for (int i = 0; i < polyline.Count - 1; i++)
//            {
//                DrawDimension (polyline [i], polyline [i + 1]);
//            }
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

        public void SaveState()
        {
            _context.SaveState ();
        }

        public void RestoreState()
        {
            _context.RestoreState ();
        }
    }
}

