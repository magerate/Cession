using System;

using UIKit;

using Cession.Geometries;
using Cession.Diagrams;
using Cession.Aligning;

using D = Cession.Diagrams;

namespace Cession.Tools
{
    public class DivideCircleTool:AddPolygonalShapeTool
    {
        private D.Circle _circle;
        private CircleRule _alignRule;

        private ClosedShape _shape1;
        private ClosedShape _shape2;

        public DivideCircleTool (ToolManager toolManager) : base (toolManager)
        {
            _alignRule = new CircleRule ();

            var testItem = new UIBarButtonItem ();
            testItem.Title = "Test";
            testItem.Clicked += delegate
            {
                Complete();
            };

            NavigationItem.RightBarButtonItems = new UIBarButtonItem[] {
                testItem,
            };
        }

        protected override void DoDraw (Cession.Drawing.DrawingContext drawingContext)
        {
            base.DoDraw (drawingContext);

            drawingContext.SaveState ();
            UIColor.Red.SetStroke ();
            drawingContext.CGContext.SetLineWidth (4);
            if (null != _shape1)
            {
                drawingContext.StrokeCloseShape (_shape1);
            }
            if (null != _shape2)
            {
                drawingContext.StrokeCloseShape (_shape2);
            }
            drawingContext.RestoreState ();
        }

        public override void Enter (Tool parentTool, params object[] args)
        {
            _circle = args [0] as D.Circle;
            _alignRule.Center = _circle.Center;
            _alignRule.Radius = _circle.Radius;
        }

        protected override Point Align (Point point)
        {
            return _alignRule.Align (point);
        }

        protected override void Commit ()
        {
            Polyline polyline = Measurer.ToPolyline ();
            var regions = _circle.Split (polyline);
            _shape1 = regions.Item1;
            _shape2 = regions.Item2;
        }
    }
}

