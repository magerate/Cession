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
    public class AddPolylineTool:DiscreteTool
    {
        private PolygonMeasurer _measurer;

        public AddPolylineTool (ToolManager toolManager) : base (toolManager)
        {
            _measurer = new PolygonMeasurer ();
            InitializeNavigationItem ();
        }

        private void InitializeNavigationItem()
        {
            NavigationItem = new UINavigationItem ();

            var exitButton = new UIBarButtonItem (UIBarButtonSystemItem.Done);
            exitButton.Clicked += delegate
            {
                Complete();
            };
            NavigationItem.LeftBarButtonItem = exitButton;


            var doneButton = new UIBarButtonItem (UIBarButtonSystemItem.Done);
            doneButton.Clicked += delegate
            {
                if(_measurer.Points.Count> 1)
                    Commit();
            };

            var arcButton = new UIBarButtonItem ();
            arcButton.Title = "Arc";
            NavigationItem.RightBarButtonItems = new UIBarButtonItem[]{
                arcButton,
                doneButton,
            };
        }

        private void Complete()
        {
            if(_measurer.Points.Count > 1)
                Commit ();
            ToolManager.SelectTool (typeof(SelectTool));
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

//        public override void Pinch (UIPinchGestureRecognizer gestureRecognizer)
//        {
//            if (gestureRecognizer.IsDone ())
//            {
////                Point p1 = _measurer.Points.Last ();
////                Point p2 = _measurer.CurrentPoint.Value;
////                Point middle = new Point ((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
////                Vector v = p2 - p1;
////                v.Rotate (Math.PI / 2);
////                Point pp = middle + v;
////
////                _measurer.Points.Add (_measurer.CurrentPoint.Value);
////                _measurer.ArcPoints.Add (p1, pp);
////
////                _measurer.CurrentPoint = null;
//            }
//            else
//            {
//                CGPoint dp = gestureRecognizer.LocationInView (Host.ToolView);
//                _measurer.CurrentPoint = ConvertToLogicalPoint (dp);
//            }
//            RefreshToolView ();
//        }

        protected virtual void Commit()
        {
            var polyline = _measurer.ToPolyline ();
            CommandManager.ExecuteListAdd (CurrentLayer.Shapes, polyline);

            _measurer.Clear ();
            RefreshToolView ();
            RefreshDiagramView ();
        }
    }
}

