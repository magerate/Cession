namespace Cession.Drawing
{
	using System;
	using System.Drawing;

	using MonoTouch.CoreGraphics;
	using MonoTouch.Foundation;
	using MonoTouch.UIKit;

	using Cession.Modeling;
	using Cession.Geometries;
	using Cession.Geometries.Shapes;

	public static class LayerDrawing
	{
		public static void Draw(this Layer layer, CGContext context)
		{
			var matrix = layer.GetTransform ();
			DrawHelper.Transform = matrix;

			context.SaveState ();
			foreach (var item in layer.Diagrams) {
				item.Draw (context);
			}

			context.RestoreState ();
		}
	}
}

