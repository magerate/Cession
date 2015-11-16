using System;
using System.Linq;
using System.Collections.Generic;

using CoreGraphics;
using UIKit;

using Cession.Geometries;
using Cession.Drawing;
using Cession.Diagrams;
using Cession.UIKit;

namespace Cession.Tools
{
    public class SplitPolygonTestTool:AddPolygonalShapeTool
    {
        private Point[] polygon1;
        private Point? p1;
        private Point? p2;
        private Point[][] result;

        public SplitPolygonTestTool (ToolManager toolManager) : base (toolManager)
        {
        }

        static Random rnd = new Random ();
        private static UIColor GetRandomColor()
        {
            return UIColor.FromRGB (
                rnd.Next(255),
                rnd.Next(255),
                rnd.Next(255)
            );
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
            if (polygon1 != null && result == null)
            {
                drawingContext.SaveState ();
                UIColor.Green.SetFill ();
                drawingContext.FillPolygon (polygon1);
                drawingContext.RestoreState ();
            }

            if (p1 != null && p2 != null)
            {
                drawingContext.SaveState ();
                drawingContext.StrokeLine (p1.Value, p2.Value);
                drawingContext.RestoreState ();
            }

            base.DoDraw (drawingContext);


            if (result != null)
            {
                for (int i = 0; i < result.Length; i++)
                {
                    drawingContext.SaveState ();
                    var color = GetRandomColor ();
                    color.SetFill ();
                    drawingContext.DrawPolygon (result[i]);
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

            if (null == p1 && null == p2)
            {
                p1 = Measurer.Points [0];
                p2 = Measurer.Points [1];
            }

            if (null != polygon1 && null != p1 &&  null != p2)
            {
                result = Polygon.Split (polygon1, p1.Value, p2.Value);
            }

            RefreshToolView ();
        }

        protected override void Exit ()
        {
            polygon1 = null;
            p1  = null;
            p2 = null;
            result = null;
            RefreshToolView ();
        }
    }
}

