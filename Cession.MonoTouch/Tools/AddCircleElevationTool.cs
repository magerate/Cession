using System;
using System.Linq;

using CoreGraphics;
using UIKit;

using Cession.Geometries;
using Cession.Drawing;
using Cession.Diagrams;
using Cession.UIKit;
using G = Cession.Geometries;
using D = Cession.Diagrams;

namespace Cession.Tools
{
    public class AddCircleElevationTool:AddCircleTool
    {
        public AddCircleElevationTool (ToolManager toolManager) : base (toolManager)
        {
        }

        protected override void Commit ()
        {
            Point center = G.Segment.GetCenter (StartPoint.Value, EndPoint.Value);
            double radius = Point.DistanceBetween (StartPoint.Value, EndPoint.Value) / 2;
            var circle = new D.Circle (center, radius);
            var elevation = new Elevation (circle);
            CommandManager.ExecuteListAdd (CurrentLayer.Shapes, elevation);
        }
    }
}

