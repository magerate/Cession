using System;
using System.Linq;
using System.Collections.Generic;

using CoreGraphics;
using UIKit;

using Cession.Geometries;
using Cession.Drawing;
using Cession.Diagrams;
using Cession.UIKit;
using Cession.Geometries.Clipping.GreinerHormann;

namespace Cession.Tools
{
    public static class ClipH
    {
        public static Point[][] Clip(Point[] subject, Point[] clip)
        {
            var v1 = subject.ToLinkList ();
            var v2 = clip.ToLinkList ();
            var result = Clipper.Intersect (v1, v2);
            var pa = result.Select (l => l.Select (v => v.ToPoint()).ToArray ()).ToArray ();

            return pa;
        }
    }

    public class ClipTestTool:AddPolygonalShapeTool
    {
        private Point[] polygon1;
        private Point[] polygon2;
        private Point[][] result;

        public ClipTestTool (ToolManager toolManager) : base (toolManager)
        {
        }

        protected override void InitializeNavigationItem()
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

            NavigationItem.RightBarButtonItems = new UIBarButtonItem[]{
                doneButton,
            };
        }

        protected override void DoDraw (DrawingContext drawingContext)
        {
            if (polygon1 != null)
            {
                drawingContext.SaveState ();
                UIColor.Green.SetFill ();
                drawingContext.FillPolygon (polygon1);
                drawingContext.RestoreState ();
            }

            if (polygon2 != null)
            {
                drawingContext.SaveState ();
                UIColor.Blue.SetFill ();
                drawingContext.FillPolygon (polygon2);
                drawingContext.RestoreState ();
            }

            base.DoDraw (drawingContext);


            if (result != null)
            {
                foreach (var p in result)
                {
                    drawingContext.SaveState ();
                    drawingContext.CGContext.SetAlpha (.5f);
                    UIColor.Red.SetFill ();
                    drawingContext.FillPolygon (p);
                    drawingContext.RestoreState ();
                }
            }
        }

        protected override void Commit ()
        {
            if (null == polygon1)
            {
                polygon1 = Measurer.Points.ToArray ();
                RefreshToolView ();
                return;
            }

            if (null == polygon2)
            {
                polygon2 = Measurer.Points.ToArray ();
            }

            if (null != polygon1 && null != polygon2)
            {
                result = ClipH.Clip (polygon1, polygon2);
            }

            RefreshToolView ();
        }

        protected override void Exit ()
        {
            polygon1 = null;
            polygon2 = null;
            result = null;
            RefreshToolView ();
        }
    }
}

