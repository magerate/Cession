﻿using System;
using System.Linq;

using CoreGraphics;
using UIKit;

using Cession.Geometries;
using Cession.Drawing;
using Cession.Diagrams;
using Cession.UIKit;
using Cession.Commands;
using G = Cession.Geometries;
using D = Cession.Diagrams;

namespace Cession.Tools
{
    public class AddCircleRoomTool:AddCircleTool
    {
        public AddCircleRoomTool (ToolManager toolManager) : base (toolManager)
        {
        }

        protected override void Commit ()
        {
            Point center = G.Segment.GetCenter (StartPoint.Value, EndPoint.Value);
            double radius = Point.DistanceBetween (StartPoint.Value, EndPoint.Value) / 2;
            var circle = new D.Circle (center, radius);
            var room = new Room (circle);
            var command = Command.CreateListAdd (CurrentLayer.Shapes, room);
            CommandManager.Execute (command);
        }
    }
}

