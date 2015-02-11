using System;

using UIKit;

using Cession.Geometries;
using Cession.UIKit;

namespace Cession.Tools
{
    public class ZoomTool:Tool
    {
        private Matrix _matrix;

        public ZoomTool (ToolManager toolManager) : base (toolManager)
        {
        }

        public override void Enter (Tool parentTool, params object[] args)
        {
            base.Enter (parentTool, args);
            _matrix = CurrentLayer.Transform;
        }

        public override void Pinch (UIPinchGestureRecognizer gestureRecognizer)
        {
            var transform = _matrix;
            transform.ScaleAt (gestureRecognizer.Scale, 
                gestureRecognizer.Scale,
                (double)DiagramView.Center.X,
                (double)DiagramView.Center.Y);
            CurrentLayer.Transform = transform;
            RefreshDiagramView ();
            RefreshToolView ();

            if (gestureRecognizer.IsDone ())
            {
                TryRestoreState ();
            }
        }
    }
}

