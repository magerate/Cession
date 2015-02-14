using System;

using UIKit;
using CoreGraphics;

using Cession.UIKit;
using Cession.Geometries;
using Cession.Drawing;

namespace Cession.Tools
{
    public abstract class DragDropTool:Tool
    {
        protected Point? StartPoint{ get; set; }
        protected Point? EndPoint{ get; set; }

        protected DragDropTool (ToolManager toolManager) : base (toolManager)
        {
        }

        public override void TouchBegin (CGPoint point)
        {
            StartPoint = ConvertToLogicalPoint (point);
        }

        public override void Pan (UIPanGestureRecognizer gestureRecognizer)
        {
            if (gestureRecognizer.IsDone ())
            {
                Commit ();
                Clear ();
                RefreshDiagramView ();
                RefreshToolView ();
                TryRestoreState ();
            }
            else
            {
                EndPoint = GetLogicPoint (gestureRecognizer);
                RefreshToolView ();
            }
        }

        protected abstract void Commit ();

        protected virtual void Clear ()
        {
            StartPoint = null;
            EndPoint = null;
        }

        protected override void DoDraw (DrawingContext drawingContext)
        {
            if (StartPoint.HasValue && EndPoint.HasValue)
            {
                DoDrawDragDrop (drawingContext);
            }
        }

        protected abstract void DoDrawDragDrop (DrawingContext drawingContext);

    }
}

