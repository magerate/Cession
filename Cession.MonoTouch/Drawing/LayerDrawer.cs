namespace Cession.Drawing
{
	using System;
	using System.Drawing;

	using MonoTouch.CoreGraphics;
	using MonoTouch.Foundation;
	using MonoTouch.UIKit;

	using Cession.Modeling;
	using Cession.Geometries;

	public class LayerDrawer:DiagramDrawer
	{
		public static readonly int LogicalUnitPerDp = 25;

		protected override void DoDraw (CGContext context, Diagram diagram)
		{
			var layer = diagram as Layer;
			if (null == layer)
				throw new ArgumentException ("diagram");

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

