namespace Cession.Tools
{
	using System;
	using System.Drawing;

	using MonoTouch.UIKit;
	using MonoTouch.CoreGraphics;
	using MonoTouch.Foundation;

	using Cession.UIKit;
	using Cession.Geometries;
	using Cession.Geometries.Shapes;
	using Cession.Drawing;
	using Cession.Modeling;
	using Cession.Commands;

	public class AddRectRoomTool:DragDropTool
	{
		public AddRectRoomTool (ToolManager toolManager):base(toolManager)
		{
		}


		protected override void DoDraw (CGContext context)
		{
			var p1 = ConvertToViewPoint (startPoint.Value);
			var p2 = ConvertToViewPoint (endPoint.Value);
			var rect = GetRect (p1, p2);
			context.StrokeRect (rect);
		}

		protected override void Commit ()
		{
			var rect = Rect.FromPoints (startPoint.Value, endPoint.Value);
			var rectDiagram = new RectangleDiagram (rect);
			var room = new Room (rectDiagram);
			CommandManager.ExecuteListAdd (CurrentLayer.Diagrams, room);
		}

		private RectangleF GetRect(PointF p1,PointF p2)
		{
			return RectangleF.FromLTRB (Math.Min (p1.X, p2.X), 
				Math.Min (p1.Y, p2.Y),
				Math.Max (p1.X, p2.X),
				Math.Max (p1.Y, p2.Y));
		}
	}
}

