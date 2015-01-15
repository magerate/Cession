namespace Cession.Drawing
{
	using System;

	using MonoTouch.CoreGraphics;
	using MonoTouch.UIKit;

	using Cession.Modeling;

	public class LabelDrawer:DiagramDrawer
	{
		protected override void DoDraw (CGContext context, Diagram diagram)
		{
			var label = diagram as Label;
			if (null == label)
				throw new ArgumentException ("label");

			var layer = diagram.Owner as Layer;
			var point = layer.ConvertToViewPoint (label.Location).ToPointF ();
			label.Text.DrawString (point, new UIStringAttributes ());
		}
	}
}

