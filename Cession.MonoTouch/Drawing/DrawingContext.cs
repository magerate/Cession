using System;
using System.Collections.Generic;
using System.Linq;

using CoreGraphics;
using Foundation;
using UIKit;

using Cession.Diagrams;
using Cession.Geometries;
using Cession.Dimensions;
using G = Cession.Geometries;
using D = Cession.Diagrams;

namespace Cession.Drawing
{
    public class DrawingContext
    {
        private CGContext _context;

        public CGContext CGContext
        {
            get{ return _context; }
        }

        public void PushTransform (Matrix transform)
        {
            _context.SaveState ();
            _context.ConcatCTM (transform.ToCGAffineTransform ());
        }

        public void PopTransform ()
        {
            _context.RestoreState ();
        }

        public DrawingContext(CGContext context)
        {
            if (null == context)
                throw new ArgumentException ();

            _context = context;
        }

        public double ConvertToDeviceSize(double size)
        {
            var t = CGContext.GetCTM ();
            return size * t.xx;
        }

        public double ConvertToLogicalSize(double size)
        {
            var t = CGContext.GetCTM ();
            return size / t.xx;
        }

        public void AddLine(Point point1, Point point2)
        {
            _context.MoveTo ((nfloat)point1.X, (nfloat)point1.Y);
            _context.AddLineToPoint ((nfloat)point2.X, (nfloat)point2.Y);
        }

        public void AddLineToPoint(Point point)
        {
            _context.AddLineToPoint ((nfloat)point.X, (nfloat)point.Y);
        }

        public void MoveToPoint(Point point)
        {
            _context.MoveTo ((nfloat)point.X, (nfloat)point.Y);
        }

        public void StrokeLine (Point point1, Point point2)
        {
            AddLine (point1, point2);
            _context.StrokePath ();
        }

        public void StrokeArc(Point point1,Point point2,Point point3)
        {
            AddArc (point1, point2, point3);
            _context.StrokePath ();
        }

        public void AddArc(Point point1,Point point2,Point point3)
        {
            Point center = G.Circle.GetCenter (point1, point2, point3).Value;
            nfloat r = (nfloat)(center.DistanceBetween (point1) );
            nfloat startAngle = (nfloat)((point1 - center).Angle);
            nfloat endAngle = (nfloat)((point3 - center).Angle);
            bool isClockwise = G.Triangle.IsClockwise (point1, point2, point3);
            _context.AddArc ((nfloat)center.X, (nfloat)center.Y, r, startAngle, endAngle, !isClockwise);
        }

        public void StrokePath(Path path)
        {
            BuildPath (path);
            _context.StrokePath ();
        }

        public void BuildPath(Path path)
        {
            var startPoint = path.Segments.First ().Point1;
            MoveToPoint (startPoint);
            foreach (var segment in path.Segments)
            {
                if (segment is LineSegment)
                    AddLineToPoint (segment.Point2);
                else
                {
                    var arcSegment = segment as ArcSegment;
                    AddArc (arcSegment.Point1, arcSegment.PointOnArc,arcSegment.Point2);
                }
            }
        }

        public void BuildCirclePath(D.Circle circle)
        {
            CGRect rect = circle.Bounds.ToCGRect();
            _context.AddEllipseInRect (rect);
        }

        public void BuildRectPath(Rectangle rectangle)
        {
            CGRect rect = rectangle.Rect.ToCGRect();
            _context.SaveState ();
            _context.ConcatCTM (rectangle.Transform.ToCGAffineTransform ());
            _context.AddRect (rect);
            _context.RestoreState ();
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

        public void DrawString(string str,Point point)
        {
            str.DrawString (point.ToCGPoint());
        }

        public void DrawDimension(ClosedShape closedShape)
        {
            if (closedShape is Path)
                DrawPathDimension (closedShape as Path);
                
        }

        public void DrawPathDimension(Path path)
        {
            foreach (var segment in path.Segments)
            {
                DrawDimension (segment.Point1, segment.Point2);
            }
        }

        public void DrawDimension (Point p1, Point p2)
        {
            double logicalLength = p1.DistanceBetween (p2);
            string strLength = Length.ConvertToString (logicalLength);

            using (var nsStr = new NSString (strLength.ToString ()))
            {
                Vector vector = p2 - p1;
                double angle = vector.Angle;

                var stringAttribute = new UIStringAttributes (){ };
                CGSize size = nsStr.GetSizeUsingAttributes (stringAttribute);

                double position;
                if (NeedReverse (angle))
                    position = (logicalLength + size.Width) / 2;
                else
                    position = (logicalLength - size.Width) / 2;

                Vector offsetVector = vector * position / vector.Length;
                Vector rotateVector = vector * size.Height / vector.Length;
                rotateVector.Rotate (Math.PI / 2);

                Point dimensionPoint;
                if (NeedReverse (angle))
                    dimensionPoint = p1 + offsetVector + rotateVector;
                else
                    dimensionPoint = p1 + offsetVector;

                _context.SaveState ();
                _context.TranslateCTM ((nfloat)dimensionPoint.X, (nfloat)dimensionPoint.Y);
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
        }

        public void BuildPolyLinePath(IReadOnlyList<Point> polyline)
        {
            Point p0 = polyline [0];
            CGContext context = CGContext;
            context.MoveTo ((nfloat)p0.X, (nfloat)p0.Y);
            for (int i = 1; i < polyline.Count; i++)
            {
                Point pi = polyline [i];
                context.AddLineToPoint ((nfloat)pi.X, (nfloat)pi.Y);
            }
        }

        public void StrokeCircle(Rect rect)
        {
            CGContext.StrokeEllipseInRect (rect.ToCGRect());
        }

        public void FillCircle(Rect rect)
        {
            CGContext.FillEllipseInRect (rect.ToCGRect());
        }

        public void AddCircle(Rect rect)
        {
            CGContext.AddEllipseInRect(rect.ToCGRect());
        }

        public void AddCircle(Point point,double radius)
        {
            var rect = new Rect (point.X - radius, point.Y - radius, 2 * radius, 2 * radius);
            AddCircle (rect);
        }

        public void StrokeRect(Rect rect)
        {
            CGContext.StrokeRect (rect.ToCGRect());
        }

        public void BuildClosedShapePath(ClosedShape closedShape)
        {
            if (closedShape is Path)
                BuildPath (closedShape as Path);
            else if (closedShape is D.Circle)
                BuildCirclePath (closedShape as D.Circle);
            else if (closedShape is Rectangle)
                BuildRectPath (closedShape as Rectangle);
        }

        public void StrokeCloseShape(ClosedShape closedShape)
        {
            BuildClosedShapePath (closedShape);
            _context.DrawPath (CGPathDrawingMode.Stroke);
        }

        public void FillCloseShape(ClosedShape closedShape)
        {
            BuildClosedShapePath (closedShape);
            _context.DrawPath (CGPathDrawingMode.Fill);
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

