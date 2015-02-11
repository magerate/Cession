namespace Cession.Tools
{
	using System;
	using CoreGraphics;

	using UIKit;
	using CoreGraphics;
	using Foundation;

	using Cession.Geometries;
	using Cession.Drawing;
	using Cession.Modeling;
	using Cession.Commands;

	public abstract class Tool
	{
		protected ToolManager toolManager;
		protected Tool parentTool;

		protected Tool (ToolManager toolManager)
		{
			this.toolManager = toolManager;
		}
		
		public void Draw (CGContext context)
		{
			if(parentTool != null)
				parentTool.Draw(context);

			InternalDraw (context);
		}

		protected virtual void InternalDraw(CGContext context)
		{
		}

		public virtual void TouchBegin(CGPoint point)
		{
		}

		public virtual void LongPress(UILongPressGestureRecognizer gestureRecognizer)
		{
			toolManager.PushTool (ToolType.Select);
			toolManager.CurrentTool.LongPress (gestureRecognizer);
		}

		public virtual void DoubleTap (UITapGestureRecognizer gestureRecognizer)
		{
			toolManager.PushTool (ToolType.Select);
			toolManager.CurrentTool.DoubleTap (gestureRecognizer);
		}

		public virtual void Tap (UITapGestureRecognizer gestureRecognizer)
		{
		}
		
		public virtual void Pan (UIPanGestureRecognizer gestureRecognizer)
		{
		}

		public virtual void DoublePan(UIPanGestureRecognizer gestureRecognizer)
		{
//			toolManager.PushTool (ToolType.Pan);
//			toolManager.CurrentTool.TouchBegin(_touchPoint);
//			toolManager.CurrentTool.Pan (gestureRecognizer);
		}
		
		public virtual void Pinch (UIPinchGestureRecognizer gestureRecognizer)
		{
			toolManager.PushTool (ToolType.Zoom);
			toolManager.CurrentTool.Pinch (gestureRecognizer);
		}
		
		public virtual void Rotate (UIRotationGestureRecognizer gestureRecognizer)
		{
		}

		public virtual void Leave ()
		{
		}

		public virtual void Enter (Tool parentTool,params object[] args)
		{
			this.parentTool = parentTool;
		}

		public virtual void WillPushTool(ToolType toolType)
		{
		}

		public virtual void RestoredFrom(ToolType toolType)
		{
		}

		public bool TryRestoreState ()
		{
			if (parentTool != null) 
			{
				toolManager.RestoreState ();
				parentTool = null;
				return true;
			}
			return false;
		}

		public void Complete()
		{
			if (TryRestoreState ())
				return;
		}

		public virtual string Tips
		{
			get{return string.Empty;}
		}

		protected void RefreshToolView()
		{
			Host.ToolView.SetNeedsDisplay ();
		}

		protected void RefreshDiagramView()
		{
			Host.DiagramView.SetNeedsDisplay ();
		}

		protected IToolHost Host
		{
			get{return toolManager.Host;}
		}

		protected UIView DiagramView
		{
			get{ return Host.DiagramView; }
		}

		protected Project Project
		{
			get{ return Host.Project; }
		}

		protected Layer CurrentLayer
		{
			get{ return Project.Layers.SelectedLayer; }
		}

		protected CommandManager CommandManager
		{
			get{ return Host.CommandManager; }
		}

		protected Point2 GetLogicPoint(UIGestureRecognizer gestureRecognizer)
		{
			var point = gestureRecognizer.LocationInView (DiagramView);
			return ConvertToLogicalPoint (point);
		}

		protected Point2 ConvertToLogicalPoint(CGPoint point)
		{
			return CurrentLayer.ConvertToLogicalPoint (point.ToPoint2());
		}

		protected CGPoint ConvertToViewPoint(Point2 point)
		{
			return CurrentLayer.ConvertToViewPoint (point).ToCGPoint();
		}

	}
}

