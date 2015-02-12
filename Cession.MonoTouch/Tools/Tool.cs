using System;

using CoreGraphics;
using UIKit;
using Foundation;

using Cession.Geometries;
using Cession.Drawing;
using Cession.Diagrams;
using Cession.Commands;

namespace Cession.Tools
{
    public abstract class Tool
    {
        protected ToolManager _toolManager;
        protected Tool _parentTool;

        protected Tool (ToolManager toolManager)
        {
            _toolManager = toolManager;
        }

        public void Draw (DrawingContext drawingContext)
        {
            if (_parentTool != null)
                _parentTool.Draw (drawingContext);

            DoDraw (drawingContext);
        }

        protected virtual void DoDraw (DrawingContext drawingContext)
        {
        }

        public virtual void TouchBegin (CGPoint point)
        {
        }

        public virtual void LongPress (UILongPressGestureRecognizer gestureRecognizer)
        {
            _toolManager.PushTool (typeof(SelectTool));
            _toolManager.CurrentTool.LongPress (gestureRecognizer);
        }

        public virtual void DoubleTap (UITapGestureRecognizer gestureRecognizer)
        {
            _toolManager.PushTool (typeof(SelectTool));
            _toolManager.CurrentTool.DoubleTap (gestureRecognizer);
        }

        public virtual void Tap (UITapGestureRecognizer gestureRecognizer)
        {
        }

        public virtual void Pan (UIPanGestureRecognizer gestureRecognizer)
        {
        }

        public virtual void DoublePan (UIPanGestureRecognizer gestureRecognizer)
        {
//			toolManager.PushTool (ToolType.Pan);
//			toolManager.CurrentTool.TouchBegin(_touchPoint);
//			toolManager.CurrentTool.Pan (gestureRecognizer);
        }

        public virtual void Pinch (UIPinchGestureRecognizer gestureRecognizer)
        {
            _toolManager.PushTool (typeof(ZoomTool));
            _toolManager.CurrentTool.Pinch (gestureRecognizer);
        }

        public virtual void Rotate (UIRotationGestureRecognizer gestureRecognizer)
        {
        }

        public virtual void Leave ()
        {
        }

        public virtual void Enter (Tool parentTool, params object[] args)
        {
            this._parentTool = parentTool;
        }

        public virtual void WillPushTool (Type toolType)
        {
        }

        public virtual void RestoredFrom (Type toolType)
        {
        }

        public bool TryRestoreState ()
        {
            if (_parentTool != null)
            {
                _toolManager.RestoreState ();
                _parentTool = null;
                return true;
            }
            return false;
        }

        public void Complete ()
        {
            if (TryRestoreState ())
                return;
        }

        public virtual string Tips
        {
            get{ return string.Empty; }
        }

        protected void RefreshToolView ()
        {
            Host.ToolView.SetNeedsDisplay ();
        }

        protected void RefreshDiagramView ()
        {
            Host.DiagramView.SetNeedsDisplay ();
        }

        protected IToolHost Host
        {
            get{ return _toolManager.Host; }
        }

        protected UIView DiagramView
        {
            get{ return Host.DiagramView; }
        }

        protected Layer CurrentLayer
        {
            get{ return Host.Layer; }
        }

        protected CommandManager CommandManager
        {
            get{ return Host.CommandManager; }
        }

        protected Point GetLogicPoint (UIGestureRecognizer gestureRecognizer)
        {
            var point = gestureRecognizer.LocationInView (DiagramView);
            return ConvertToLogicalPoint (point);
        }

        protected Point ConvertToLogicalPoint (CGPoint point)
        {
            return CurrentLayer.ConvertToLogicalPoint (point.ToPoint ());
        }

        protected CGPoint ConvertToViewPoint (Point point)
        {
            return CurrentLayer.ConvertToViewPoint (point).ToCGPoint ();
        }

    }
}

