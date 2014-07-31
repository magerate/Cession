namespace Cession.Tools
{
	using System;
	using System.Drawing;

	using MonoTouch.UIKit;
	using MonoTouch.CoreGraphics;
	using MonoTouch.Foundation;

	using Cession.UIKit;
	using Cession.Geometries;
	using Cession.Drawing;

	public abstract class DragDropTool:Tool
	{
		protected Point2? startPoint;
		protected Point2? endPoint;

		public DragDropTool (ToolManager toolManager):base(toolManager)
		{
		}

		public override void TouchBegin (PointF point)
		{
			startPoint = ConvertToLogicalPoint (point);
		}

		public override void Pan (UIPanGestureRecognizer gestureRecognizer)
		{
			if (gestureRecognizer.IsDone ()) {
				Commit ();
				Clear ();
				Refresh ();
				TryRestoreState ();
				return;
			}


			endPoint = GetLogicPoint (gestureRecognizer);
			Refresh ();
		}

		protected abstract void Commit ();

		protected virtual void Clear()
		{
			startPoint = null;
			endPoint = null;
		}

		protected override void InternalDraw(CGContext context)
		{
			if (startPoint.HasValue && endPoint.HasValue) {
				DoDraw (context);
			}
		}

		protected abstract void DoDraw (CGContext context);

	}
}

