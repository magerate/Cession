using System;

using UIKit;
using CoreGraphics;

using Cession.Geometries;
using Cession.UIKit;

namespace Cession.Tools
{
    public class PanTool:Tool
    {
        private Matrix _matrix;

        public PanTool (ToolManager toolManager) : base (toolManager)
        {
        }

        public override void Enter (Tool parentTool, params object[] args)
        {
            base.Enter (parentTool, args);
            _matrix = CurrentLayer.Transform;
        }

        public override void Pan (UIPanGestureRecognizer gestureRecognizer)
        {
            Matrix transform = _matrix;
            CGPoint offset = gestureRecognizer.TranslationInView (Host.ToolView);
            transform.Translate ((double)offset.X, (double)offset.Y);
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

