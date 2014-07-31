namespace Cession.Drawing
{
	using System;

	using MonoTouch.CoreGraphics;
	using MonoTouch.UIKit;

	using Cession.Modeling;
	using Cession.Geometries;
	using Cession.Geometries.Shapes;

	public class RoomDrawer:DiagramDrawer
	{
		public override void Draw (CGContext context, Diagram diagram)
		{
			base.Draw (context,diagram);
			var room = diagram as Room;

			context.SaveState ();
			UIColor.Gray.SetFill ();

			DrawWallSection (context, room);
			context.RestoreState ();
		}

		public override void DrawSelected (CGContext context, Diagram diagram)
		{
			base.DrawSelected (context, diagram);

			var room = diagram as Room;

			context.SaveState ();
			UIColor.Blue.SetFill ();
			context.SetLineWidth (2.0f);

			DrawWallSection (context, room);

			context.RestoreState ();
		}

		private void DrawWallSection(CGContext context,Room room)
		{
			context.BuildFigurePath (room.OuterContour);
			context.BuildFigurePath (room.Contour);
			context.DrawPath (CGPathDrawingMode.EOFillStroke);
		}
	}
}

