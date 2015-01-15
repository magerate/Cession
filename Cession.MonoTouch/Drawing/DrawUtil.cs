namespace Cession.Drawing
{
	using System;
	using System.Drawing;
	using System.Collections.Generic;

	using MonoTouch.CoreGraphics;
	using MonoTouch.Foundation;
	using MonoTouch.UIKit;

	public static class DrawUtil
	{
		public static void StrokeLine(this CGContext context,PointF p1,PointF p2)
		{
			context.MoveTo (p1.X, p1.Y);
			context.AddLineToPoint (p2.X, p2.Y);
			context.StrokePath ();
		}

		public static void FillCircle(this CGContext context,PointF point,float radius)
		{
			var rect = new RectangleF (point.X - radius, point.Y - radius, 2 * radius, 2 * radius);
			context.FillEllipseInRect (rect);
		}

		public static void DrawString(this string str,PointF point,UIStringAttributes stringAttribute)
		{
			if(null == str)
				throw new ArgumentNullException();

			if(null == stringAttribute)
				throw new ArgumentNullException();

			using (var nsString = new NSString (str)) {
				nsString.DrawString (point, stringAttribute);
			}
		}

		public static SizeF MeasureString(this string str, UIStringAttributes stringAttribute)
		{			
			if(null == str)
				throw new ArgumentNullException();

			if(null == stringAttribute)
				throw new ArgumentNullException();

			using(NSString nsStr = new NSString(str))
			{
				return nsStr.GetSizeUsingAttributes (stringAttribute);
			}
		}
	}
}

