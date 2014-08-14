namespace Cession.Tools
{
	using System;

	using MonoTouch.UIKit;

	using Cession.Geometries;
	using Cession.UIKit;

	public class PanTool:Tool
	{
		private Matrix matrix;

		public PanTool (ToolManager toolManager):base(toolManager)
		{
		}

		public override void Enter (Tool parentTool, params object[] args)
		{
			base.Enter (parentTool, args);
			matrix = CurrentLayer.Transform;
		}

		public override void Pan (UIPanGestureRecognizer gestureRecognizer)
		{
			var transform = matrix;
			var offset = gestureRecognizer.TranslationInView (this.Host.ToolView);
			transform.Translate ((double)offset.X, (double)offset.Y);
			CurrentLayer.Transform = transform;
			RefreshDiagramView ();
			RefreshToolView ();

			if (gestureRecognizer.IsDone ()) {
				TryRestoreState ();
			}
		}
	}
}

