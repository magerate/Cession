namespace Cession.Drawing
{
	using System;

	using MonoTouch.CoreGraphics;
	using MonoTouch.UIKit;

	using Cession.Modeling;
	using Cession.Geometries;

	public class RoomDrawer:DiagramDrawer
	{
		protected override void DoDraw (CGContext context, Diagram diagram)
		{
			var room = diagram as Room;

			context.SaveState ();
			UIColor.Gray.SetFill ();

			DrawWallSection (context, room);
			DrawDimension (context, room);
			context.RestoreState ();
		}

		protected override void DoDrawSelected (CGContext context, Diagram diagram)
		{
			var room = diagram as Room;

			context.SaveState ();
			UIColor.Blue.SetFill ();
			context.SetLineWidth (2.0f);

			DrawWallSection (context, room);

			var handles = room.Contour.GetHandles (DrawHelper.Transform);
			foreach (var handle in handles) {
				handle.Draw (context);
			}

			context.RestoreState ();
		}

		private void DrawWallSection(CGContext context,Room room)
		{
			context.BuildFigurePath (room.OuterContour);
			context.BuildFigurePath (room.Contour);
			foreach (var door in room.Doors) {
				context.BuildDoorPath (door);
			}
			context.DrawPath (CGPathDrawingMode.EOFillStroke);
		}

		private void DrawDimension(CGContext context,Room room){
			var polygon = room.Contour as IPolygonal;
			for (int i = 0; i < polygon.SideCount; i++) {
				var segment = polygon [i];
				context.DrawDimension (segment.P1, segment.P2);
			}
		}
	}
}

