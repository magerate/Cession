using System;

using UIKit;
using CoreGraphics;

using Cession.Geometries;

namespace Cession.Tools
{
    public class TriangleTestTool:Tool
    {
        private Point p1;
        private Point p2;
        private Point p3;

        private Point cp;

        public TriangleTestTool (ToolManager toolManager):base(toolManager)
        {
            CGPoint center = Host.ToolView.Center;
            CGPoint dp1 = new CGPoint (center.X + 100, center.Y + 50);
            CGPoint dp2 = new CGPoint (center.X - 100, center.Y + 100);
            p1 = ConvertToLogicalPoint (center);
            p2 = ConvertToLogicalPoint (dp2);
            p3 = ConvertToLogicalPoint (dp1);
        }

        public override void Pan (UIPanGestureRecognizer gestureRecognizer)
        {
            cp = GetLogicPoint(gestureRecognizer);
            RefreshToolView ();
        }

        protected override void DoDraw (Cession.Drawing.DrawingContext drawingContext)
        {
            UIColor fillColor;
            if (Triangle.IsClamp (p1, p2, p3, cp))
                fillColor = UIColor.Blue;
            else
                fillColor = UIColor.Gray;

            drawingContext.MoveToPoint (p1);
            drawingContext.AddLineToPoint (p2);
            drawingContext.AddLineToPoint (p3);

            CGContext cgc = drawingContext.CGContext;
            cgc.ClosePath ();

            cgc.SaveState ();
            fillColor.SetFill ();
            cgc.FillPath ();
            cgc.RestoreState ();
        }


    }
}

