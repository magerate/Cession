using System;

using UIKit;
using CoreGraphics;

using Cession.Geometries;
using Cession.UIKit;
using Cession.Diagrams;

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

            Rect rect = CurrentLayer.ConvertToViewRect (CurrentLayer.Bounds);
           
            double maxTx = -rect.Left;
            double minTx = Host.ToolView.Bounds.Right - rect.Right;
            double maxTy = -rect.Top;
            double minTy = Host.ToolView.Bounds.Bottom - rect.Bottom;

            double ox;
            if (minTx >= maxTx)
                ox = 0;
            else
                ox = MathHelper.Clamp (minTx, maxTx, (double)offset.X);

            double oy;
            if (minTy >= maxTy)
                oy = 0;
            else
                oy = MathHelper.Clamp (minTy, maxTy, (double)offset.Y);

            transform.Translate (ox, oy);
                
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

