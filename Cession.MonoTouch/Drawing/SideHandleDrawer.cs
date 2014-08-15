namespace Cession.Drawing
{
	using System;
	using System.Drawing;

	using MonoTouch.CoreGraphics;
	using MonoTouch.UIKit;

	using Cession.Modeling;
	using Cession.Geometries;

	public class SideHandleDrawer:DiagramDrawer
	{
		protected override void DoDraw (CGContext context, Diagram diagram)
		{
			var sideHandle = diagram as SideHandle;
			var rect = GetBound (sideHandle);
			context.SaveState ();
			UIColor.Blue.SetFill ();
			context.FillRect (rect);
			context.RestoreState ();
		}

		private RectangleF GetBound(SideHandle handle){
			return new RectangleF (handle.Location.X - handle.Size / 2,
				handle.Location.Y - handle.Size / 2,
				handle.Size,
				handle.Size);
		}
	}
}

