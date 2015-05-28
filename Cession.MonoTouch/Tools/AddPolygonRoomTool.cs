﻿using System;
using System.Linq;

using CoreGraphics;
using UIKit;

using Cession.Geometries;
using Cession.Drawing;
using Cession.Diagrams;
using Cession.UIKit;

namespace Cession.Tools
{
    public class AddPolygonRoomTool:AddPolygonalShapeTool
    {
        public AddPolygonRoomTool (ToolManager toolManager) : base (toolManager)
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

            NavigationItem.RightBarButtonItems = new UIBarButtonItem[]{
                doneButton,
            };
        }

        protected override void Commit ()
        {
            if (Measurer.Points.Count > 2)
            {
                var path = Measurer.ToPath ();
                var room = new Room (path);
                ExecuteAddShape (room);
            }
        }
    }
}

