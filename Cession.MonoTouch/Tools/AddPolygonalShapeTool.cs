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
                Point lp = GetOffsetedPoint (gestureRecognizer);
                lp = Align (lp);
                _measurer.CurrentPoint = lp;
            }
            RefreshToolView ();
        }


        protected virtual Point Align(Point point)
        {
            return point;
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

        protected override void Exit ()
        {
            Complete ();
            ToolManager.SelectTool (typeof(SelectTool));
        }
    }
}

