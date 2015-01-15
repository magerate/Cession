namespace Cession.Drawing
{
	using System;
	using System.Drawing;
	using System.Collections.Generic;

	using MonoTouch.CoreGraphics;
	using MonoTouch.Foundation;
	using MonoTouch.UIKit;

	using Cession.Modeling;
	using Cession.Geometries;
	using Cession.Dimensions;

	public static class DrawHelper
	{
		public static Matrix Transform{ get; set; }

		private static Stack<Matrix> matrices = new Stack<Matrix> ();

		public static void PushTransform(Matrix transform){
			matrices.Push (transform);
			Transform = transform;
		}

		public static void PopTransform(){
			Transform = matrices.Pop ();
		}


		static DrawHelper()
		{
			Transform = Matrix.Identity;
		}

		public static void StrokeRect(CGContext context,RectangleDiagram rect)
		{
			context.StrokeRect (rect.ToRect (Transform));
		}

		public static void StrokeLine(this CGContext context,Point2 p1,Point2 p2)
		{
			p1 = Transform.Transform (p1);
			p2 = Transform.Transform (p2);

			context.MoveTo ((float)p1.X, (float)p1.Y);
			context.AddLineToPoint ((float)p2.X, (float)p2.Y);
			context.StrokePath ();
		}

		public static void StrokePolygon(this CGContext context,PathDiagram polygon)
		{
			context.BuildPolygonPath (polygon);
			context.StrokePath ();
		}

		public static void BuildPolygonPath(this CGContext context,PathDiagram polygon)
		{
			var points = polygon.Points;
			var p0 = Transform.Transform (points [0]);
			context.MoveTo ((float) p0.X, (float) p0.Y);

			Point2 pi;
			for (int i = 1; i < points.Count; i++) {
				pi = Transform.Transform (points [i]);
				context.AddLineToPoint((float) pi.X, (float) pi.Y);
			}
			context.ClosePath ();
		}

		public static void BuildPolygonPath(this CGContext context,IList<Point2> polygon)
		{
			var p0 = Transform.Transform (polygon [0]);
			context.MoveTo ((float) p0.X, (float) p0.Y);

			Point2 pi;
			for (int i = 1; i < polygon.Count; i++) {
				pi = Transform.Transform (polygon [i]);
				context.AddLineToPoint((float) pi.X, (float) pi.Y);
			}
			context.ClosePath ();
		}

		public static void BuildFigurePath(this CGContext context,ClosedShapeDiagram figure)
		{
			if (figure is PathDiagram)
				context.BuildPolygonPath (figure as PathDiagram);
			else if (figure is RectangleDiagram) {
				context.AddRect ((figure as RectangleDiagram).ToRect (Transform));
			}
		}

		public static void BuildDoorPath(this CGContext context,Door door){
			var matrix = door.Transform * Transform;
			var p1 = matrix.Transform (door.OriginalBounds.LeftTop);
			var p2 = matrix.Transform (door.OriginalBounds.RightTop);
			var p3 = matrix.Transform (door.OriginalBounds.RightBottom);
			var p4 = matrix.Transform (door.OriginalBounds.LeftBottom);

			context.MoveTo ((float)p1.X, (float)p1.Y);
			context.AddLineToPoint((float) p2.X, (float) p2.Y);
			context.AddLineToPoint((float) p3.X, (float) p3.Y);
			context.AddLineToPoint((float) p4.X, (float) p4.Y);
			context.ClosePath ();
		}

		private static bool NeedReverse(double angle)
		{
			return angle >= Math.PI / 2 && angle <= Math.PI * 3 / 2;
		}

		public static void DrawDimension(this CGContext context,Point2 p1,Point2 p2){
			var logicalLength = p1.DistanceBetween (p2);
			var length = new Length(logicalLength);

			using(var nsStr = new NSString (length.ToString ())){
				var vector = p2 - p1;
				var angle = vector.Angle;

				var stringAttribute = new UIStringAttributes (){ };
				var size = nsStr.GetSizeUsingAttributes (stringAttribute);
				var logicalWidth = size.Width / Transform.M11;
				var logicalHeight = size.Height / Transform.M11;

				double position;
				if (NeedReverse(angle))
					position = (logicalLength + logicalWidth) / 2;
				else
					position = (logicalLength - logicalWidth) / 2;

				var offsetVector = vector * position / vector.Length;
				var rotateVector = vector * logicalHeight /vector.Length;
				rotateVector.Rotate (Math.PI / 2);

				Point2 dimensionPoint;
				if (NeedReverse(angle))
					dimensionPoint = p1 + offsetVector + rotateVector;
				else
					dimensionPoint = p1 + offsetVector;

				var ddPoint = Transform.Transform (dimensionPoint);
				context.SaveState ();
				context.TranslateCTM (ddPoint.X, ddPoint.Y);
				if (NeedReverse(angle))
					context.RotateCTM ((float)(Math.PI+angle));
				else
					context.RotateCTM ((float)angle);
			
				nsStr.DrawString (PointF.Empty, stringAttribute);
				context.RestoreState ();
			}
		}


	}
}

