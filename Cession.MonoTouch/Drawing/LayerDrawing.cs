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
		public static readonly int LogicalUnitPerDp = 25;

		public static void Draw(this Layer layer, CGContext context)
		{
			DrawHelper.Transform = layer.Transform;

			context.SaveState ();
			foreach (var item in layer.Diagrams) {
				item.Draw (context);
			}

			context.RestoreState ();
		}

		public static Matrix GetLayerDefaultTransform(){
			return Layer.GetDefaultTransform(Layer.DefaultSize, 
				(int)UIScreen.MainScreen.Bounds.Width,
				(int)UIScreen.MainScreen.Bounds.Height,
				LogicalUnitPerDp);
		}
	}
}

