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

            double x = CurrentLayer.Size.Width / 2;
            double y = CurrentLayer.Size.Height / 2;

            Matrix dm = transform;

            double maxTx = x * dm.M11 - dm.OffsetX;
            double minTx = Host.ToolView.Bounds.Right - x * dm.M11 - dm.OffsetX;
            double maxTy = y * dm.M22 - dm.OffsetY;;
            double minTy = Host.ToolView.Bounds.Bottom - y * dm.M22 - dm.OffsetY;;

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

