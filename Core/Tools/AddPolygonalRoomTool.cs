namespace Cession.Tools
{
	using System;
	using System.Drawing;
	using System.Linq;
	using System.Collections.Generic;


	using MonoTouch.UIKit;
	using MonoTouch.CoreGraphics;
	using MonoTouch.Foundation;

	using Cession.UIKit;
	using Cession.Geometries;
	using Cession.Geometries.Shapes;
	using Cession.Modeling;
	using Cession.Drawing;
	using Cession.Alignments;
	using Cession.Commands;

	public class AddPolygonalRoomTool:Tool
	{
		private List<Point2> points = new List<Point2>();
		private Point2? currentPoint;

		private DevicePointToPointRule p2pRule = new DevicePointToPointRule();

		public AddPolygonalRoomTool (ToolManager toolManager):base(toolManager)
		{
			p2pRule.Scale = (float)(1 / CurrentLayer.Scale);
		}

		public override void TouchBegin (PointF point)
		{
			if (points.Count == 0) {
				var lp = ConvertToLogicalPoint (point);
				points.Add (lp);
				p2pRule.ReferencePoint = lp;
			}
		}

		public override void Pan (UIPanGestureRecognizer gestureRecognizer)
		{
			if (gestureRecognizer.IsDone ()) {
				if (p2pRule.IsAligned) {
					AddRoom ();
					Clear ();
					RefreshToolView ();
				} else {
					points.Add (currentPoint.Value);
					currentPoint = null;
				}
				return;
			}

			currentPoint = GetLogicPoint (gestureRecognizer);
			if (points.Count > 2)
				currentPoint = p2pRule.Align (currentPoint.Value);

			RefreshToolView ();
		}

		private void Clear()
		{
			points.Clear ();
			currentPoint = null;
			p2pRule.Reset ();
		}

		private void AddRoom()
		{
			var polygon = new PathDiagram (points);
			var room = new Room (polygon);
			CommandManager.ExecuteListAdd (CurrentLayer.Diagrams, room);
		}

		protected override void InternalDraw (CGContext context)
		{
			if (points.Count > 1) {
				for (int i = 0; i < points.Count - 1; i++) {
					context.StrokeLine (points [i], points [i + 1]);
				}
			}

			if (currentPoint.HasValue) {
				context.StrokeLine (points.Last (), currentPoint.Value);
			}

			p2pRule.Draw (context,CurrentLayer.Transform);
		}
	}
}

