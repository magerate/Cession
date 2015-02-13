using System;

using UIKit;

using Cession.Geometries;
using Cession.UIKit;

namespace Cession.Tools
{
    public class ZoomTool:Tool
    {
        private readonly double MinScale = 0.2;
        private readonly double MaxScale = 4;

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

            double min = MinScale / _matrix.M11;
            double max = MaxScale / _matrix.M11;

            double sx = MathHelper.Clamp (min, max, gestureRecognizer.Scale);
            double sy = MathHelper.Clamp (min, max, gestureRecognizer.Scale);

            transform.ScaleAt (sx, sy,
                (double)Host.ToolView.Center.X,
                (double)Host.ToolView.Center.Y);

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

