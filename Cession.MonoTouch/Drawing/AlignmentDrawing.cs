namespace Cession.Drawing
{
	using System;
	using System.Drawing;

	using MonoTouch.CoreGraphics;
	using MonoTouch.UIKit;

	using Cession.Geometries;
	using Cession.Alignments;

	public static class AlignmentDrawing
	{
		public static void Draw(this DevicePointToPointRule rule,
			CGContext context,
			Matrix transform)

		{
			if (!rule.IsAligned)
				return;

			var point = transform.Transform(rule.ReferencePoint).ToPointF ();
			context.SaveState ();
			context.SetAlpha (.4f);
			UIColor.Blue.SetFill ();
			context.FillCircle (point, 16);
			context.RestoreState ();
		}
	}
}

