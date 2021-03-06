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
        private ToolManager _toolManager;
        private Tool _parentTool;

        public Tool ParentTool
        {
            get{ return _parentTool; }
        }

        protected ToolManager ToolManager
        {
            get{ return _toolManager; }
        }

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
            _toolManager.PushTool(typeof(PanTool));
            _toolManager.CurrentTool.Pan (gestureRecognizer);
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

        public void Finish ()
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
            get{ return Host.Project.SelectedLayer; }
        }

        protected CommandManager CommandManager
        {
            get{ return Host.CommandManager; }
        }

        protected Point GetLogicPoint (UIGestureRecognizer gestureRecognizer)
        {
            var point = gestureRecognizer.LocationInView (Host.ToolView);
            return ConvertToLogicalPoint (point);
        }

        protected Point GetOffsetedPoint(UIGestureRecognizer gestureRecognizer)
        {
            var point = gestureRecognizer.LocationInView (Host.ToolView);
            point.X -= 48;
            point.Y -= 48;
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

        protected void ExecuteAddShape(Shape shape)
        {
            var command = Command.CreateListAdd (CurrentLayer.Shapes, shape);
            CommandManager.Execute (command);
        }

    }
}

