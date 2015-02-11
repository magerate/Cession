namespace Cession.Tools
{
	using System;
	using CoreGraphics;
	using System.Linq;

	using UIKit;
	using CoreGraphics;
	using Foundation;

	using Cession.UIKit;
	using Cession.Geometries;
	using Cession.Drawing;
	using Cession.Modeling;
	using Cession.Commands;
	using Cession.Utilities;

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
			var room = new Room (rectDiagram,CurrentLayer);
			room.Name = CurrentLayer.CreateRoomName ();

			CommandManager.ExecuteListAdd (CurrentLayer.Diagrams, room);
		}

		private CGRect GetRect(CGPoint p1,CGPoint p2)
		{
			return CGRect.FromLTRB (Math.Min (p1.X, p2.X), 
				Math.Min (p1.Y, p2.Y),
				Math.Max (p1.X, p2.X),
				Math.Max (p1.Y, p2.Y));
		}
	}
}

