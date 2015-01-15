namespace Cession.Modeling
{
	using System;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	using Cession.Geometries;
	using Cession.Drawing;

	public class LabelHitTestProvider:HitTestProvider
	{
		protected override Rect DoGetBounds (Diagram diagram)
		{
			var label = diagram as Label;
			if (null == label)
				throw new ArgumentException ("diagram");

			using (var nsStr = new NSString (label.Text)) {
				var stringAttribute = new UIStringAttributes (){ };
				var size = nsStr.GetSizeUsingAttributes (stringAttribute);

				var logicalSize = new Size2 ((int)(size.Width * LayerDrawer.LogicalUnitPerDp),
					(int)(size.Height * LayerDrawer.LogicalUnitPerDp));

				return new Rect (label.Location, logicalSize);
			}
		}
	}
}

