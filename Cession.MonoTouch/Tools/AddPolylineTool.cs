using System;
using System.Linq;

using CoreGraphics;
using UIKit;

using Cession.Geometries;
using Cession.Drawing;
using Cession.Diagrams;
using Cession.UIKit;
using Cession.Commands;

namespace Cession.Tools
{
    public class AddPolylineTool:AddPolygonalShapeTool
    {
        private UIBarButtonItem _arcButton;

        public AddPolylineTool (ToolManager toolManager) : base (toolManager)
        {
            InitializeNavigationItem ();
        }

        private void InitializeNavigationItem()
        {
            NavigationItem = new UINavigationItem ();

            var exitButton = new UIBarButtonItem ();
            exitButton.Title = "Exit";
            exitButton.Clicked += delegate
            {
                Exit();
            };
            NavigationItem.LeftBarButtonItem = exitButton;


            var doneButton = new UIBarButtonItem (UIBarButtonSystemItem.Done);
            doneButton.Clicked += delegate
            {
                Complete();
            };

            _arcButton = new UIBarButtonItem ();
            _arcButton.Title = "Arc";
            _arcButton.Clicked += delegate
            {
                if(_arcButton.Style == UIBarButtonItemStyle.Plain)
                    _arcButton.Style = UIBarButtonItemStyle.Done;
                else
                    _arcButton.Style = UIBarButtonItemStyle.Plain;
            };

            NavigationItem.RightBarButtonItems = new UIBarButtonItem[]{
                _arcButton,
                doneButton,
            };
        }

        public override void Pan (UIPanGestureRecognizer gestureRecognizer)
        {
            if (_arcButton.Style == UIBarButtonItemStyle.Done)
            {
                AddArc (gestureRecognizer);
            }
            else
            {
                base.Pan (gestureRecognizer);
            }
        
        }

        public void AddArc (UIPanGestureRecognizer gestureRecognizer)
        {
            if (gestureRecognizer.IsDone ())
            {
                Point p1 = Measurer.Points.Last ();
                Point p2 = Measurer.CurrentPoint.Value;
                Point middle = new Point ((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                Vector v = p2 - p1;
                v.Rotate (Math.PI / 2);
                Point pp = middle + v;

                Measurer.ArcPoints.Add (p1, pp);
                Measurer.AddPoint ();
            }
            else
            {
                CGPoint dp = gestureRecognizer.LocationInView (Host.ToolView);
                Measurer.CurrentPoint = ConvertToLogicalPoint (dp);
            }
            RefreshToolView ();
        }

        protected override void Commit()
        {
            if (Measurer.Points.Count > 1)
            {
                var polyline = Measurer.ToPolyline ();
                var command = Command.CreateListAdd(CurrentLayer.Shapes, polyline);
                CommandManager.Execute (command);
            }
        }
    }
}

