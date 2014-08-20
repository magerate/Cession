namespace Cession
{
	using System;
	using System.Drawing;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;
	using MonoTouch.CoreGraphics;

	using Cession.Drawing;
	using Cession.Tools;

	public class ToolView:UIView
	{
		public ToolManager ToolManager{ get; set;}

		private UITapGestureRecognizer tapRecognizer;
		private UITapGestureRecognizer doubleTapRecognizer;
		private UIPanGestureRecognizer panRecognizer;
		private UIPinchGestureRecognizer pinchRecognizer;
		private UIPanGestureRecognizer doublePanRecognizer;
		private UIRotationGestureRecognizer rotateRecognizer;
		private UILongPressGestureRecognizer longPressGestureRecognizer;


		public ToolView (RectangleF frame):base(frame)
		{
			this.BackgroundColor = UIColor.Clear;
			InitializeGestureRecognizer();
		}

		private void InitializeGestureRecognizer ()
		{
			tapRecognizer = new UITapGestureRecognizer (Tap);
			AddGestureRecognizer (tapRecognizer);

			doubleTapRecognizer = new UITapGestureRecognizer (DoubleTap);
			doubleTapRecognizer.NumberOfTouchesRequired = 2;
			AddGestureRecognizer (doubleTapRecognizer);

			longPressGestureRecognizer = new UILongPressGestureRecognizer (LongPress);
			longPressGestureRecognizer.MinimumPressDuration = 1f;
			AddGestureRecognizer (longPressGestureRecognizer);

			panRecognizer = new UIPanGestureRecognizer (Pan);
			panRecognizer.MaximumNumberOfTouches = 1;
			AddGestureRecognizer (panRecognizer);

			pinchRecognizer = new UIPinchGestureRecognizer (Pinch);
			AddGestureRecognizer (pinchRecognizer);

			doublePanRecognizer = new UIPanGestureRecognizer(DoublePan);
			doublePanRecognizer.MinimumNumberOfTouches = 2;
			doublePanRecognizer.MaximumNumberOfTouches = 2;
			AddGestureRecognizer (doublePanRecognizer);

			rotateRecognizer = new UIRotationGestureRecognizer(Rotate);
			AddGestureRecognizer (rotateRecognizer);
		}

		private void Tap (UITapGestureRecognizer gestureRecognizer)
		{
			if (CanPerformToolAction()) 
				ToolManager.CurrentTool.Tap (gestureRecognizer);
		}

		private void LongPress (UILongPressGestureRecognizer gestureRecognizer)
		{
			if (CanPerformToolAction()) 
				ToolManager.CurrentTool.LongPress (gestureRecognizer);
		}

		private void DoubleTap (UITapGestureRecognizer gestureRecognizer)
		{
			if (CanPerformToolAction()) 
				ToolManager.CurrentTool.DoubleTap (gestureRecognizer);
		}

		private void Pan (UIPanGestureRecognizer gestureRecognizer)
		{
			if (CanPerformToolAction()) 
				ToolManager.CurrentTool.Pan (gestureRecognizer);
		}

		private void Pinch (UIPinchGestureRecognizer gestureRecognizer)
		{
			if (CanPerformToolAction()) 
				ToolManager.CurrentTool.Pinch (gestureRecognizer);
		}

		private void DoublePan (UIPanGestureRecognizer gestureRecognizer)
		{
			if (CanPerformToolAction()) 
				ToolManager.CurrentTool.DoublePan (gestureRecognizer);
		}

		private void Rotate (UIRotationGestureRecognizer gestureRecognizer)
		{
			if (CanPerformToolAction()) 
				ToolManager.CurrentTool.Rotate (gestureRecognizer);
		}

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			if(null != ToolManager)
			{
				var touch = (UITouch)touches.AnyObject;
				var point = touch.LocationInView (this);
				ToolManager.CurrentTool.TouchBegin (point);
			}
		}

		private bool CanPerformToolAction()
		{
			if (ToolManager == null)
				return false;

			return true;
		}

		public override void Draw (RectangleF rect)
		{
			if (null == ToolManager)
				return;


			using (var context = UIGraphics.GetCurrentContext ()) {
				DrawHelper.Transform = ToolManager.Host.Project.Layers.SelectedLayer.Transform;
				foreach (var item in ToolManager.Host.Project.Layers.SelectedLayer.SelectedDiagrams) {
					item.DrawSelected (context);
				};

				ToolManager.CurrentTool.Draw (context);
			}
		}
	}
}

