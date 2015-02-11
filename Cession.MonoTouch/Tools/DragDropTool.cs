using System;

using UIKit;
using CoreGraphics;
using Foundation;

using Cession.UIKit;
using Cession.Geometries;
using Cession.Drawing;

namespace Cession.Tools
{
    public abstract class DragDropTool:Tool
    {
        protected Point? startPoint;
        protected Point? endPoint;

        public DragDropTool (ToolManager toolManager) : base (toolManager)
        {
        }

        public override void TouchBegin (CGPoint point)
        {
            startPoint = ConvertToLogicalPoint (point);
        }

        public override void Pan (UIPanGestureRecognizer gestureRecognizer)
        {
            if (gestureRecognizer.IsDone ())
            {
                Commit ();
                Clear ();
                RefreshToolView ();
                TryRestoreState ();
                return;
            }


            endPoint = GetLogicPoint (gestureRecognizer);
            RefreshToolView ();
        }

        protected abstract void Commit ();

        protected virtual void Clear ()
        {
            startPoint = null;
            endPoint = null;
        }

        protected override void DoDraw (DrawingContext context)
        {
            if (startPoint.HasValue && endPoint.HasValue)
            {
                DoDrawDragDrop (context);
            }
        }

        protected abstract void DoDrawDragDrop (DrawingContext context);

    }
}

