namespace Cession.Tools
{
	using System;
	using System.Drawing;

	using MonoTouch.UIKit;

	using Cession.UIKit;
	using Cession.Geometries;
	using Cession.Drawing;
	using Cession.Modeling;

	public class SelectTool:Tool
	{
		private PointF touchPoint;

		public SelectTool (ToolManager toolManager):base(toolManager)
		{
		}

		public override void Pan (UIPanGestureRecognizer gestureRecognizer)
		{
			if(gestureRecognizer.IsBegan())
			{
				if (!TryOperateDiagram ()) {
					toolManager.PushTool (ToolType.Pan);
				}
				toolManager.CurrentTool.TouchBegin (touchPoint);
				toolManager.CurrentTool.Pan (gestureRecognizer);
			}
		}

		public override void TouchBegin (PointF point)
		{
			touchPoint = point;
		}

		private bool TryOperateDiagram()
		{
			if (TryPanHandle ())
				return true;

			if (TryMove ())
				return true;

			return false;
		}

		private bool TryPanHandle()
		{
			if(CurrentLayer.SelectedDiagrams.Count == 0)
				return false;

			var room = CurrentLayer.SelectedDiagrams [0] as Room;
			if (null == room)
				return false;

			var handles = room.Contour.GetHandles (CurrentLayer.Transform);
			for (int i = 0; i < handles.Length; i++) {
				if (handles[i].Contains (touchPoint.ToPoint2())) {
					toolManager.PushTool (ToolType.MoveSide,handles[i]);
					return true;
				}
			}

			return false;
		}

		private bool TryMove()
		{
			HitTest (ConvertToLogicalPoint(touchPoint));
			if (CurrentLayer.SelectedDiagrams.Count != 0) {
				toolManager.PushTool (ToolType.Move,CurrentLayer.SelectedDiagrams);
				return true;
			}
			return false;
		}

		public override void Tap (UITapGestureRecognizer gestureRecognizer)
		{
			var point = GetLogicPoint (gestureRecognizer);
			HitTest (point);
			RefreshToolView ();
		}

		private void HitTest(Point2 point)
		{
			var shape = CurrentLayer.HitTest (point);

			CurrentLayer.ClearSelection ();
			if (null != shape) {
				if (shape.CanSelect)
					CurrentLayer.Select (shape);
				else
					CurrentLayer.Select (shape.Owner);
			}
		}
	}
}

