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
    public abstract class AddPolygonalShapeTool:DiscreteTool
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
            _measurer.Draw (drawingContext);
        }

        public override void Pan (UIPanGestureRecognizer gestureRecognizer)
        {
            if (gestureRecognizer.IsDone ())
            {
                _measurer.AddPoint ();
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

        protected virtual void Complete()
        {
            Commit ();
            Clear ();
            RefreshToolView ();
            RefreshDiagramView ();
        }

        protected void Exit ()
        {
            Complete ();
            ToolManager.SelectTool (typeof(SelectTool));
        }
    }
}

