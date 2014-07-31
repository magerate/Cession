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
	using Cession.Geometries.Shapes;

	public static class DrawHelper
	{
		public static Matrix Transform{ get; set; }

		private static Stack<Matrix> matrices = new Stack<Matrix> ();

		public static void PushTransform(Matrix transform){
			matrices.Push (transform);
			Transform = transform;
		}


		static DrawHelper()
		{
			Transform = Matrix.Identity;
		}

		public static void StrokeRect(CGContext context,RectangleDiagram rect)
		{
			context.StrokeRect (rect.ToRect (Transform));
		}

		public static void StrokeLine(this CGContext context,PointF p1,PointF p2)
		{
			context.MoveTo (p1.X, p1.Y);
			context.AddLineToPoint (p2.X, p2.Y);
			context.StrokePath ();
		}

		public static void StrokeLine(this CGContext context,Point2 p1,Point2 p2)
		{
			p1 = Transform.Transform (p1);
			p2 = Transform.Transform (p2);

			context.MoveTo ((float)p1.X, (float)p1.Y);
			context.AddLineToPoint ((float)p2.X, (float)p2.Y);
			context.StrokePath ();
		}

		public static void FillCircle(this CGContext context,PointF point,float radius)
		{
			var rect = new RectangleF (point.X - radius, point.Y - radius, 2 * radius, 2 * radius);
			context.FillEllipseInRect (rect);
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

		public static void BuildFigurePath(this CGContext context,ClosedShapeDiagram figure)
		{
			if (figure is PathDiagram)
				context.BuildPolygonPath (figure as PathDiagram);
			else if (figure is RectangleDiagram) {
				context.AddRect ((figure as RectangleDiagram).ToRect (Transform));
			}
		}

		public static void DrawString(string str,UIFont font,PointF point)
		{
			if(null == str)
				throw new ArgumentNullException();
			if(null == font)
				throw new ArgumentNullException();

			str = str.Replace("\n","~");
			string[] strs = str.Split(new char[] {'~'});			
			int i = 0;
			SizeF size;
			if(strs.Length>0)
			{
				size = MeasureString(strs[0],font);			
				foreach(string s in strs)
				{
					float x = point.X;
					float y = point.Y + i*size.Height;
					using(NSString nsString = new NSString(s))
					{					   
						nsString.DrawString(new PointF(x,y),font);
					}
					i++;
				}	
			}
		}

		public static SizeF MeasureString(this string str, UIFont font)
		{			
			using(NSString nsStr = new NSString(str))
			{
				return nsStr.StringSize(font);				
			}
		}
	}
}

