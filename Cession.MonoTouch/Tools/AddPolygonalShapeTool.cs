using System;
using System.Linq;

using CoreGraphics;
using UIKit;

using Cession.Geometries;
using Cession.Drawing;
using Cession.Diagrams;
using Cession.UIKit;

namespace Cession.Tools
{
    public abstract class AddPolygonalShapeTool:Tool
    {
        private PolygonMeasurer _measurer;

        protected PolygonMeasurer Measurer
        {
            get{return _measurer;}
        }

        protected AddPolygonalShapeTool (ToolManager toolManager) : base (toolManager)
        {
            _measurer = new PolygonMeasurer ();
        }

        public override void TouchBegin (CGPoint point)
        {
            base.TouchBegin (point);
            if (_measurer.Points.Count == 0)
                _measurer.Points.Add (ConvertToLogicalPoint (point));
        }

        protected override void DoDraw (DrawingContext drawingContext)
        {
            drawingContext.DrawPolyline (_measurer.Points);
            if (_measurer.CurrentPoint != null)
            {
                Point p1 = _measurer.Points.Last ();
                Point p2 = _measurer.CurrentPoint.Value;
                drawingContext.StrokeLine (p1, p2);
                drawingContext.DrawDimension (p1, p2);
            }
        }

        public override void Pan (UIPanGestureRecognizer gestureRecognizer)
        {
            if (gestureRecognizer.IsDone ())
            {
                _measurer.Points.Add (_measurer.CurrentPoint.Value);
                _measurer.CurrentPoint = null;
            }
            else
            {
                CGPoint dp = gestureRecognizer.LocationInView (Host.ToolView);
                _measurer.CurrentPoint = ConvertToLogicalPoint (dp);
            }
            RefreshToolView ();
        }

        protected abstract void Commit();

        protected virtual void Clear()
        {
            _measurer.Clear ();
        }

        protected void Complete()
        {
            Commit ();
            Clear ();
            RefreshToolView ();
            RefreshDiagramView ();
        }
    }
}

