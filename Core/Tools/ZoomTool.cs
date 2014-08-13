namespace Cession.Tools
{
	using System;

	using MonoTouch.UIKit;

	using Cession.Geometries;
	using Cession.UIKit;

	public class ZoomTool:Tool
	{
		private Matrix matrix;

		public ZoomTool (ToolManager toolManager):base(toolManager)
		{
		}

		public override void Enter (Tool parentTool, params object[] args)
		{
			base.Enter (parentTool, args);
			matrix = CurrentLayer.Transform;
		}

		public override void Pinch (UIPinchGestureRecognizer gestureRecognizer)
		{
			var transform = matrix;
			transform.ScaleAt (gestureRecognizer.Scale, 
				gestureRecognizer.Scale,
				(double)DiagramView.Center.X,
				(double)DiagramView.Center.Y);
			CurrentLayer.Transform = transform;
			RefreshDiagramView ();
			RefreshToolView ();

			if (gestureRecognizer.IsDone ()) {
				TryRestoreState ();
			}
		}
	}
}

